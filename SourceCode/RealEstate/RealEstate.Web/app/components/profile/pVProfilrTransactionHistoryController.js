(function (app) {
    app.controller('pVProfilrTransactionHistoryController', pVProfilrTransactionHistoryController);

    pVProfilrTransactionHistoryController.$inject = ['$scope', 'BaseService', 'apiService', '$rootScope', '$window', '$timeout', 'blockUI', '$modal', '$log', 'authData', 'authenticationService', 'loginService', '$filter'];

    function pVProfilrTransactionHistoryController($scope, BaseService, apiService, $rootScope, $window, $timeout, blockUI, $modal, $log, authData, authenticationService, loginService, $filter) {
        $scope.data = {
            lstHistoryTransaction: []
        }
        $scope.transactionCurrentPage = 1;
        $scope.pageSize = 5;
        $scope.totalTransactionHistoryCount = 0;
        angular.element(document).ready(function () {
        });
        $scope.changePassword = {};
        $scope.$on('fireLoadTransactionHistoryEvent', function (event, userInfo) {

            $scope.getAllHistoryTransaction = function (pageNumber) {
                var myBlockUI = blockUI.instances.get('BlockUITransactionHistory');
                myBlockUI.start();
                pageNumber = pageNumber || 1;
                var config = {
                    params: {
                        userID: userInfo.id,
                        page: pageNumber,
                        pageSize: $scope.pageSize,
                    }
                }
                apiService.get('api/management/gettransactionhistory', config, function (respone) {
                    $scope.data.lstHistoryTransaction = respone.data.items;
                    console.log($scope.data.lstHistoryTransaction);
                    $scope.totalTransactionHistoryCount = respone.data.totalCount;
                    myBlockUI.stop();
                }, function (respone) {
                    myBlockUI.stop();
                    BaseService.displayError("Không lấy được dữ liệu phòng", 3000);
                });
            }
            $scope.load = function () {
                $scope.getAllHistoryTransaction();
            };
            $scope.load();
        });

        $scope.pageHistoryTransactionChanged = function (newPage) {
            $scope.getAllHistoryTransaction(newPage);
        };

    }
})(angular.module('myApp'));