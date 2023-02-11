
--2TotalSaleByMonth
WITH tmpChannelMonth AS(
	SELECT MonthValue, c.*
	FROM 
	(
		SELECT (1) AS MonthValue union
		SELECT 2 union
		SELECT 3 union
		SELECT 4 union
		SELECT 5 union
		SELECT 6 union
		SELECT 7 union
		SELECT 8 union
		SELECT 9 union
		SELECT 10 union
		SELECT 11 union
		SELECT 12 
	) t  cross join 
	Channel as c
)

SELECT chn.MonthValue, chn.ChannelName, Sum(odl.TotalPrice) Totals, Datename(year,  odl.OrderedAt) as YearCol
FROM 
	tmpChannelMonth chn LEFT JOIN
	OrderList as odl ON odl.ChannelId = chn.Id and chn.MonthValue = Month(odl.OrderedAt)
WHERE odl.OrderStatus = 'COMPLETED'
	AND OrderedAt > DATEADD(MONTH, (DATEDIFF(MONTH, 0, CURRENT_TIMESTAMP) - 4 ), 0)
Group by chn.MonthValue, chn.ChannelName,Datename(year,  odl.OrderedAt)
Order by chn.MonthValue;

