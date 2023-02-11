SELECT count(fdk.Id) AS FeedbackCol,
       datename(MONTH, fdk.CreatedAt) AS MonthChar,	
       month(fdk.CreatedAt) AS MonthCol
FROM Feedback AS fdk
WHERE  fdk.CreatedAt > DATEADD(MONTH, (DATEDIFF(MONTH, 0, CURRENT_TIMESTAMP) - 4 ), 0)
	 GROUP BY datename(MONTH, fdk.CreatedAt),
         month(fdk.CreatedAt);