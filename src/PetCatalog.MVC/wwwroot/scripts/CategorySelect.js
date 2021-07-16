$(function () {
    $("#cata-select").change(function () {
        $(".cate-select-row").css("visibility","collapse");
        let id = $("#cata-select option:selected").val();
        let trId = `#tr-id-${id}`
        $(trId).css("visibility", "visible");
    });
});