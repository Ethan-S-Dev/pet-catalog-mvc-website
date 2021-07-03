$(function() {
    $("#category-select").change(function() {
        if ($("#category-select option:selected").val() == -1) {
            $("#category-name").css("visibility", "visible");
            $("#category-name-filed").prop("readonly", false);
            $("#category-name-filed").val("");
        } else {
            $("#category-name").css("visibility", "collapse");
            $("#category-name-filed").prop("readonly", true);           
            var categoryName = $("#category-select option:selected").text();
            $("#category-name-filed").val(categoryName);            
        }        
    })
})