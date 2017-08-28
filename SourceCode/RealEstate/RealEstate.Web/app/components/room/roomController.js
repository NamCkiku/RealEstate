(function (app) {
    app.controller('RoomController', RoomController);

    RoomController.$inject = ['$scope', 'BaseService', 'apiService', '$rootScope', '$window', '$timeout', 'blockUI', '$modal', '$log', 'authData', 'authenticationService', 'loginService', '$filter'];

    function RoomController($scope, BaseService, apiService, $rootScope, $window, $timeout, blockUI, $modal, $log, authData, authenticationService, loginService, $filter) {
        $scope.data = {
            lstRoomHot: []
        }
        $scope.baseUrl = $rootScope.baseUrl;
        $scope.init = function () {
            $scope.getAllRoomHot();
        }
        $scope.getAllRoomHot = function () {
            var myBlockUI = blockUI.instances.get('ListRoomBlockUI');
            myBlockUI.start();
            apiService.get('api/room/getallroom', null, function (respone) {
                $scope.data.lstRoomHot = respone.data;
                console.log($scope.data.lstRoomHot);
                myBlockUI.stop();
            }, function (respone) {
                myBlockUI.stop();
                BaseService.displayError("Không lấy được dữ liệu phòng", 3000);
            });
        }
        $scope.init();
    }
})(angular.module('myApp'));