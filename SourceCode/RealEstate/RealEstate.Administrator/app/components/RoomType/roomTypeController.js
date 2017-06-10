(function (app) {
    app.controller('roomTypeController', roomTypeController);

    roomTypeController.$inject = ['$scope', '$modal', 'BaseService', 'apiService', '$rootScope', '$window', '$timeout', 'ENUMS'];

    function roomTypeController($scope, $modal, BaseService, apiService, $rootScope, $window, $timeout, ENUMS) {
        $scope.enums = ENUMS;
        $scope.date = new Date();
    }
})(angular.module('myApp'));