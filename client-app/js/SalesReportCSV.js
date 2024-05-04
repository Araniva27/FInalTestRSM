const generateCSV = () => {

    const headers = [
        'Product Name', 
        'Unit Price ($)', 
        'Product Quantity', 
        'Product Category', 
        'Order Date', 
        'Total ($)'
    ];
    $('#tbody-salesReport tr:first-child th').each(function() {
        headers.push($(this).text());
    });

    const data = [];
    //data.push(headers);

    $('#tbody-salesReport tr').each(function() {
        const row = [];
        $(this).find('td').each(function() {
            row.push($(this).text());
        });
        data.push(row);
    });
    const csvContent = [headers, ...data];

    const csv = Papa.unparse(csvContent);

    const blob = new Blob([csv], { type: 'text/csv;charset=utf-8' });
    saveAs(blob, 'salesReport.csv');
}

document.querySelector("#btnCSV").addEventListener("click",generateCSV);