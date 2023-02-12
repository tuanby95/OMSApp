SELECT Count(odl.RecipientName)
FROM  OrderList odl 
WHERE odl.OrderStatus = 'COMPLETED'

-- Table chanel + number of customer share in % -- 
DECLARE @totalsale Float ;
SET @totalsale =
  (SELECT COUNT (DISTINCT odl.BuyerId) AS TotalSale
   FROM OrderList odl)
SELECT cnl.ChannelName,
       COUNT(DISTINCT odl.BuyerId) AS NumberOfOrder ,
       ROUND ((COUNT( DISTINCT odl.BuyerId)/@totalsale)*100,0) AS phantram
FROM Channel cnl
INNER JOIN OrderList odl ON cnl.id = odl.ChannelId	
GROUP BY ChannelName