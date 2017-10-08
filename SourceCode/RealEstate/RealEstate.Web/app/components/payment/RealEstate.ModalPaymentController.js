(function (app) {
    app.controller('ModalPaymentController', ModalPaymentController);

    ModalPaymentController.$inject = ['$scope', 'BaseService', 'apiService', '$rootScope', '$window', '$timeout', 'blockUI', '$modal', '$modalInstance', 'items', 'loginService', 'authData', 'vcRecaptchaService'];

    function ModalPaymentController($scope, BaseService, apiService, $rootScope, $window, $timeout, blockUI, $modal, $modalInstance, items, loginService, authData, vcRecaptchaService) {
        $scope.data = items;
        console.log(items);
        $scope.cart = {
            CardType: "VIETTEL"
        }
        $scope.bankinfo = {
            bankname: "VCB",
            totalMonney: '20000'
        }
        $scope.data.lsCashType = [
            { text: "Thẻ cào VIETTEL", value: "VIETTEL" },
            { text: "Thẻ cào VINAPHONE", value: "VNP" },
            { text: "Thẻ cào MOBIPHONE", value: "VMS" },
            { text: "Vcoin", value: "VCOIN" },
            { text: "Gate", value: "GATE" },
        ];
        $scope.init = function () {
        };
        $scope.init();
        $scope.close = function () {
            $modalInstance.dismiss('cancel');
        };
        $scope.ok = function (response) {

        };
        $scope.PaymentCard = function () {
            BaseService.ValidatorForm("#formpaymentCash");
            var formStep2 = angular.element(document.querySelector('#formpaymentCash'));
            var formValidation = formStep2.data('formValidation').validate();
            if (formValidation.isValid()) {
                var myBlockUI = blockUI.instances.get('BlockUIPayment');
                myBlockUI.start();
                apiService.post('api/payment/paymentcash', $scope.cart, function (respone) {
                    console.log(respone);
                    BaseService.displaySuccess(respone.data.message, 10000);
                    myBlockUI.stop();
                    $modalInstance.close(response);
                }, function (respone) {
                    myBlockUI.stop();
                    BaseService.displayError(respone.data.message, 10000);
                });
            }
            else {
                BaseService.displayError("Vui lòng nhập đầy đủ thông tin", 5000);
            }

        }
        $scope.PaymentBank = function () {
            //var myBlockUI = blockUI.instances.get('BlockUIPayment');
            //myBlockUI.start();
            //apiService.post('api/payment/paymentcash', $scope.cart, function (respone) {
            //    console.log(respone);
            //    BaseService.displaySuccess(respone.data.message, 10000);
            //    myBlockUI.stop();
            //    $modalInstance.close(response);
            //}, function (respone) {
            //    myBlockUI.stop();
            //    BaseService.displayError(respone.data.message, 10000);
            //});
        }
    }
})(angular.module('myApp'));