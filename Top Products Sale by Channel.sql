USE OMSDb1
GO
--- In Customer DashBoard (Top Products Sale by Channel) 
; 
WITH TMP AS
(	
	SELECT chal.Id
			,chal.ChannelName
			,odrl.ProductId
			,pd.ProductName
			,SUM(odr.TotalPrice) AS TotalSpend
			,COUNT(odr.Id) AS NumberOfOrders
			,ROW_NUMBER() OVER(PARTITION BY chal.Id 
								ORDER BY SUM(odr.TotalPrice) DESC) AS rank
	FROM OrderList odr
	JOIN Channel chal ON odr.ChannelId = chal.Id
	JOIN OrderDetail odrl ON odr.Id = odrl.OrderId
	JOIN Product pd ON odrl.ProductId = pd.Id
	WHERE CONVERT(date,odr.OrderedAt) BETWEEN '2023-01-24'
		AND DATEADD(SECOND, 1, '2023-03-30')
	AND odr.OrderStatus NOT IN ('FAILED','CANCELLED','RETURN')
	GROUP BY chal.Id
			,chal.ChannelName
			,odrl.ProductId
			,pd.ProductName
)
SELECT TOP 4
		ChannelName	
		,ProductId
		,ProductName
		,TotalSpend
		,NumberOfOrders
FROM TMP
WHERE rank = 1
ORDER BY TotalSpend DESC
		,NumberOfOrders DESC

 -- khach hang nao chi nhieu tien nhat cho moi channel
 
 