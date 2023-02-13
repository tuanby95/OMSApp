
--- All ProductVariants in All Channels  
DECLARE @PageNumber INT;
DECLARE @RowsOfPage INT;
DECLARE @MaxTablePage FLOAT;
SET @PageNumber = 1;
SET @RowsOfPage = 10;

SELECT @MaxTablePage = COUNT(*) FROM ProductVariant
SET @MaxTablePage = CEILING(@MaxTablePage/@RowsOfPage)
WHILE @MaxTablePage >= @PageNumber
BEGIN
	SELECT  DISTINCT   pv.Id
				,pvi.ProductVariantImage
				,pv.SKU, pd.ProductName
				,pv.ProductVariantValue 
				,pcd.Quantity
				,pcd.Inprocess
				,pcd.Sold
	FROM Product pd
	JOIN ProductVariant pv        ON pd.Id = pv.ProductId
	JOIN ProductChannel pc        ON pc.ProductVariantId = pv.Id   
	JOIN ProductChannelDetail pcd ON pv.Id = pcd.ProductChannelId 
	JOIN ProductVariantImage pvi  ON pv.Id = pvi.ProductVariantId
	WHERE pvi.MainImage = 1
	ORDER BY pcd.Quantity DESC
	OFFSET (@PageNumber-1)*@RowsOfPage ROWS FETCH NEXT @RowsOfPage ROWS ONLY
	SET @PageNumber = @PageNumber + 1 
END


--- All ProductVariants by Stock Available in All Channels 
DECLARE @PageNumber INT;
DECLARE @RowsOfPage INT;
DECLARE @MaxTablePage FLOAT;
SET @PageNumber = 1;
SET @RowsOfPage = 10;

SELECT @MaxTablePage = COUNT(*) FROM ProductVariant
SET @MaxTablePage = CEILING(@MaxTablePage/@RowsOfPage)
WHILE @MaxTablePage >= @PageNumber
BEGIN
	SELECT  DISTINCT   pv.Id
				,pvi.ProductVariantImage
				,pv.SKU, pd.ProductName
				,pv.ProductVariantValue 
				,pcd.Quantity
				,pcd.Inprocess
				,pcd.Sold
	FROM Product pd
	JOIN ProductVariant pv        ON pd.Id = pv.ProductId
	JOIN ProductChannel pc        ON pc.ProductVariantId = pv.Id   
	JOIN ProductChannelDetail pcd ON pv.Id = pcd.ProductChannelId 
	JOIN ProductVariantImage pvi  ON pv.Id = pvi.ProductVariantId
	WHERE pvi.MainImage = 1
		AND pcd.Quantity != 0
	ORDER BY pcd.Quantity DESC
	OFFSET (@PageNumber-1)*@RowsOfPage ROWS FETCH NEXT @RowsOfPage ROWS ONLY
	SET @PageNumber = @PageNumber + 1 
END

---  All ProductVariants by Low on Stock in All Channels 
DECLARE @PageNumber INT;
DECLARE @RowsOfPage INT;
DECLARE @MaxTablePage FLOAT;
SET @PageNumber = 1;
SET @RowsOfPage = 10;

SELECT @MaxTablePage = COUNT(*) FROM ProductVariant
SET @MaxTablePage = CEILING(@MaxTablePage/@RowsOfPage)
WHILE @MaxTablePage >= @PageNumber
BEGIN
	SELECT  DISTINCT   pv.Id
				,pvi.ProductVariantImage
				,pv.SKU, pd.ProductName
				,pv.ProductVariantValue 
				,pcd.Quantity
				,pcd.Inprocess
				,pcd.Sold
	FROM Product pd
	JOIN ProductVariant pv        ON pd.Id = pv.ProductId
	JOIN ProductChannel pc        ON pc.ProductVariantId = pv.Id   
	JOIN ProductChannelDetail pcd ON pv.Id = pcd.ProductChannelId 
	JOIN ProductVariantImage pvi  ON pv.Id = pvi.ProductVariantId
	WHERE pvi.MainImage = 1
		AND pcd.Quantity <= 10 AND pcd.Quantity > 0
	ORDER BY pcd.Quantity DESC
	OFFSET (@PageNumber-1)*@RowsOfPage ROWS FETCH NEXT @RowsOfPage ROWS ONLY
	SET @PageNumber = @PageNumber + 1 
END


---  All ProductVariants by Out of Stock in All Channels 
DECLARE @PageNumber INT;
DECLARE @RowsOfPage INT;
DECLARE @MaxTablePage FLOAT;
SET @PageNumber = 1;
SET @RowsOfPage = 10;

SELECT @MaxTablePage = COUNT(*) FROM ProductVariant
SET @MaxTablePage = CEILING(@MaxTablePage/@RowsOfPage)
WHILE @MaxTablePage >= @PageNumber
BEGIN
	SELECT  DISTINCT   pv.Id
				,pvi.ProductVariantImage
				,pv.SKU, pd.ProductName
				,pv.ProductVariantValue 
				,pcd.Quantity
				,pcd.Inprocess
				,pcd.Sold
	FROM Product pd
	JOIN ProductVariant pv        ON pd.Id = pv.ProductId
	JOIN ProductChannel pc        ON pc.ProductVariantId = pv.Id   
	JOIN ProductChannelDetail pcd ON pv.Id = pcd.ProductChannelId 
	JOIN ProductVariantImage pvi  ON pv.Id = pvi.ProductVariantId
	WHERE pvi.MainImage = 1
		AND pcd.Quantity = 0
	ORDER BY pcd.Quantity DESC
	OFFSET (@PageNumber-1)*@RowsOfPage ROWS FETCH NEXT @RowsOfPage ROWS ONLY
	SET @PageNumber = @PageNumber + 1 
END




