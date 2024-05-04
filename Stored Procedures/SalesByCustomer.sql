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