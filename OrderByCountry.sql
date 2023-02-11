USE OMSDb1
GO

---		DASHBOARD SCREEN (sale by location [Orders] by location table )
DECLARE @PageNumber INT;
DECLARE @RowsOfPage INT;
DECLARE @MaxTablePage FLOAT;
SET @PageNumber = 1;
SET @RowsOfPage = 5;
SELECT @MaxTablePage = COUNT(*)
						FROM OrderList odr
						WHERE CONVERT(date,odr.OrderedAt) BETWEEN '2023-01-24'
							  AND DATEADD(DAY, 1, '2023-01-28')
SET @MaxTablePage = CEILING(@MaxTablePage/@RowsOfPage)
WHILE @MaxTablePage >= @PageNumber
BEGIN
	SELECT odr.Region
			,COUNT(odr.Id) AS NumberOfOrders
			,SUM(odr.TotalPrice) AS TotalSales
	FROM OrderList odr
	WHERE  odr.OrderStatus NOT IN('FAILED','CANCELLED','RETURN')
		AND CONVERT(date,odr.OrderedAt) BETWEEN '2023-01-24'
		AND DATEADD(SECOND, 1, '2023-01-30')
	GROUP BY odr.Region
	ORDER BY TotalSales DESC
	OFFSET (@PageNumber-1)*@RowsOfPage ROWS FETCH NEXT @RowsOfPage ROWS ONLY
	SET @PageNumber = @PageNumber + 1 
END