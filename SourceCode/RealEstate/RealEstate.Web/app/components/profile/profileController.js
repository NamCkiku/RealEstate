(function (app) {
    app.controller('ProfileController', ProfileController);

    ProfileController.$inject = ['$scope', 'BaseService', 'apiService', '$rootScope', '$window', '$timeout', 'blockUI', '$modal', '$log', 'authData', 'authenticationService', 'loginService', '$filter'];

    function ProfileController($scope, BaseService, apiService, $rootScope, $window, $timeout, blockUI, $modal, $log, authData, authenticationService, loginService, $filter) {
        $scope.userInfo = {

        }

        angular.element(document).ready(function () {
            $scope.init();
        });
        $scope.data = {
            MainTab: 1,
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
                        userID: user.userID
                    }
                }
                apiService.get('api/management/getuserbyid', config, function (respone) {
                    $scope.userInfo = respone.data;
                    console.log($scope.userInfo);
                    $scope.fireLoadProfileHistoryEvent();
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

        $scope.init = function () {
            $scope.GetAllUserInfo();
        };


         $scope.showPopupPayment = function () {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'ModalPayment.html',
                controller: 'ModalPaymentController',
                size: 'md',
                backdrop: 'static',
                keyboard: false,
                resolve: {
                    items: function () {
                        return $scope.userInfo;
                    }
                }
            });

            modalInstance.result.then(function (response) {
            }, function () {
                $log.info('Modal dismissed at: ' + new Date());
            });
        }


        $scope.fireLoadProfileHistoryEvent = function () {
            $scope.$broadcast('fireLoadProfileHistoryEvent', $scope.userInfo);
        };
        $scope.fireLoadListRoomByUserEvent = function () {
            $scope.changeTab(2);
            $scope.$broadcast('fireLoadListRoomByUserEvent', $scope.userInfo);
        };
    }
})(angular.module('myApp'));