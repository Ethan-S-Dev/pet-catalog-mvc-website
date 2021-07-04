$(function () {
    $(".desc-li").click(function () {
        let id = $(this).attr("animal-id");
        $(".page-shadow-layer").css("visibility", "visible");
        let divName = `#desc-animal-${id}`;
        $(divName).css("visibility", "visible")
    })

    $(".animal-desc .animal-desc-header button").click(function () {
        let id = $(this).attr("animal-id");
        $(".page-shadow-layer").css("visibility", "collapse");
        let divName = `#desc-animal-${id}`;
        $(divName).css("visibility", "collapse")
    });
});
