$(document).ready(function() {
    $('.hideDropdown').hide(); 
    $('.hiddenTable').hide(); 
});

let currentPage = 1; 

function generatePDF(){
    
    const data = {
        productCategory: document.getElementById("cmbCategories").value,
        startDate: document.getElementById("txtStartDate").value,
        endDate: document.getElementById("txtEndDate").value,
    };

    const doc = new jspdf.jsPDF();

    // Título del informe
    doc.text("Sales Report", 10, 10);

    // Información de filtro
    // doc.text(`Product Category: ${data.productCategory}`, 10, 20);
    // doc.text(`Start date: ${data.startDate}`, 10, 30);
    // doc.text(`End date: ${data.endDate}`, 10, 40);

    // Datos de la tabla
    const rows = [];
    $('#tbody-salesReport tr').each(function() {
        const row = [];
        $(this).find('td').each(function() {
            row.push($(this).text());
        });
        rows.push(row);
    });

    // Construir la tabla en el PDF
    doc.autoTable({
        head: [['Product Name', 'Unit Price ($)', 'Product Quantity', 'Product Category', 'Order Date', 'Total ($)']],
        body: rows
    });

    doc.save("salesReport.pdf");

}

function getSalesReportData (pageNumber = 1){

    const productCategoryFilter = $('#cmbCategories').val();
    const startDateFilter = $('#txtStartDate').val();
    const endDateFilter = $('#txtEndDate').val();
    let pageSize = 50;
    
    var url = "http://localhost:5248/api/ReportSales/SalesReport?" ;

    const filter = [];    
    if(productCategoryFilter && productCategoryFilter != "0"){
        filter.push(`productCategory=${encodeURIComponent(productCategoryFilter)}`);
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
    console.log(url);
    $.ajax({        
        url : url,            
        type : 'GET',        
        dataType : 'json',
            
        success : function(data) {      
            $('.hideDropdown').show();
            $('.hiddenTable').show(); 
            $('#tbody-salesReport').empty();                       
            $('#lblPageNumber').text(`Page Number: ${currentPage}`);
            data.forEach(function(row) {
                $('#tbody-salesReport').append(`
                    <tr>
                        <td>${row.productName}</td>
                        <td>${row.unitPrice}</td>
                        <td>${row.productQuantity}</td>
                        <td>${row.productCategory}</td>
                        <td>${row.orderDate}</td>
                        <td>${row.total}</td>
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





document.querySelector("#btnPdf").addEventListener("click",generatePDF);
//document.querySelector("#btnGenerateSalesReport").addEventListener("click",getSalesReportData());

