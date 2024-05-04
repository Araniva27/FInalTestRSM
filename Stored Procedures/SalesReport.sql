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