(function (app) {
    app.controller('RoomDetailController', RoomDetailController);

    RoomDetailController.$inject = ['$scope', 'BaseService', 'apiService', '$rootScope', '$window', '$timeout', 'blockUI', '$modal', '$log', 'authData', 'authenticationService', 'loginService', '$filter'];

    function RoomDetailController($scope, BaseService, apiService, $rootScope, $window, $timeout, blockUI, $modal, $log, authData, authenticationService, loginService, $filter) {
        $scope.baseUrl = $rootScope.baseUrl;
        $scope.init = function () {
            $scope.getRoomById();
        }
        $scope.roomObj = {

        }
        $scope.getRoomById = function () {
            var myBlockUI = blockUI.instances.get('BlockUIRoomDetail');
            myBlockUI.start();
            var config = {
                params: {
                    roomId: $('#txtRoomId').val(),
                }
            }
            apiService.get('api/room/getroombyid', config, function (respone) {
                $scope.roomObj = respone.data;
                console.log($scope.roomObj);
                $scope.roomObj.convenient = JSON.parse(respone.data.convenient);
                $scope.roomObj.moreImages = JSON.parse(respone.data.moreImages);
                myBlockUI.stop();
            }, function (respone) {
                myBlockUI.stop();
                BaseService.displayError("Không lấy được dữ liệu phòng", 3000);
            });
        }


        $scope.init();
    }
})(angular.module('myApp'));