(function (app) {
    app.controller('CreateRoomController', CreateRoomController);

    CreateRoomController.$inject = ['$scope', 'BaseService', 'apiService', '$rootScope', '$window', '$timeout', 'blockUI', '$rootScope', '$modal', '$filter'];

    function CreateRoomController($scope, BaseService, apiService, $rootScope, $window, $timeout, blockUI, $rootScope, $modal, $filter) {
        $scope.isDistrict = true;
        $scope.isWard = true;
        $scope.isActive = '1';
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
                $scope.isDistrict = false;
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
                $scope.isWard = false;
                myBlockUI.stop();
            }, function (respone) {
                myBlockUI.stop();
                BaseService.displayError("Không lấy được dữ liệu xã phường", 3000);
            });
        }

        $scope.onSuccess = function () {
            $log.info('Upload Successfull...');
        };
        $scope.onSelect = function (e) {
            angular.forEach(e.file, function (index, value) {
                var ok = value.extension == ".xlsx"
                    || value.extension == ".pdf"
                    || value.extension == ".doc"
                    || value.extension == ".html";

                if (value.extension === ok) {
                    e.preventDefault();
                    alert("Please upload jpg image files");
                }
            });
            //$.each(e.file, function (index, value) {
            //    var ok = value.extension == ".xlsx"
            //        || value.extension == ".pdf"
            //        || value.extension == ".doc"
            //        || value.extension == ".html";

            //    if (value.extension === ok) {
            //        e.preventDefault();
            //        alert("Please upload jpg image files");
            //    }
            //});
        };
        $scope.uploadImage = function () {
            angular.element('#txtUploadImage').trigger();
        }
        $scope.onError = function () {
            $log.info('Upload Errored out...');
            console.log('Error while uploading attachment');
        };
        $scope.fileAttachmentOptions = function () {
            //async: {
            //    saveUrl: 'app/upload/uploadAttch',
            //        removeUrl: 'remove',
            //            removeVerb: 'DELETE',
            //                autoUpload: false
            //},
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