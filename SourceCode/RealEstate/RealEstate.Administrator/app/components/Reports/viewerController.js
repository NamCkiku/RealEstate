'use strict';

/**
 * @ngdoc function
 * @name angularDemoApp.controller:ViewerCtrl
 * @description
 * # ViewerCtrl
 * Controller of the angularDemoApp
 */

(function (app) {
    app.controller('viewerController', viewerController);

    viewerController.$inject = ['$scope', '$modal', 'BaseService', 'apiService', '$rootScope', '$window', '$timeout', 'ENUMS', 'blockUI'];

    function viewerController($scope, $modal, BaseService, apiService, $rootScope, $window, $timeout, ENUMS, blockUI) {
        $scope.init = function () {
            // Set full screen mode for the viewer
            var options = new Stimulsoft.Viewer.StiViewerOptions();
            options.appearance.scrollbarsMode = true;
            var viewer = new $window.Stimulsoft.Viewer.StiViewer(options, 'StiViewer', false);
            var report = new $window.Stimulsoft.Report.StiReport();
            report.loadFile($rootScope.baseUrl + 'ReportTemplates/Report');
            viewer.report = report;
            viewer.renderHtml('viewer');
        };
        $scope.init();
    }
})(angular.module('myApp'));