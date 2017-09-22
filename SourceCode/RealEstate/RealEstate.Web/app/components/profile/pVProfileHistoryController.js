(function (app) {
    app.controller('PVProfileHistoryController', PVProfileHistoryController);

    PVProfileHistoryController.$inject = ['$scope', 'BaseService', 'apiService', '$rootScope', '$window', '$timeout', 'blockUI', '$modal', '$log', 'authData', 'authenticationService', 'loginService', '$filter'];

    function PVProfileHistoryController($scope, BaseService, apiService, $rootScope, $window, $timeout, blockUI, $modal, $log, authData, authenticationService, loginService, $filter) {
        $scope.data = {
            lstRoomByUser: [],
            lstHistoryLogin: []
        }
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.totalCount = 0;
        angular.element(document).ready(function () {
        });
        $scope.changePassword = {};
        $scope.$on('fireLoadProfileHistoryEvent', function (event, userInfo) {
            $scope.getAllRoomByUserId = function () {
                var myBlockUI = blockUI.instances.get('BlockUIProfle');
                myBlockUI.start();
                var config = {
                    params: {
                        userID: userInfo.id,
                        page: 1,
                        pageSize: 5,
                        keyword: ""
                    }
                }
                apiService.get('api/room/getallroombyuser', config, function (respone) {
                    $scope.data.lstRoomByUser = respone.data.items;
                    $scope.page = respone.data.page;
                    $scope.pagesCount = respone.data.totalPages;
                    $scope.totalCount = respone.data.totalCount;
                    console.log($scope.data.lstRoomByUser);
                    myBlockUI.stop();
                }, function (respone) {
                    myBlockUI.stop();
                    BaseService.displayError("Không lấy được dữ liệu phòng", 3000);
                });
            }
            $scope.getAllHistoryLogin = function () {
                var myBlockUI = blockUI.instances.get('BlockUIProfle');
                myBlockUI.start();
                var config = {
                    params: {
                        userID: userInfo.id,
                        page: 1,
                        pageSize: 5,
                    }
                }
                apiService.get('api/management/getallhistorylogin', config, function (respone) {
                    $scope.data.lstHistoryLogin = respone.data.items;
                    console.log($scope.data.lstHistoryLogin);
                    myBlockUI.stop();
                }, function (respone) {
                    myBlockUI.stop();
                    BaseService.displayError("Không lấy được dữ liệu phòng", 3000);
                });
            }
            $scope.load = function () {
                $scope.getAllRoomByUserId();
                $scope.getAllHistoryLogin();
            };
            $scope.load();
        });
    }
})(angular.module('myApp'));