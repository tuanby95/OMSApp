--1 TotalRevenue by Month
SELECT SUM(odi.TotalPrice) AS RevenueCol,
       DATENAME(MONTH, odi.OrderedAt) AS MonthCharCol,
       YEAR(odi.OrderedAt) AS YearCol
FROM OrderInfo AS odi
WHERE odi.OrderStatus = 'COMPLETED'
  AND OrderedAt > DATEADD(MONTH, (DATEDIFF(MONTH, 0, CURRENT_TIMESTAMP) - 4), 0)
GROUP BY DATENAME(MONTH, odi.OrderedAt),
         YEAR(odi.OrderedAt),
         MONTH(odi.OrderedAt)
ORDER BY YEAR(odi.OrderedAt),
         MONTH(odi.OrderedAt) ASC;
