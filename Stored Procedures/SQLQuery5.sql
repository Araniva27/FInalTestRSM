CREATE PROCEDURE SalesByCustomer (
    @FechaInicio DATETIME = NULL,
    @FechaFin DATETIME = NULL,
    @Customer VARCHAR(255) = NULL,
    @Product VARCHAR(255) = NULL
)
AS
BEGIN
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
            (@FechaInicio IS NULL OR so.OrderDate >= @FechaInicio)
            AND (@FechaFin IS NULL OR so.OrderDate <= @FechaFin)
            AND (@Customer IS NULL OR c.FirstName = @Customer)
            AND (@Product IS NULL OR P.Name = @Product)
    )

    SELECT
        CustomerName,
        CategoryName,
        ProductName,
        COUNT(SalesOrderID) AS Sales,
        UnitPrice,
        SUM(UnitPrice * OrderQty) AS TotalSales
    FROM
        VentasCTE
    GROUP BY
      CustomerName, CategoryName, ProductName, UnitPrice;
END;

exec SalesByCustomer