$(function () {
    $(".desc-li").click(function () {
        let id = $(this).attr("animal-id");
        $(".page-shadow-layer").css("visibility", "visible");
        let divName = `#desc-animal-${id}`;
        $(divName).css("visibility", "visible")
    })

    $(".comments-btn").click(function () {
        let id = $(this).attr("animal-id");
        $(".page-shadow-layer").css("visibility", "visible");
        let divName = `#comment-animal-${id}`;
        $(divName).css("visibility", "visible")
    })

    $(".animal-desc .animal-desc-header button").click(function () {
        let id = $(this).attr("animal-id");
        $(".page-shadow-layer").css("visibility", "collapse");
        let divName = `#desc-animal-${id}`;
        $(divName).css("visibility", "collapse")
    });

    $(".animal-comment .animal-comment-header button").click(function () {
        let id = $(this).attr("animal-id");
        $(".page-shadow-layer").css("visibility", "collapse");
        let divName = `#comment-animal-${id}`;
        $(divName).css("visibility", "collapse")
    });
});
