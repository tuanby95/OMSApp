DECLARE @PageNumber INT;
DECLARE @RowsofPage INT ; 
DECLARE @MaxTablePage FLOAT;

SET @PageNumber = 1;
SET @RowsOfPage = 11;
Select @MaxTablePage = COUNT(*) FROM Channel
SET @MaxTablePage = CEILING(@MaxTablePage/@RowsOfPage)
WHILE @MaxTablePage >= @PageNumber
BEGIN
SELECT ChannelName,
       SUM(CASE
               WHEN odl.OrderStatus NOT IN ('FAILED', 'RETURN', 'CANCELED') THEN 1
               ELSE 0
           END) AS NumberOfOrder,
       SUM(CASE
               WHEN odl.OrderStatus NOT IN ('FAILED', 'RETURN', 'CANCELED') THEN odl.TotalPrice
           END) AS Tongtien,
       chn.ChannelStatus,
       chn.CreatedAt,
       chn.LastUpdate
FROM Channel chn
JOIN OrderList odl ON chn.Id = odl.ChannelId
GROUP BY ChannelName,
         ChannelStatus,
         CreatedAt,
         LastUpdate
ORDER BY ChannelName DESC
OFFSET (@PageNumber-1)*@RowsOfPage ROWS FETCH NEXT @RowsOfPage ROWS ONLY
SET @PageNumber = @PageNumber + 1 

END