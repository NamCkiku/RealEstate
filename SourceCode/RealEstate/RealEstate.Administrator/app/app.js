var app;
(function () {
    app = angular.module("myApp", ['ui.router', 'kendo.directives', 'ui.bootstrap', 'ngAnimate']);
    app.run(['$rootScope', function ($rootScope) {
        var baseUrl = $('baseurl').attr('value');
        $rootScope.baseUrl = baseUrl;
        $rootScope.RootScopeDateFormat = 'dd/MM/yyyy';
    }]);
    app.constant('ENUMS',
    {
        ModalType: {
            Error: 1,
            Warning: 2,
            Confirmation: 3,
            Information: 4
        },
    });
})();