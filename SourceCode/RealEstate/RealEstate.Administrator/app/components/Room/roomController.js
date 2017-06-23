(function (app) {
    app.controller('roomController', roomController);

    roomController.$inject = ['$scope', '$modal', 'BaseService', 'apiService', '$rootScope', '$window', '$timeout', 'ENUMS', 'blockUI'];

    function roomController($scope, $modal, BaseService, apiService, $rootScope, $window, $timeout, ENUMS, blockUI) {
        $scope.enums = ENUMS;
        $scope.enums = ENUMS;
        $scope.pageSize = 10;
        $scope.filter = {
            Keywords: "",
            StartDate: "",
            EndDate: "",
            searchByStartDate: true,
            searchByEndDate: true,
            Status: true
        }
        $scope.data = {
            lstRoomType: [],
            lstProvince: [],
            lstDistrict: [],
            lstWard: [],
        }
    }
})(angular.module('myApp'));