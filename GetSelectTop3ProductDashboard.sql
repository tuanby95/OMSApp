SELECT TOP 3 ProductName AS Products,
           SUM(ord.Quantity * p.Price) AS ValueSale
FROM Product p
INNER JOIN OrderDetail ord ON p.ProviderId = ord.Id
GROUP BY ProductName
ORDER BY ValueSale DESC

Select * From OrderList
Select * from OrderDetail