const generateCSV = () => {

    const headers = [
        'Product Name', 
        'Product Category', 
        'Total Sales ($)', 
        'Percent of Total Category Sales in Region', 
        'Percent of Total Sales in Region', 
        'Territory', 
        'Order Date'
    ];
    $('#tbody-productByRegionReport tr:first-child th').each(function() {
        headers.push($(this).text());
    });

    const data = [];
    //data.push(headers);

    $('#tbody-productByRegionReport tr').each(function() {
        const row = [];
        $(this).find('td').each(function() {
            row.push($(this).text());
        });
        data.push(row);
    });
    const csvContent = [headers, ...data];

    const csv = Papa.unparse(csvContent);

    const blob = new Blob([csv], { type: 'text/csv;charset=utf-8' });
    saveAs(blob, 'salesByRegionReport.csv');
}

document.querySelector("#btnCSV").addEventListener("click",generateCSV);