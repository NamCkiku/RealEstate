var app;
(function () {
    app = angular.module("myApp", ['ui.router', 'kendo.directives', 'ui.bootstrap', 'ngAnimate', 'blockUI']);
    app.run(['$rootScope', function ($rootScope) {
        var baseUrl = $('baseurl').attr('value');
        $rootScope.baseUrl = baseUrl;
        $rootScope.RootScopeDateFormat = 'dd/MM/yyyy';
    }]);
    app.config(['blockUIConfig', function (blockUIConfig) {
        // Change the default overlay message
        blockUIConfig.message = '';
        blockUIConfig.autoInjectBodyBlock = false;
        // Change the default delay to 100ms before the blocking is visible
        blockUIConfig.delay = 100;
        blockUIConfig.css = {
            border: 'none',
            padding: '15px',
            backgroundColor: 'none',
            '-webkit-border-radius': '10px',
            '-moz-border-radius': '10px',
            opacity: .5,
            color: '#fff',
            baseZ: 9000
        }
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