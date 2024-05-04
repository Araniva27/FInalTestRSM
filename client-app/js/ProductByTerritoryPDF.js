$(document).ready(function() {
    $('.hideDropdown').hide(); 
    $('.hiddenTable').hide(); 
});

let currentPage = 1; 

const getSalesReportData = (pageNumber = 1) =>{

    const productCategoryFilter = $('#cmbCategories').val();
    const territoryFilter = $('#cmbTerritories').val();
    const startDateFilter = $('#txtStartDate').val();
    const endDateFilter = $('#txtEndDate').val();
    let pageSize = 50;
    var url = "http://localhost:5248/api/SalesByRegionReport/SalesByRegionReport?" ;

    const filter = [];    
    if(productCategoryFilter && productCategoryFilter != "0"){
        filter.push(`productCategory=${encodeURIComponent(productCategoryFilter)}`);
    } 

    if(territoryFilter && territoryFilter != "0"){
        filter.push(`territory=${encodeURIComponent(territoryFilter)}`);
    }

    if(startDateFilter){        
        filter.push(`startDate=${encodeURIComponent(startDateFilter)}`);
    }

    if(endDateFilter){        
        filter.push(`endDate=${encodeURIComponent(endDateFilter)}`);
    }

    filter.push(`pageNumber=${pageNumber}`);
    filter.push(`pageSize=${pageSize}`);

    url += filter.join("&");    
    $.ajax({        
        url : url,            
        type : 'GET',        
        dataType : 'json',
            
        success : function(data) {      
            $('.hideDropdown').show();
            $('.hiddenTable').show(); 
            $('#tbody-productByRegionReport').empty();
            $('#lblPageNumber').text(`Page Number: ${currentPage}`);
            data.forEach(function(row) {
                $('#tbody-productByRegionReport').append(`
                    <tr>
                        <td>${row.productName}</td>
                        <td>${row.productCategory}</td>
                        <td>${row.totalSales}</td>
                        <td>${row.percentOfTotalCategorySalesInRegion}</td>                        
                        <td>${row.percentOfTotalSalesInRegion}</td>
                        <td>${row.territoryName}</td>
                        <td>${row.orderDate}</td>
                    </tr>
                `);
            });

            //generatePDF();
        },

        error : function(xhr, status) {
            alert('Ha ocurrido un error al obtener la información');       
        },       
    });
}

const generatePDF = () => {    
    const doc = new jspdf.jsPDF();

    // Título del informe
    doc.text("Sales by Region Report", 10, 10);
    

    // Datos de la tabla
    const rows = [];
    $('#tbody-productByRegionReport tr').each(function() {
        const row = [];
        $(this).find('td').each(function() {
            row.push($(this).text());
        });
        rows.push(row);
    });

    // Construir la tabla en el PDF
    doc.autoTable({
        head: [['Product Name', 'Product Category', 'Total Sales ($)', 'Percent of Total Category Sales in Region', 'Percent of Total Sales in Region', 'Territory', 'Order Date']],
        body: rows
    });

    doc.save("productByRegionReport.pdf");
}

const nextPage = () => {
    currentPage++;
    getSalesReportData(currentPage);
}

const previousPage = () => {
    if(currentPage > 1){
        currentPage--;
        getSalesReportData(currentPage);
    }        
}


// document.querySelector("#btnGenerateProductByRegionReport").addEventListener("click",getSalesReportData);
document.querySelector("#btnPdf").addEventListener("click",generatePDF);