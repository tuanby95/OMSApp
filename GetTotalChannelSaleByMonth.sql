
--2TotalSaleByMonth
WITH tmpChannelMonth AS
  (SELECT MonthValue,
          c.*
   FROM
     (SELECT (1) AS MonthValue
      UNION SELECT 2
      UNION SELECT 3
      UNION SELECT 4
      UNION SELECT 5
      UNION SELECT 6
      UNION SELECT 7
      UNION SELECT 8
      UNION SELECT 9
      UNION SELECT 10
      UNION SELECT 11
      UNION SELECT 12) t
   CROSS JOIN Channel AS c)
SELECT chn.MonthValue,
       chn.ChannelName,
       Sum(odi.TotalPrice) Totals,
       Datename(YEAR, odi.OrderedAt) AS YearCol
FROM tmpChannelMonth chn
LEFT JOIN OrderInfo AS odi ON odi.ChannelId = chn.Id
AND chn.MonthValue = Month(odi.OrderedAt)
WHERE odi.OrderStatus = 'COMPLETED'
  AND OrderedAt > DATEADD(MONTH, (DATEDIFF(MONTH, 0, CURRENT_TIMESTAMP) - 4), 0)
GROUP BY chn.MonthValue,
         chn.ChannelName,
         Datename(YEAR, odi.OrderedAt)
ORDER BY chn.MonthValue;
