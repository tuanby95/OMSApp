SELECT Region,
       Round(100*(sum(CASE
                          WHEN month(OrderedAt) = month(getdate())
                               AND year(OrderedAt) = year(getdate()) THEN TotalPrice
							  ELSE 0
                      END) - sum(CASE
                                     WHEN month(OrderedAt) = month(dateadd(MONTH, -1, CURRENT_TIMESTAMP))
                                          AND year(OrderedAt) = year(dateadd(MONTH, -1, CURRENT_TIMESTAMP)) THEN TotalPrice
										  ELSE 0
                                 END)) / sum(CASE
                                                 WHEN month(OrderedAt) = month(dateadd(MONTH, -1, CURRENT_TIMESTAMP))
                                                      AND year(OrderedAt) = year(dateadd(MONTH, -1, CURRENT_TIMESTAMP)) THEN TotalPrice
													  ELSE 0
                                             END), 0) as PercentageGrowUp
FROM OrderList
GROUP BY Region
