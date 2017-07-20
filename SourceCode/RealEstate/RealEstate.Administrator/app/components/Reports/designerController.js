'use strict';

/**
 * @ngdoc function
 * @name angularDemoApp.controller:ViewerCtrl
 * @description
 * # ViewerCtrl
 * Controller of the angularDemoApp
 */

(function (app) {
    app.controller('designerController', designerController);

    designerController.$inject = ['$scope', '$modal', 'BaseService', 'apiService', '$rootScope', '$window', '$timeout', 'ENUMS', 'blockUI'];

    function designerController($scope, $modal, BaseService, apiService, $rootScope, $window, $timeout, ENUMS, blockUI) {

        $scope.init = function () {
            console.log('Loading Designer view');

            console.log('Set full screen mode for the designer');
            var options = new $window.Stimulsoft.Designer.StiDesignerOptions();
            options.appearance.scrollbarsMode = true;
            options.appearance.fullScreenMode = true;
            console.log('Create the report designer with specified options');
            var designer = new $window.Stimulsoft.Designer.StiDesigner(options, 'StiDesigner', false);

            console.log('Create a new report instance');
            var report = new $window.Stimulsoft.Report.StiReport();

            console.log('Load report from url');
            report.loadFile($rootScope.baseUrl + 'ReportTemplates/SimpleList.mrt');

            console.log('Edit report template in the designer');
            designer.report = report;

            console.log('Rendering the viewer to selected element');
            designer.renderHtml('designer');

            console.log('Loading completed successfully!');
        };
        $scope.init();
    }
})(angular.module('myApp'));