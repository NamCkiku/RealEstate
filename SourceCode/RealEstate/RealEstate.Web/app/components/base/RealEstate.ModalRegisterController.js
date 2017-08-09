(function (app) {
    app.controller('ModalRegisterController', ModalRegisterController);

    ModalRegisterController.$inject = ['$scope', 'BaseService', 'apiService', '$rootScope', '$window', '$timeout', 'blockUI', '$modal', '$modalInstance', 'items'];

    function ModalRegisterController($scope, BaseService, apiService, $rootScope, $window, $timeout, blockUI, $modal, $modalInstance, items) {
        $scope.data = items;
        $scope.init = function () {
        };
        $scope.accountRegister = {
            FullName: "",
            Email: "",
            Password: "",
            ConfirmPassword: "",
            Terms: "",
            PhoneNumber: "",
            Address: ""
        }
        $scope.init();
        $scope.close = function () {
            $modalInstance.dismiss('cancel');
        };
        $scope.ok = function (response) {
            BaseService.ValidatorForm("#formRegister");
            var formRegister = angular.element(document.querySelector('#formRegister'));
            var formValidation = formRegister.data('formValidation').validate();
            if (formValidation.isValid()) {
                var myBlockUI = blockUI.instances.get('BlockUIRegister');
                myBlockUI.start();
                apiService.post('api/account/register', $scope.accountRegister, function (respone) {
                    console.log(respone);
                    if (respone.data.succeeded) {
                        myBlockUI.stop();
                        $modalInstance.close(response);
                    } else {
                        myBlockUI.stop();
                        $scope.messageRegisterError = respone.data.message;
                    }
                }, function (respone) {
                    myBlockUI.stop();
                });
            }
        };
    }
})(angular.module('myApp'));