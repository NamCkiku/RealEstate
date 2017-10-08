var app;
(function () {
    app = angular.module("myApp", ['kendo.directives', 'ui.bootstrap', 'ngAnimate'
        , 'blockUI', 'LocalStorageModule', 'ui.select2', 'rzModule', 'ui.utils.masks', 'ngtimeago', 'ngTagsInput', 'ngSanitize', 'checklist-model', 'angularUtils.directives.dirPagination','vcRecaptcha']);
    app.run(['$rootScope', function ($rootScope) {
        var baseUrl = 'http://localhost:22034/';
        $rootScope.baseUrl = baseUrl;
        $rootScope.RootScopeDateFormat = 'dd/MM/yyyy';
    }]);
    app.config(function(vcRecaptchaServiceProvider){
      vcRecaptchaServiceProvider.setDefaults({
          key: '6LfnmTMUAAAAAJ1RV4Lk5FWmiSzzyWGDbd2nMbxL',
        theme: 'light',
        size: 'normal',
        type: 'image',
        lang: 'vi'
      });
    });
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
})();