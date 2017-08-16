$(document).ready(function () {
    $.slidebars();
    $("#owl-search").owlCarousel({
        pagination: false,
        paginationNumbers: true,
        autoPlay: 6000,
        slideSpeed: 300,
        paginationSpeed: 400,
        items: 1, //10 items above 1000px browser width
        itemsDesktop: [1200, 1], //5 items between 1000px and 901px
        itemsDesktopSmall: [991, 1], // betweem 900px and 601px
        itemsTablet: [767, 1], //2 items between 600 and 0
        itemsMobile: [640, 1] // itemsMobile disabled - inherit from itemsTablet option
    });
    $("#owl-brand").owlCarousel({
        navigation: false,
        items: 6,
        slideSpeed: 200,
        paginationSpeed: 800,
        rewindSpeed: 1000,
        pagination: false,
        autoPlay: 6000,
        itemsCustom: [[480, 2], [320, 2], [768, 3], [767, 3], [991, 4], [1200, 6]],
        responsive: true,
        navigationText: false
    });
    $("#owl-demo-people-say").owlCarousel({
        pagination: true,
        paginationNumbers: false,
        autoPlay: 6000,
        slideSpeed: 300,
        paginationSpeed: 400,
        items: 2, //10 items above 1000px browser width
        itemsDesktop: [1200, 2], //5 items between 1000px and 901px
        itemsDesktopSmall: [991, 2], // betweem 900px and 601px
        itemsTablet: [767, 2], //2 items between 600 and 0
        itemsMobile: [640, 1] // itemsMobile disabled - inherit from itemsTablet option
    });
});

function showGrid() {
    $('.listing_wrapper').addClass('col-md-4 col-lg-4');
    $('.listing_wrapper').removeClass('col-md-12 col-lg-12');
    $('#grid_view').addClass('icon_selected');
    $('#list_view').removeClass('icon_selected');
}
function showList() {
    $('.listing_wrapper').removeClass('col-md-4 col-lg-4');
    $('.listing_wrapper').addClass('col-md-12 col-lg-12');
    $('#list_view').addClass('icon_selected');
    $('#grid_view').removeClass('icon_selected');
}