CREATE PROCEDURE Sales_CTE
	@productCategory nvarchar(100) = NULL,
	@startDate datetime = NULL,
	@endDate datetime = NULL,
	@territory nvarchar(50) = NULL
AS
WITH Sales_CTE AS(
	SELECT 
		p.Name as ProductName,
		pc.Name as ProductCategory,		
		SUM(sod.LineTotal) as TotalSales,
		soh.TerritoryID as Territory,
		st.Name as TerritoryName,
		soh.OrderDate as OrderDate
	FROM [Production].[Product] as p
	INNER JOIN [Production].[ProductSubcategory] ps ON p.ProductSubcategoryID = ps.ProductSubcategoryID
	INNER JOIN [Production].[ProductCategory] pc ON ps.ProductCategoryID = pc.ProductCategoryID
	INNER JOIN [Sales].[SalesOrderDetail] sod ON p.ProductID = sod.ProductID
	INNER JOIN [Sales].[SalesOrderHeader] soh ON sod.SalesOrderID = soh.SalesOrderID
	INNER JOIN [Sales].[SalesTerritory] st ON soh.TerritoryID = st.TerritoryID
	WHERE
		(@startDate IS NULL OR OrderDate >= @startDate)
	AND
		(@endDate IS NULL OR OrderDate <= @endDate)

	GROUP BY p.Name, pc.Name, soh.TerritoryID, st.Name, soh.OrderDate
),
TotalSalesByCategoryAndRegion AS (
	SELECT
		ProductName,
		ProductCategory,
		TotalSales,
		Territory,
		TerritoryName,
		OrderDate,
		SUM(TotalSales) OVER (PARTITION BY ProductCategory, Territory) as TotalSalesByCategoryAndRegion,
		SUM(TotalSales) OVER (PARTITION BY Territory) as TotalSalesByRegion
	FROM Sales_CTE
),
PercentageOfContribution AS (
	SELECT
		ProductName,
		ProductCategory,
		TotalSales,
		Territory,
		TerritoryName,
		OrderDate,
		(TotalSales / TotalSalesByCategoryAndRegion) * 100 as PercentOfTotalCategorySalesInRegion,
		(TotalSales / TotalSalesByRegion) * 100 as PercentOfTotalSalesInRegion
	FROM TotalSalesByCategoryAndRegion
)SELECT	
    ProductName,
    ProductCategory,    
    TotalSales,
    PercentOfTotalCategorySalesInRegion,
	PercentOfTotalSalesInRegion,
	Territory,
	TerritoryName,
	OrderDate
FROM PercentageOfContribution
WHERE 
	(@productCategory IS NULL OR ProductCategory = @productCategory)
AND
	(@territory IS NULL OR TerritoryName = @territory)
GO

--Filtro de ProductCategory, top de ventas, SalesByTerritory
exec Sales_CTE @productCategory = 'Bikes'
exec Sales_CTE @territory = 'Northwest', @startDate = '2011-05-31', @endDate = '2011-05-31'