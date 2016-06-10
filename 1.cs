using System;

namespace ConsoleApplication1
{
  public class RouteElem
  {
    public string PointFrom { get; set; }
    public string PointTo { get; set; }

    public RouteElem(string astrForm, string astrTo)
    {
      PointFrom = astrForm;
      PointTo = astrTo;
    }
  }

  class Program
  {
    public static RouteElem[] OrderRoute(RouteElem[] aarrPoints)
    {
      RouteElem[] arrRes = new RouteElem[aarrPoints.Length];
      Array.Copy(aarrPoints, arrRes, aarrPoints.Length);
      //ищем 1-й элемент (на него нет ссылок слева)
      for(int i=0;i<arrRes.Length; i++)
      {
        bool fFound = false;
        for(int j=i+1; j<arrRes.Length; j++)
        {
          if(arrRes[j].PointTo == arrRes[i].PointFrom)
          {
            fFound = true;
            break;
          }
        }
        if(!fFound)
        {
          //нашли 1-й элемент
          RouteElem pElem = arrRes[0];
          arrRes[0] = arrRes[i];
          arrRes[i] = arrRes[0];
          break;
        }
      }
      //ищем последующие
      for(int i=0;i<arrRes.Length-1; i++)
      {
        for(int j=i+1; j<arrRes.Length; j++)
        {
          if(arrRes[i].PointTo == arrRes[j].PointFrom)
          {
            RouteElem pElem = arrRes[i + 1];
            arrRes[i + 1] = arrRes[j];
            arrRes[j] = pElem;
            break;
          }
        }
      }
      return arrRes;
    }

    static void Main(string[] args)
    {
      RouteElem[] arrSrc = new RouteElem[] { new RouteElem("Мельбурн", "Кельн"), new RouteElem("Париж", "Дакар"), new RouteElem("Москва", "Париж"), new RouteElem("Кельн", "Москва") };
      RouteElem[] arrRes = OrderRoute(arrSrc);
      foreach(RouteElem pElem in arrRes)
      {
        Console.WriteLine("{0}>{1}", pElem.PointFrom, pElem.PointTo);
      }
      Console.ReadKey();
    }
  }
}

/*
Сложность получившегося алгоритма:

 * Макс. число операций: СУММА_ПРОГРЕССИИ(a[i]=i, i=1..N)+СУММА_ПРОГРЕССИИ(a[i]=i, i=1..N) = 2*(N^2+N)/2
 * Сложность O(N^2)
 * 
*/
