---- Product Details
SELECT pd.ProductName
		,pv.ProductVariantValue
		,pv.SKU
		,pd.Cost
		,pd.ProductHeight
		,pd.ProductWidth
		,pd.ProductLength
		,pd.ProductWeight
		,pd.ProductDescription
		,pri.ProviderName
FROM Product pd
JOIN ProductVariant pv ON pd.Id = pv.ProductId
JOIN [Provider] pri  ON pd.ProviderId = pri.Id 
WHERE pv.Id = '1'

--- WareHouse Stock 
SELECT wh.WarehouseName
		,pcd.Quantity
FROM Warehouse wh 
JOIN ProductChannelDetail pcd ON wh.Id = pcd.WarehouseId
JOIN ProductChannel pc		  ON pcd.ProductChannelId = pc.Id
WHERE pc.ProductVariantId = '1'
--- Listed Stock on channels Stores

SELECT chal.ChannelName
		,pc.CSKU
		,pcd.Quantity
FROM ProductChannel pc
LEFT JOIN ProductChannelDetail pcd ON pc.Id = pcd.ProductChannelId
JOIN Channel chal	ON chal.Id = pc.ChannelId				
WHERE pc.ProductVariantId = '1'