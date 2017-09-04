(function (app) {
    'use strict';
    app.directive('owlCarousel', owlCarousel);
    owlCarousel.$inject = ['$rootScope'];
    function owlCarousel($rootScope) {
        return {
            restrict: 'E',
            transclude: false,
            link: function (scope) {
                scope.initCarousel = function (element) {
                    // provide any default options you want
                    var defaultOptions = {
                        autoplay: true,
                        autoplayTimeout: 2500,
                        loop: false, nav: true,
                        responsiveClass: true,
                        margin: 30,
                        responsive: {
                            0: { items: 1 },
                            767: { items: 3 },
                            992: { items: 4 }
                        }
                    };
                    var customOptions = scope.$eval($(element).attr('data-options'));
                    // combine the two options objects
                    for (var key in customOptions) {
                        defaultOptions[key] = customOptions[key];
                    }
                    // init carousel
                    $(element).owlCarousel(defaultOptions);
                };
            }
        };
    }

})(angular.module('myApp'));

(function (app) {
    'use strict';
    app.directive('owlCarouselItem', owlCarouselItem);
    owlCarouselItem.$inject = ['$rootScope'];
    function owlCarouselItem($rootScope) {
        return {
            restrict: 'A',
            transclude: false,
            link: function (scope, element) {
                // wait for the last item in the ng-repeat then call init
                if (scope.$last) {
                    scope.initCarousel(element.parent());
                }
            }
        };
    }

})(angular.module('myApp'));