(function (app) {
    app.controller('CreateRoomController', CreateRoomController);

    CreateRoomController.$inject = ['$scope', 'BaseService', 'apiService', '$rootScope', '$window', '$timeout', 'blockUI', '$rootScope', '$modal', '$filter'];

    function CreateRoomController($scope, BaseService, apiService, $rootScope, $window, $timeout, blockUI, $rootScope, $modal, $filter) {
        $scope.isActive = '1';
        kendo.culture("vi-VN");
        $scope.room = {

        }
        $scope.data = {
            lstRoomType: [],
            lstProvince: [],
            lstDistrict: [],
            lstWard: []
        }

        $scope.init = function () {
            $scope.GetAllRoomType();
            $scope.GetAllProvince();
            angular.element('#txtuserName').focus();
        };

        $scope.GetSeoTitle = GetSeoTitle;
        function GetSeoTitle() {
            $scope.room.Alias = BaseService.getSeoTitle($scope.room.RoomName);
        }
        //Hàm lấy ra commbobox loại phòng
        $scope.GetAllRoomType = function () {
            var myBlockUI = blockUI.instances.get('BlockUIRoom');
            myBlockUI.start();
            apiService.get('api/roomtype/getallroomtype', null, function (respone) {
                console.log(respone.data)
                $scope.lstRoomTypeTree = respone.data;
                $scope.lstRoomTypeComboboxTree = BaseService.getTree($scope.lstRoomTypeTree, { idKey: 'ID', parentKey: 'ParentID' });
                $scope.lstRoomTypeComboboxTree.forEach(function (item) {
                    recur(item, 0, $scope.data.lstRoomType);
                });
                console.log($scope.data.lstRoomType);
                myBlockUI.stop();
            }, function (respone) {
                myBlockUI.stop();
                BaseService.displayError("Không lấy được dữ liệu Loại Phòng", 3000);
            });
        }

        //Hàm lấy ra commbobox tỉnh thành
        $scope.GetAllProvince = function () {
            var myBlockUI = blockUI.instances.get('BlockUIRoom');
            myBlockUI.start();
            apiService.get('api/management/getallprovince', null, function (respone) {
                $scope.data.lstProvince = respone.data;
                myBlockUI.stop();
            }, function (respone) {
                myBlockUI.stop();
                BaseService.displayError("Không lấy được dữ liệu Tỉnh Thành", 3000);
            });
        }


        //Hàm lấy ra commbobox quận huyện theo tỉnh thành
        $scope.GetAllDistrict = GetAllDistrict;
        function GetAllDistrict(id) {
            var myBlockUI = blockUI.instances.get('BlockUIRoom');
            myBlockUI.start();
            apiService.get('api/management/getalldistrict', null, function (respone) {
                $scope.data.lstDistrict = $filter('filter')(respone.data, { provinceId: id }, true);
                $scope.data.lstWard = [];
                myBlockUI.stop();
            }, function (respone) {
                myBlockUI.stop();
                BaseService.displayError("Không lấy được dữ liệu Quận huyện", 3000);
            });
        }


        //Hàm lấy ra commbobox xã phường theo quận huyện
        $scope.GetAllWard = GetAllWard;
        function GetAllWard(id) {
            var myBlockUI = blockUI.instances.get('BlockUIRoom');
            myBlockUI.start();
            apiService.get('api/management/getallward', null, function (respone) {
                $scope.data.lstWard = $filter('filter')(respone.data, { districtID: id }, true);
                myBlockUI.stop();
            }, function (respone) {
                myBlockUI.stop();
                BaseService.displayError("Không lấy được dữ liệu xã phường", 3000);
            });
        }

        $scope.onSuccess = function () {
        };
        $scope.onSelect = function (e) {
        };
        $scope.onError = function () {
        };
        $scope.kendoUploadOptions = {
            async: {
                saveUrl: $rootScope.baseUrl + 'api/upload/uploadimage?type=room',
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
            success: $scope.onSuccess,
            remove: $scope.onRemove,
            select: $scope.onSelect,
            error: $scope.onError,

        }
        $scope.kendoUploadDropzoneOptions = {
            async: {
                saveUrl: $rootScope.baseUrl + 'api/upload/uploadsingeimage',
                autoUpload: true
            },
            multiple: true,
            validation: {
                allowedExtensions: [".jpg", ".jpeg", ".png", ".bmp", ".gif"],
                maxFileSize: 4194304
            },
            dropZone: ".dropZoneElement",
            localization: {
                select: 'Chọn ảnh',
                remove: 'Xóa',
                cancel: 'Hủy'
            },
            success: $scope.onSuccess,
            remove: $scope.onRemove,
            select: $scope.onSelect,
            error: $scope.onError,

        }
        $scope.nextStep = function (item) {
            if (item == 1) {
                BaseService.ValidatorForm("#formStep1");
                var formStep1 = angular.element(document.querySelector('#formStep1'));
                var formValidation = formStep1.data('formValidation').validate();
                if (formValidation.isValid()) {
                    $scope.isActive = '2';
                    angular.element('#txtRoomName').focus();
                }
                else {
                    BaseService.displayError("Vui lòng nhập đầy đủ thông tin", 5000);
                }
            }
            else if (item == 2) {
                BaseService.ValidatorForm("#formStep2");
                var formStep2 = angular.element(document.querySelector('#formStep2'));
                var formValidation = formStep2.data('formValidation').validate();
                if (formValidation.isValid()) {
                    $scope.isActive = '3';
                }
                else {
                    BaseService.displayError("Vui lòng nhập đầy đủ thông tin", 5000);
                }
            }
            else if (item == 3) {
                $scope.isActive = '4';
            }
            else {
                $scope.isActive = '1';
            }
        }
        $scope.previousStep = previousStep;
        function previousStep(item) {
            if (item == 1) {
                $scope.isActive = '1';
            }
            else if (item == 2) {
                $scope.isActive = '2';
            }
            else if (item == 3) {
                $scope.isActive = '3';
            }
            else {
                $scope.isActive = '4';
            }
        }



        $scope.GetUserLogin = GetUserLogin;
        function GetUserLogin() {
            var user = $rootScope.userInfomation;
            if (user.IsAuthenticated) {
                var myBlockUI = blockUI.instances.get('BlockUIRoom');
                myBlockUI.start();
                var config = {
                    params: {
                        id: user.userID
                    }
                }
                apiService.get('api/account/user', config, function (respone) {
                    var userInfo = respone.data.result;
                    $scope.room.username = userInfo.fullName;
                    $scope.room.email = userInfo.email;
                    $scope.room.phone = userInfo.phoneNumber;
                    $scope.room.address = userInfo.address;
                    myBlockUI.stop();
                }, function (respone) {
                    myBlockUI.stop();
                    BaseService.displayError("Không lấy được dữ liệu người dùng", 3000);
                });
            }
            else {
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
            }
        }

        function times(n, str) {
            var result = '';
            for (var i = 0; i < n; i++) {
                result += str;
            }
            return result;
        };
        function recur(item, level, arr) {
            arr.push({
                Name: times(level, '–') + ' ' + item.roomTypeName,
                ID: item.id,
                Level: level,
                Indent: times(level, '–')
            });
            if (item.children) {
                item.children.forEach(function (item) {
                    recur(item, level + 1, arr);
                });
            }
        };
        $scope.init();
    }
})(angular.module('myApp'));