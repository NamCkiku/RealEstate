(function (app) {
    app.controller('PVProfileUpdateUserController', PVProfileUpdateUserController);

    PVProfileUpdateUserController.$inject = ['$scope', 'BaseService', 'apiService', '$rootScope', '$window', '$timeout', 'blockUI', '$modal', '$log', 'authData', 'authenticationService', 'loginService', '$filter'];

    function PVProfileUpdateUserController($scope, BaseService, apiService, $rootScope, $window, $timeout, blockUI, $modal, $log, authData, authenticationService, loginService, $filter) {
        $scope.data = {
            lstRoomByUser: [],
            lstHistoryLogin: []
        }
        $scope.baseUrl = $rootScope.baseUrl;
        kendo.culture("vi-VN");
        angular.element(document).ready(function () {
        });
        $scope.userInfo = {};
        $scope.$on('fireLoadProfileUpdateUserEvent', function (event, userInfo) {
            $scope.userInfo = userInfo;
            $scope.data.lstGender = [
                { text: "Nam", value: true },
                { text: "Nữ", value: false },
            ];
            var address = document.getElementById('adress');
            var options = {
                componentRestrictions: { country: "VN" }
            };
            $scope.searchBox = new google.maps.places.Autocomplete(address, options);
            $scope.searchBox.addListener('place_changed', function () {

                $scope.userInfo.address = document.getElementById('adress').value;
            });
        });

        $scope.kendoUploadOptions = {

            async: {
                saveUrl: $rootScope.baseUrl + 'api/upload/uploadimage?type=avatar',
                autoUpload: true
            },
            multiple: false,
            validation: {
                //allowedExtensions: ['.html,.xlsx,.pdf,.doc,'],
                maxFileSize: 4194304
            },
            localization: {
                select: 'Chọn ảnh',
                remove: 'Xóa',
                cancel: 'Hủy'
            },
            success: function (e) {
                console.log(e);
                if (e.response != null) {
                    $scope.userInfo.avatar = e.response;
                }
            },
        }
        $scope.updateUser = function () {
            BaseService.ValidatorForm("#form-profile");
            var formprofile = angular.element(document.querySelector('#form-profile'));
            var formValidation = formprofile.data('formValidation').validate();
            if (formValidation.isValid()) {
                var myBlockUI = blockUI.instances.get('BlockUIUpdateUser');
                myBlockUI.start();
                apiService.post('api/account/updateuser', $scope.userInfo, function (respone) {
                    console.log(respone);
                    if (respone.data.succeeded) {
                        BaseService.displaySuccess("Chúc mừng bạn đã cập nhập thành công", 5000);
                        myBlockUI.stop();
                    } else {
                        myBlockUI.stop();
                        $scope.messageRegisterError = respone.data.message;
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