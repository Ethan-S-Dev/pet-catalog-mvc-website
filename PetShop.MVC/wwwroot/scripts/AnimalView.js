function openDesc(id) {
        $(".page-shadow-layer").css("visibility", "visible");
        let divName = `#desc-animal-${id}`;
        $(divName).css("visibility", "visible")
    }

function closeDesc(id) {
    $(".page-shadow-layer").css("visibility", "collapse");
    let divName = `#desc-animal-${id}`;
    $(divName).css("visibility", "collapse")
}