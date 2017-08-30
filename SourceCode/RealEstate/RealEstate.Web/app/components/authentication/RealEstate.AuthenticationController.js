(function (app) {
    app.controller('AuthenticationController', AuthenticationController);

    AuthenticationController.$inject = ['$scope', 'BaseService', 'apiService', '$rootScope', '$window', '$timeout', 'blockUI', '$modal', '$log', 'authData', 'authenticationService', 'loginService'];

    function AuthenticationController($scope, BaseService, apiService, $rootScope, $window, $timeout, blockUI, $modal, $log, authData, authenticationService, loginService) {
        $scope.account = {

        }
        $scope.data = {
            lstRoomType:[]
        }
        $scope.init = function () {
            authenticationService.init();
            $scope.userInfo = authData.authenticationData;
            $rootScope.userInfomation = authData.authenticationData;
            if (!$scope.userInfo.IsAuthenticated) {
                //$window.location.href = "/";
            }
            $scope.GetAllRoomType();
        };
       

        //Hàm lấy ra commbobox loại phòng
        $scope.GetAllRoomType = function () {
            apiService.get('api/roomtype/getallroomtype', null, function (respone) {
                $scope.data.lstRoomType = respone.data;
            }, function (respone) {
                BaseService.displayError("Không lấy được dữ liệu Loại Phòng", 3000);
            });
        }

        $scope.logOut = function () {
            loginService.logOut();
        }
        $scope.showPopupLogin = function () {
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

            modalInstance.result.then(function (response) {
            }, function () {
                $log.info('Modal dismissed at: ' + new Date());
            });
        }
        $scope.showPopupRegister = function () {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'ModalRegister.html',
                controller: 'ModalRegisterController',
                size: 'sm',
                backdrop: 'static',
                keyboard: false,
                resolve: {
                    items: function () {
                        return $scope.account;
                    }
                }
            });

            modalInstance.result.then(function (response) {
            }, function () {
                $log.info('Modal dismissed at: ' + new Date());
            });
        }

        $scope.init();
    }
})(angular.module('myApp'));