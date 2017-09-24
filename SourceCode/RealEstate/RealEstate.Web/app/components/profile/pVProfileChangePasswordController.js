(function (app) {
    app.controller('PVProfileChangePasswordController', PVProfileChangePasswordController);

    PVProfileChangePasswordController.$inject = ['$scope', 'BaseService', 'apiService', '$rootScope', '$window', '$timeout', 'blockUI', '$modal', '$log', 'authData', 'authenticationService', 'loginService', '$filter'];

    function PVProfileChangePasswordController($scope, BaseService, apiService, $rootScope, $window, $timeout, blockUI, $modal, $log, authData, authenticationService, loginService, $filter) {
        $scope.baseUrl = $rootScope.baseUrl;
        kendo.culture("vi-VN");
        angular.element(document).ready(function () {
        });
        $scope.userInfochangepass = {

        }
        $scope.userInfo = {};
        $scope.$on('fireLoadProfileChangePasswordEvent', function (event, userInfo) {
            $scope.userInfo = userInfo;
        });
        $scope.ChangePassword = function () {
            BaseService.ValidatorForm("#form-changepass");
            var formprofile = angular.element(document.querySelector('#form-changepass'));
            var formValidation = formprofile.data('formValidation').validate();
            if (formValidation.isValid()) {
                var myBlockUI = blockUI.instances.get('BlockUIChangePas');
                myBlockUI.start();
                apiService.post('api/account/changepassword', $scope.userInfochangepass, function (respone) {
                    console.log(respone);
                    if (respone.data.succeeded) {
                        BaseService.displaySuccess("Chúc mừng bạn đã đổi mật khẩu thành công", 5000);
                        loginService.logOut();
                        myBlockUI.stop();
                    } else {
                        $scope.message = 'Mật khẩu cũ không đúng';
                        myBlockUI.stop();
                    }
                }, function (respone) {
                    myBlockUI.stop();
                });
            }
            else {
                BaseService.displayError("Vui lòng nhập đầy đủ thông tin", 5000);
            }
        }
    }
})(angular.module('myApp'));