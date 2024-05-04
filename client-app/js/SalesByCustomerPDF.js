$(document).ready(function() {
    $('.hideDropdown').hide(); 
    $('.hiddenTable').hide(); 
});

let currentPage = 1;

const getSalesByCustomerReportData = (pageNumber = 1) =>{

    const productName = $('#txtProductName').val();
    const customerName = $('#txtCustomerName').val();
    const startDateFilter = $('#txtStartDate').val();
    const endDateFilter = $('#txtEndDate').val();
    let pageSize = 50;

    var url = "http://localhost:5248/api/SalesByCustomerReport/SalesByCustomer?" ;

    const filter = [];    
    if(productName){
        filter.push(`productName=${encodeURIComponent(productName)}`);
    } 

    if(customerName){
        filter.push(`customerName=${encodeURIComponent(customerName)}`);
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
            $('#tbody-salesByCustomerReport').empty();
            $('#lblPageNumber').text(`Page Number: ${currentPage}`);
            data.forEach(function(row) {
                $('#tbody-salesByCustomerReport').append(`
                    <tr>
                        <td>${row.customerName}</td>
                        <td>${row.categoryName}</td>
                        <td>${row.productName}</td>
                        <td>${row.sales}</td>                        
                        <td>${row.unitPrice}</td>
                        <td>${row.totalSales}</td>                        
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
    doc.text("Sales by Customer Report", 10, 10);
    

    // Datos de la tabla
    const rows = [];
    $('#tbody-salesByCustomerReport tr').each(function() {
        const row = [];
        $(this).find('td').each(function() {
            row.push($(this).text());
        });
        rows.push(row);
    });

    // Construir la tabla en el PDF
    doc.autoTable({
        head: [['Customer Name', 'Category Name', 'Product Name', 'Sales quantity', 'Unit Price', 'Total Sales']],
        body: rows
    });

    doc.save("salesByCustomerReport.pdf");
}
const nextPage = () => {
    currentPage++;
    getSalesByCustomerReportData(currentPage);
}

const previousPage = () => {
    if(currentPage > 1){
        currentPage--;
        getSalesByCustomerReportData(currentPage);
    }        
}
document.querySelector("#btnPdf").addEventListener("click",generatePDF);