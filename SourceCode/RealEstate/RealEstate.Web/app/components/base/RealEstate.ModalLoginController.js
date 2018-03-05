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
            BaseService.ValidatorForm("#formLogin");
            var formLogin = angular.element(document.querySelector('#formLogin'));
            var formValidation = formLogin.data('formValidation').validate();
            if (formValidation.isValid()) {
                var myBlockUI = blockUI.instances.get('BlockUILogin');
                myBlockUI.start();
                loginService.login($scope.account.UserName, $scope.account.Password).then(function (response) {
                    if (response != null && response.data.error == "invalid_grant") {
                        myBlockUI.stop();
                        $scope.messageError = "Tài khoản hoặc mật khẩu không chính xác";
                    }
                    else {
                        myBlockUI.stop();
                        $modalInstance.close(response);
                    }
                });
            }
        };

        $scope.authExternalProvider = function (provider) {

            var redirectUri = location.protocol + '//' + location.host + '/authcomplete.html';

            var externalProviderUrl = $rootScope.baseUrl + "api/account/externallogin?provider=" + provider
                + "&response_type=token&client_id=self&redirect_uri=" + redirectUri + "&state=VIwd5edwJC0VxBoi4L6XDtjr4p6LaB9UM7D34hoJDaM1";
            window.$windowScope = $scope;

            var oauthWindow = window.open(externalProviderUrl, "Authenticate Account", "location=0,status=0,width=600,height=750");
        };
        $scope.authCompletedCB = function (fragment) {

            $scope.$apply(function () {

                if (fragment.haslocalaccount == 'False') {

                    authService.logOut();

                    authService.externalAuthData = {
                        provider: fragment.provider,
                        userName: fragment.external_user_name,
                        externalAccessToken: fragment.external_access_token
                    };

                    $location.path('/associate');

                }
                else {
                    //Obtain access token and redirect to orders
                    var externalData = { provider: fragment.provider, externalAccessToken: fragment.external_access_token };
                    authService.obtainAccessToken(externalData).then(function (response) {

                        $location.path('/orders');

                    },
                        function (err) {
                            $scope.message = err.error_description;
                        });
                }

            });
        }
    }
})(angular.module('myApp'));