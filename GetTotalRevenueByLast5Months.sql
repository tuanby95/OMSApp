--1 TotalRevenue by Month
 SELECT 
	SUM(odl.TotalPrice) AS RevenueCol,
	DATENAME(MONTH,odl.OrderedAt) as MonthCharCol,
	YEAR(odl.OrderedAt) as YearCol
FROM OrderList odl
WHERE odl.OrderStatus = 'COMPLETED'
and OrderedAt > DATEADD(MONTH, (DATEDIFF(MONTH, 0, CURRENT_TIMESTAMP) - 4 ), 0)
GROUP BY DATENAME(MONTH,odl.OrderedAt), YEAR(odl.OrderedAt),
		MONTH(odl.OrderedAt)
ORDER BY YEAR(odl.OrderedAt), MONTH(odl.OrderedAt) ASC;