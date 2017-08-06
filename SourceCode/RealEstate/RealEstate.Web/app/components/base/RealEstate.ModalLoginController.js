(function (app) {
    app.controller('ModalLoginController', ModalLoginController);

    ModalLoginController.$inject = ['$scope', 'BaseService', 'apiService', '$rootScope', '$window', '$timeout', 'blockUI', '$modal', '$modalInstance', 'items', 'loginService', 'authData'];

    function ModalLoginController($scope, BaseService, apiService, $rootScope, $window, $timeout, blockUI, $modal, $modalInstance, items, loginService, authData) {
        $scope.data = items;
        console.log($scope.data);
        $scope.account = {
            UserName: '',
            Password: ''
        }
        $scope.init = function () {
        };
        $scope.init();
        $scope.close = function () {
            $modalInstance.dismiss('cancel');
        };
        $scope.ok = function (response) {
            loginService.login($scope.account.UserName, $scope.account.Password).then(function (response) {
                if (response != null && response.data.error != undefined) {
                    BaseService.displayError(response.data.error_description);
                }
                else {
                    $modalInstance.close(response);
                }
            });

        };
    }
})(angular.module('myApp'));