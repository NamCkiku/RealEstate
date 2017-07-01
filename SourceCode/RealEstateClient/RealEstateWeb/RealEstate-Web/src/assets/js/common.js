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