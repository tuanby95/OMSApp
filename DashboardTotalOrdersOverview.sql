SELECT DATENAME(MONTH, odl.OrderedAt) AS Thang,
       SUM(odl.TotalPrice) AS TongTien
FROM OrderList odl
WHERE odl.OrderStatus ='COMPLETED'
  AND odl.OrderedAt BETWEEN '2023-01-01 00:00:00' AND DATEADD(SECOND, -1, '2023-07-27')
  AND YEAR(odl.OrderedAt) = YEAR(GETDATE())
GROUP BY DATENAME(MONTH, odl.OrderedAt)