DECLARE @PageNumber AS INT DECLARE @RowsOfPage AS INT DECLARE @MaxTablePage AS FLOAT
SET @PageNumber=1
SET @RowsOfPage=10
SELECT @MaxTablePage = COUNT (*)
FROM Payment
SET @MaxTablePage = CEILING(@MaxTablePage/@PageNumber) WHILE @MaxTablePage > @PageNumber BEGIN
SELECT pmt.PaymentMethod,
       pmt.CardNumber,
       pmt.CreatedAt,
       pmt.CreatedAt AS LastUpdated
FROM Payment AS pmt
ORDER BY pmt.PaymentMethod
OFFSET (@PageNumber-1)*@RowsOfPage ROWS FETCH NEXT @RowsOfPage ROWS ONLY
SET @PageNumber = @PageNumber + 1 END