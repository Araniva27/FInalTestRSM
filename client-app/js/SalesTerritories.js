$(document).ready(function() {
    getAllTerritories();
});

const getAllTerritories = () => {
    $.ajax({
        // 
        url : `http://localhost:5248/api/SalesTerritories/AllTerritories`,            
        type : 'GET',        
        dataType : 'json',
            
        success : function(data) {
        // 
        $('#cmbTerritories').empty();
            
            // 
            $('#cmbTerritories').append(`<option value="0">All territories</option>`);
            $.each(data, function(index, category) {
                $('#cmbTerritories').append(`<option value="${category.name}">${category.name}</option>`);
            });     
        },

        error : function(xhr, status) {
            alert('Ha ocurrido un error al obtener la información');       
        },       
    });
}
