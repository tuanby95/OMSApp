
--- In Dashboard Screen (Total Orders)- Orders by status Completed, Failed, Return
SELECT CONVERT(date,odl.OrderedAt) AS OrderedDate ,
		SUM(CASE WHEN odl.OrderStatus IN('COMPLETED') THEN 1 ELSE 0 END )AS NumberOfCompleted
		,SUM(CASE WHEN odl.OrderStatus IN('FAILED') THEN 1 ELSE 0 END )AS NumberOfFailed
		,SUM(CASE WHEN odl.OrderStatus IN('RETURN') THEN 1 ELSE 0 END )AS NumberOfReturn
		,COUNT(1) AS NumberOfOrder
   FROM [OrderList] odl
   WHERE
	CONVERT(date,odl.OrderedAt) BETWEEN '2023-01-24'
	AND DATEADD(SECOND, 1, '2023-01-31')
   GROUP BY CONVERT(date,odl.OrderedAt)

