(function (app) {
    app.controller('PVProfileHistoryController', PVProfileHistoryController);

    PVProfileHistoryController.$inject = ['$scope', 'BaseService', 'apiService', '$rootScope', '$window', '$timeout', 'blockUI', '$modal', '$log', 'authData', 'authenticationService', 'loginService', '$filter'];

    function PVProfileHistoryController($scope, BaseService, apiService, $rootScope, $window, $timeout, blockUI, $modal, $log, authData, authenticationService, loginService, $filter) {
        $scope.data = {
            lstRoomByUser: [],
            lstHistoryLogin: []
        }
        $scope.roomCurrentPage = 1;
        $scope.historyCurrentPage = 1;
        $scope.pageSize = 5;
        $scope.totalCount = 0;
        $scope.totalHistoryCount = 0;
        angular.element(document).ready(function () {
        });
        $scope.changePassword = {};
        $scope.$on('fireLoadProfileHistoryEvent', function (event, userInfo) {
            $scope.getAllRoomByUserId = function (pageNumber) {
                var myBlockUI = blockUI.instances.get('BlockUIProfle');
                myBlockUI.start();
                pageNumber = pageNumber || 1;
                var config = {
                    params: {
                        userID: userInfo.id,
                        page: pageNumber,
                        pageSize: $scope.pageSize,
                        keyword: ""
                    }
                }
                apiService.get('api/room/getallroombyuser', config, function (respone) {
                    $scope.data.lstRoomByUser = respone.data.items;
                    $scope.totalCount = respone.data.totalCount;
                    myBlockUI.stop();
                }, function (respone) {
                    myBlockUI.stop();
                    BaseService.displayError("Không lấy được dữ liệu phòng", 3000);
                });
            }
            $scope.getAllHistoryLogin = function (pageNumber) {
                var myBlockUI = blockUI.instances.get('BlockUIProfle');
                myBlockUI.start();
                pageNumber = pageNumber || 1;
                var config = {
                    params: {
                        userID: userInfo.id,
                        page: pageNumber,
                        pageSize: $scope.pageSize,
                    }
                }
                apiService.get('api/management/getallhistorylogin', config, function (respone) {
                    $scope.data.lstHistoryLogin = respone.data.items;
                    $scope.totalHistoryCount = respone.data.totalCount;
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

        $scope.pageRoomByUserChanged = function (newPage) {
            $scope.getAllRoomByUserId(newPage);
        };

        $scope.pageHistoryLoginChanged = function (newPage) {
            $scope.getAllHistoryLogin(newPage);
        };
    }
})(angular.module('myApp'));