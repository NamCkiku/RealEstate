(function (app) {
    app.controller('ProfileController', ProfileController);

    ProfileController.$inject = ['$scope', 'BaseService', 'apiService', '$rootScope', '$window', '$timeout', 'blockUI', '$modal', '$log', 'authData', 'authenticationService', 'loginService', '$filter'];

    function ProfileController($scope, BaseService, apiService, $rootScope, $window, $timeout, blockUI, $modal, $log, authData, authenticationService, loginService, $filter) {
        $scope.userInfo = {

        }
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.totalCount = 0;
        angular.element(document).ready(function () {
            $scope.init();
        });
        $scope.data = {
            MainTab: 1,
            lstRoomByUser: [],
            lstHistoryLogin: []
        };
        $scope.changeTab = function (tabIndex) {
            return $scope.data.MainTab = tabIndex;
        }

        $scope.GetAllUserInfo = function GetAllUserInfo() {
            var user = $rootScope.userInfomation;
            if (user.IsAuthenticated) {
                var myBlockUI = blockUI.instances.get('BlockUIProfle');
                myBlockUI.start();
                var config = {
                    params: {
                        id: user.userID
                    }
                }
                apiService.get('api/account/user', config, function (respone) {
                    $scope.userInfo = respone.data;
                    console.log($scope.userInfo);
                    myBlockUI.stop();
                }, function (respone) {
                    myBlockUI.stop();
                    BaseService.displayError("Không lấy được dữ liệu người dùng", 3000);
                });
            }
            else {
                var modalInstance = $modal.open({
                    animation: true,
                    templateUrl: 'ModalLogin.html',
                    controller: 'ModalLoginController',
                    size: 'sm',
                    backdrop: 'static',
                    keyboard: false,
                    resolve: {
                        items: function () {
                            return $scope.account;
                        }
                    }
                });
            }
        }

        $scope.getAllRoomByUserId = function () {
            var myBlockUI = blockUI.instances.get('BlockUIProfle');
            myBlockUI.start();
            var config = {
                params: {
                    userID: $rootScope.userInfomation.userID,
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
                    userID: $rootScope.userInfomation.userID,
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

        $scope.init = function () {
            $scope.GetAllUserInfo();
            $scope.getAllRoomByUserId();
            $scope.getAllHistoryLogin();
        };

        $scope.fireLoadProfileInformationEvent = function () {
            $scope.changeTab(1);
            $scope.$broadcast('fireLoadProfileInformationEvent', $scope.userInfo);
        };
        $scope.fireLoadListRoomByUserEvent = function () {
            $scope.changeTab(2);
            $scope.$broadcast('fireLoadListRoomByUserEvent', $scope.userInfo);
        };
    }
})(angular.module('myApp'));