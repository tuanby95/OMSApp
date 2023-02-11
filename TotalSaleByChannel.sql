USE OMSDb1
GO
--Total Sale By Channel

-- Main DashBoard Screen (Total Sale By Channel)
DECLARE @TotalSale float
SET @TotalSale =
  (SELECT SUM(CASE WHEN odr.OrderStatus NOT IN('FAILED','CANCELLED','RETURN') THEN odr.TotalPrice END) AS TotalSale
   FROM [OrderList] odr
   WHERE MONTH(odr.OrderedAt) = MONTH(GETDATE())
   AND YEAR(odr.OrderedAt) = YEAR(GETDATE())
   )
SELECT TOP 4 chal.Id
	   ,chal.ChannelName
	   ,SUM(CASE WHEN odr.OrderStatus NOT IN('FAILED','CANCELLED','RETURN') THEN odr.TotalPrice END) AS Total
	   ,ROUND(SUM(CASE WHEN odr.OrderStatus NOT IN('FAILED','CANCELLED','RETURN') THEN odr.TotalPrice END)/@TotalSale * 100,2) AS Percentages 
FROM  Channel chal
LEFT JOIN  [OrderList] odr ON chal.Id = odr.ChannelId
WHERE MONTH(odr.OrderedAt) = MONTH(GETDATE())
AND YEAR(odr.OrderedAt) = YEAR(GETDATE())
GROUP BY chal.Id,
		 chal.ChannelName



