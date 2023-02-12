-- Total Orders theo tổng  // Screen Dashboard
SELECT SUM (odl.TotalPrice) AS TotalOrders
FROM OrderList odl 
WHERE odl.OrderStatus = 'COMPLETED'
  AND odl.OrderedAt BETWEEN '2023-01-01 00:00:00' AND DATEADD(SECOND, -1, '2023-07-27')  
  AND Year(odl.OrderedAt) = Year(GETDATE()) 