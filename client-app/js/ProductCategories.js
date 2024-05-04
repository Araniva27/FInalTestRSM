$(document).ready(function() {
    getAllCategories();
});

const getAllCategories = () => {
    $.ajax({
        // 
        url : `http://localhost:5248/api/ProductCategory/AllCategories`,            
        type : 'GET',        
        dataType : 'json',
            
        success : function(data) {
        // 
        $('#cmbCategories').empty();
            
            // 
            $('#cmbCategories').append(`<option value="0">All categories</option>`);
            $.each(data, function(index, category) {
                $('#cmbCategories').append(`<option value="${category.name}">${category.name}</option>`);
            });     
        },

        error : function(xhr, status) {
            alert('Ha ocurrido un error al obtener la información');       
        },       
    });
}

