(function (app) {
    app.controller('ModalRegisterController', ModalRegisterController);

    ModalRegisterController.$inject = ['$scope', 'BaseService', 'apiService', '$rootScope', '$window', '$timeout', 'blockUI', '$modal', '$modalInstance', 'items'];

    function ModalRegisterController($scope, BaseService, apiService, $rootScope, $window, $timeout, blockUI, $modal, $modalInstance, items) {
        $scope.data = items;
        console.log($scope.data);
        $scope.init = function () {
        };
        $scope.init();
        $scope.close = function () {
            $modalInstance.dismiss('cancel');
        };
        $scope.ok = function (response) {
            $modalInstance.close(response);
        };
    }
})(angular.module('myApp'));