(function (app) {
    app.controller('CreateRoomController', CreateRoomController);

    CreateRoomController.$inject = ['$scope', 'BaseService', 'apiService', '$rootScope', '$window', '$timeout', 'blockUI', '$rootScope', '$modal', '$filter'];

    function CreateRoomController($scope, BaseService, apiService, $rootScope, $window, $timeout, blockUI, $rootScope, $modal, $filter) {
        $scope.isActive = '3';
        kendo.culture("vi-VN");
        $scope.room = {

        }
        $scope.data = {
            lstRoomType: [],
            lstProvince: [],
            lstDistrict: [],
            lstWard: [],
            lstToilet: [],
            lstCompass: [],
            lstConvenient: [],
            lstNumberPeople: [],
            lstNumberRoom:[]
        }
        $scope.registerControl = function () {
            $scope.data.lstNumberPeople = [
                { text: "Chưa xác định", value: "" },
                { text: "1 người", value: "1" },
                { text: "2 người", value: "2" },
                { text: "3 người", value: "3" },
                { text: "4 người", value: "4" },
                { text: "5 người", value: "5" },
                { text: "6 người", value: "6" },
                { text: "7 người", value: "7" },
                { text: "8 người", value: "8" },
                { text: "9 người", value: "9" },
                { text: "10 người", value: "10" },
            ];
            $scope.data.lstNumberRoom = [
                { text: "Chưa xác định", value: "" },
                { text: "1 phòng", value: "1" },
                { text: "2 phòng", value: "2" },
                { text: "3 phòng", value: "3" },
                { text: "4 phòng", value: "4" },
                { text: "5 phòng", value: "5" },
                { text: "6 phòng", value: "6" },
                { text: "7 phòng", value: "7" },
                { text: "8 phòng", value: "8" },
                { text: "9 phòng", value: "9" },
                { text: "10 phòng", value: "10" },
            ];
            $scope.data.lstToilet = [
                { text: "Chưa xác định", value: "" },
                { text: "Khép kín", value: "Khép kín" },
                { text: "Chung", value: "Chung" },
            ];
            $scope.data.lstCompass = [
                { text: "Chưa xác định", value: "" },
                { text: "Đông", value: "Đông" },
                { text: "Đông bắc", value: "Đông bắc" },
                { text: "Đông nam", value: "Đông nam" },
                { text: "Bắc", value: "Bắc" },
                { text: "Tây", value: "Tây" },
                { text: "Tây bắc", value: "Tây bắc" },
                { text: "Tây nam", value: "Tây nam" },
                { text: "Nam", value: "Nam" },
            ];
            $scope.data.lstConvenient = [
                { id: "Chỗ để xe", label: "Chỗ để xe" },
                { id: "Sân phơi", label: "Sân phơi" },
                { id: "Thang máy", label: "Thang máy" },
                { id: "Internet", label: "Internet" },
                { id: "Điều hòa", label: "Điều hòa" },
                { id: "Bình nóng lạnh", label: "Bình nóng lạnh" },
                { id: "Máy giặt", label: "Máy giặt" },
                { id: "Truyền hình cáp", label: "Truyền hình cáp" },
                { id: "Tivi", label: "Tivi" },
            ];
            $scope.selectConvenientOptions = {
                placeholder: "Chọn các tiện ích...",
                dataTextField: "label",
                dataValueField: "id",
                valuePrimitive: true,
                autoBind: false,
                dataSource: $scope.data.lstConvenient,
            }
            $scope.editorOptions = {
                tools: [
                    "bold",
                    "italic",
                    "underline",
                    "strikethrough",
                    "justifyLeft",
                    "justifyCenter",
                    "justifyRight",
                    "justifyFull",
                    "insertUnorderedList",
                    "insertOrderedList",
                    "indent",
                    "outdent",
                    "createLink",
                    "unlink",
                    "subscript",
                    "superscript",
                    "createTable",
                    "addRowAbove",
                    "addRowBelow",
                    "addColumnLeft",
                    "addColumnRight",
                    "deleteRow",
                    "deleteColumn",
                    "viewHtml",
                    "formatting",
                    "cleanFormatting",
                    "fontName",
                    "fontSize",
                    "foreColor",
                    "backColor",
                    "print",
                    "pdf"
                ],
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
                    autoUpload: false
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
        }

        $scope.init = function () {
            if ($rootScope.userInfomation.IsAuthenticated) {
                $scope.registerControl();
                $scope.GetAllRoomType();
                $scope.GetAllProvince();
                $scope.initMap();
                angular.element('#txtuserName').focus();
            }
            else {
                $window.location.href = '/';
            }

        };


        $scope.initMap = function () {
            var address = document.getElementById('adress');
            var options = {
                componentRestrictions: { country: "VN" }
            };
            $scope.autocomplete = new google.maps.places.Autocomplete(address, options);
            var pos = {
                lat: 21.0029317912212212,
                lng: 105.820226663232323
            };
            $scope.myLatlng = new google.maps.LatLng(pos.lat, pos.lng);
            $scope.map = new google.maps.Map(document.getElementById('map-canvas'), {
                zoom: 14,
                center: $scope.myLatlng
            });
            $scope.marker = new google.maps.Marker({
                position: $scope.myLatlng,
                draggable: true,
            });
            $scope.marker.setMap($scope.map);
            google.maps.event.addListener($scope.marker, 'dragend', function () {
                $scope.geocodePosition($scope.marker.getPosition());
            });

            $scope.geocodePosition = function (pos) {
                geocoder = new google.maps.Geocoder();
                geocoder.geocode
                    ({
                        latLng: pos
                    },
                    function (results, status) {
                        if (status == google.maps.GeocoderStatus.OK) {
                            console.log(results[0].geometry.location.lat(), results[0].geometry.location.lng());
                        }
                        else {
                            alert("error");
                        }
                    }
                    );
            }

            $scope.searchBox = new google.maps.places.Autocomplete(document.getElementById('pac-input'), options);
            $scope.map.controls[google.maps.ControlPosition.TOP_CENTER].push(document.getElementById('pac-input'));
            $scope.searchBox.addListener('place_changed', function () {
                $scope.searchBox.set('map', null);
                $scope.places = $scope.searchBox.getPlace();
                var bounds = new google.maps.LatLngBounds();
                var i, place;
                bounds.extend($scope.places.geometry.location);
                $scope.map.fitBounds(bounds);
                $scope.searchBox.set('map', $scope.map);
                $scope.map.setZoom(Math.min($scope.map.getZoom(), 12));
                $scope.marker.setPosition($scope.places.geometry.location);
                google.maps.event.addListener($scope.marker, 'map_changed', function () {
                    if (!this.getMap()) {
                        this.unbindAll();
                    }
                });
            });
        }

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
                $scope.room.username = user.fullName;
                $scope.room.email = user.email;
                $scope.room.phone = user.phonenumber;
                $scope.room.address = user.address;
                //var myBlockUI = blockUI.instances.get('BlockUIRoom');
                //myBlockUI.start();
                //var config = {
                //    params: {
                //        id: user.userID
                //    }
                //}
                //apiService.get('api/account/user', config, function (respone) {
                //    var userInfo = respone.data.result;
                //    $scope.room.username = userInfo.fullName;
                //    $scope.room.email = userInfo.email;
                //    $scope.room.phone = userInfo.phoneNumber;
                //    $scope.room.address = userInfo.address;
                //    myBlockUI.stop();
                //}, function (respone) {
                //    myBlockUI.stop();
                //    BaseService.displayError("Không lấy được dữ liệu người dùng", 3000);
                //});
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