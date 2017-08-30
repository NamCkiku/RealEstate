(function (app) {
    app.controller('ModalPaymentController', ModalPaymentController);

    ModalPaymentController.$inject = ['$scope', 'BaseService', 'apiService', '$rootScope', '$window', '$timeout', 'blockUI', '$modal', '$modalInstance', 'items', 'loginService', 'authData'];

    function ModalPaymentController($scope, BaseService, apiService, $rootScope, $window, $timeout, blockUI, $modal, $modalInstance, items, loginService, authData) {
        $scope.data = items;
        $scope.init = function () {
        };
        $scope.init();
        $scope.close = function () {
            $modalInstance.dismiss('cancel');
        };
        $scope.ok = function (response) {
            
        };
    }
})(angular.module('myApp'));