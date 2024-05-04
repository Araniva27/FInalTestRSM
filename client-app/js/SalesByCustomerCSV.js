const generateCSV = () => {

    const headers = [
        'Customer Name', 
        'Category Name', 
        'Product Name', 
        'Sales', 
        'Unit Price', 
        'Total Sales',         
    ];
    $('#tbody-salesByCustomerReport tr:first-child th').each(function() {
        headers.push($(this).text());
    });

    const data = [];
    //data.push(headers);

    $('#tbody-salesByCustomerReport tr').each(function() {
        const row = [];
        $(this).find('td').each(function() {
            row.push($(this).text());
        });
        data.push(row);
    });
    const csvContent = [headers, ...data];

    const csv = Papa.unparse(csvContent);

    const blob = new Blob([csv], { type: 'text/csv;charset=utf-8' });
    saveAs(blob, 'salesByCustomerReport.csv');
}
document.querySelector("#btnCSV").addEventListener("click",generateCSV);