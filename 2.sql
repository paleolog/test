CREATE TABLE [dbo].[Sales]
(
  [salesid] INT IDENTITY(1, 1),
  [productid] INT,
  [datetime] DATETIME,
  [customerid] INT
)
GO
--список первых продуктов по всем клиентам
SELECT l.*
FROM
(
  SELECT l.[productid], COUNT(*) [cnt]
  FROM
  (
    SELECT s.[productid]
    FROM [dbo].[Sales] AS s
    WHERE NOT EXISTS(SELECT * FROM [dbo].[Sales] AS ss
                     WHERE(ss.[customerid] = s.[customerid]) 
                       AND (ss.[datetime] < s.[datetime]))
  ) AS l  
  GROUP BY l.[productid]
) AS l  
ORDER BY l.[cnt] DESC
