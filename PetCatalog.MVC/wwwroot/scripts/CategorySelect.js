$(function () {
    $("#cata-select").change(function () {
        $(".cate-select-row").css("visibility", "collapse");
        let id = $("#cata-select option:selected").val();
        if (id == -1) {
            $(".cate-select-row").css("visibility", "visible");
        }
        else {
            let trName = `#tr-id-${id}`;
            $(trName).css("visibility", "visible");
        }     
    });
});