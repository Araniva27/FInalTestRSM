# Final Test RSM by Manuel Araniva

To properly use the application and the API, it is necessary to follow the following recommendations:

1- Modify the port number in the JavaScript URLs in the following files:

  - SalesByCustomerPDF.js (line 16)
  - SalesReportPDF.js (line 53)
  - SalesTerritories.js (line 8)
  - ProductCategories.js (line 8)
  - ProductByTerritoryPDF.js (line 15)

2-Add the following stored procedures
```sql
CREATE PROCEDURE SalesReport
    @productCategory nvarchar(100) = null,
    @startDate date = null,
    @endDate date = null,
    @pageNumber int = 1,
    @pageSize int = 30
AS
BEGIN
    DECLARE @startRow int
    DECLARE @endRow int
    
    SET @startRow = (@pageNumber - 1) * @pageSize + 1
    SET @endRow = @pageNumber * @pageSize

    ;WITH TotalSalesForProducts_CTE AS (
        SELECT 
            ROW_NUMBER() OVER (ORDER BY soh.OrderDate) AS RowNum,
            p.ProductID AS ProductId,           
            p.Name AS ProductName,           
            sod.UnitPrice AS UnitPrice,
            pc.Name AS ProductCategory,
            soh.OrderDate AS OrderDate,
            SUM(sod.OrderQty) AS ProductQuantity,
            (sod.UnitPrice * SUM(sod.OrderQty)) AS Total
        FROM 
            [Production].[Product] p
        INNER JOIN [Sales].[SalesOrderDetail] sod ON p.ProductID = sod.ProductID
        INNER JOIN [Sales].[SalesOrderHeader] soh ON sod.SalesOrderID = soh.SalesOrderID        
        INNER JOIN [Production].[ProductSubcategory] psc ON psc.ProductSubcategoryID = p.ProductSubcategoryID
        INNER JOIN [Production].[ProductCategory] pc ON pc.ProductCategoryID = psc.ProductCategoryID
        WHERE
            (@productCategory IS NULL OR pc.Name = @productCategory)
        AND
            (@startDate IS NULL OR soh.OrderDate >= @startDate)
        AND
            (@endDate IS NULL OR soh.OrderDate <= @endDate)
        GROUP BY 
            p.ProductID, p.Name, sod.UnitPrice, pc.Name, soh.OrderDate
    )
    SELECT
        ProductId,
        ProductName,
        UnitPrice,
        ProductQuantity,
        ProductCategory,
        OrderDate,
        Total
    FROM
        TotalSalesForProducts_CTE
    WHERE
        RowNum BETWEEN @startRow AND @endRow
	ORDER BY OrderDate ASC
END
```
-------------------------------------------------------------------------------
CREATE PROCEDURE SalesByCustomer (
    @startDate date = NULL,
    @endDate date = NULL,
    @customer nvarchar(255) = NULL,
    @product nvarchar(255) = NULL,
    @PageNumber INT = 1,  
    @PageSize INT = 10 
)
AS
BEGIN
    SET NOCOUNT ON;

    WITH VentasCTE AS (
        SELECT
            so.SalesOrderID,
            so.OrderDate,
            so.CustomerID,
            c.FirstName AS CustomerName,
            sod.ProductID,
            p.Name AS ProductName,
            psc.ProductCategoryID,
            psc.Name AS CategoryName,
            sod.UnitPrice,
            sod.OrderQty,
            sod.UnitPrice * sod.OrderQty AS TotalPrice,
            sa.AddressLine1
        FROM
            Sales.SalesOrderHeader so
        INNER JOIN
            Sales.SalesOrderDetail sod ON so.SalesOrderID = sod.SalesOrderID
        INNER JOIN
            Production.Product p ON sod.ProductID = p.ProductID
        INNER JOIN
            Production.ProductSubcategory psc ON p.ProductSubcategoryID = psc.ProductSubcategoryID
        INNER JOIN
            Production.ProductCategory pc ON psc.ProductCategoryID = pc.ProductCategoryID
        LEFT JOIN
            Sales.SalesPerson sp ON so.SalesPersonID = sp.BusinessEntityID
        INNER JOIN
            Person.Person c ON so.CustomerID = c.BusinessEntityID
        INNER JOIN
            Person.Address sa ON so.BillToAddressID = sa.AddressID
        WHERE
            (@startDate IS NULL OR so.OrderDate >= @startDate)
            AND (@endDate IS NULL OR so.OrderDate <= @endDate)
            AND (@customer IS NULL OR c.FirstName LIKE '%'+ @customer + '%')
            AND (@product IS NULL OR p.Name LIKE '%' + @product + '%')
    ), SalesSummary AS (
        SELECT
            CustomerName,
            CategoryName,
            ProductName,
            COUNT(SalesOrderID) AS Sales,
            UnitPrice,
            SUM(UnitPrice * OrderQty) AS TotalSales,
            ROW_NUMBER() OVER (ORDER BY SUM(UnitPrice * OrderQty) DESC, CustomerName, CategoryName, ProductName) AS RowNumber
        FROM
            VentasCTE
        GROUP BY
            CustomerName, CategoryName, ProductName, UnitPrice 
    )
    SELECT *
    FROM SalesSummary
    WHERE RowNumber BETWEEN (@PageNumber - 1) * @PageSize + 1 AND @PageNumber * @PageSize;
END
-------------------------------------------------------------------------------
CREATE PROCEDURE Sales_CTE
	@productCategory nvarchar(100) = NULL,
	@startDate datetime = NULL,
	@endDate datetime = NULL,
	@territory nvarchar(50) = NULL,
	@PageNumber INT = 1,  
    @PageSize INT = 10
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
		(TotalSales / TotalSalesByRegion) * 100 as PercentOfTotalSalesInRegion,
		ROW_NUMBER() OVER (ORDER BY TotalSales DESC, ProductCategory, Territory) AS RowNum
	FROM TotalSalesByCategoryAndRegion
)
SELECT	
    ProductName,
    ProductCategory,    
    TotalSales,
    PercentOfTotalCategorySalesInRegion,
	PercentOfTotalSalesInRegion,
	Territory,
	TerritoryName,
	OrderDate
FROM PercentageOfContribution
WHERE RowNum BETWEEN (@PageNumber - 1) * @PageSize + 1 AND @PageNumber * @PageSize
AND (@productCategory IS NULL OR ProductCategory = @productCategory)
AND (@territory IS NULL OR TerritoryName = @territory)
-------------------------------------------------------------------------------
