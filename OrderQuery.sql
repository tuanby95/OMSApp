

--- In Dashboard Screen (Total Orders)- Orders by status Completed, Failed, Return
DECLARE @PageNumber INT;
DECLARE @RowsOfPage INT;
DECLARE @MaxTablePage FLOAT;
SET @PageNumber = 1;
SET @RowsOfPage = 5;
SELECT @MaxTablePage = COUNT(*)
						FROM OrderList odr
						WHERE CONVERT(date,odr.OrderedAt) BETWEEN '2023-01-24'
							AND DATEADD(SECOND, 1, '2023-01-31')
SET @MaxTablePage = CEILING(@MaxTablePage/@RowsOfPage)
WHILE @MaxTablePage >= @PageNumber
BEGIN
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
	 ORDER BY OrderedDate ASC
	 OFFSET (@PageNumber-1)*@RowsOfPage ROWS FETCH NEXT @RowsOfPage ROWS ONLY
	 SET @PageNumber = @PageNumber + 1 
END


