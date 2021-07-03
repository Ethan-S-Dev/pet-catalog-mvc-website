$(function () {
    $("#cata-select").change(function () {
        $(".cate-select-row").css("visibility", "collapse");
        let id = $("#cata-select option:selected").val();
        let trName = `#tr-id-${id}`;
        $(trName).css("visibility", "visible");
    });
});