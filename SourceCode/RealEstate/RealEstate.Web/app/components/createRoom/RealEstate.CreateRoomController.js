(function (app) {
    app.controller('CreateRoomController', CreateRoomController);

    CreateRoomController.$inject = ['$scope', 'BaseService', 'apiService', '$rootScope', '$window', '$timeout', 'blockUI', '$rootScope', '$modal', '$filter'];

    function CreateRoomController($scope, BaseService, apiService, $rootScope, $window, $timeout, blockUI, $rootScope, $modal, $filter) {
        $scope.isActive = '1';
        kendo.culture("vi-VN");
        $scope.searchType = 3;
        $scope.room = {
            MoreImages: [],
        }
        $scope.searchInfo = {

        }
        $scope.data = {
            lstToilet: [],
            lstCompass: [],
            lstConvenient: [],
            lstNumberPeople: [],
            lstNumberRoom: []
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
                success: $scope.onSuccessDropzone,
            }
        }
        $scope.onSuccess = function (e) {
            if (e.response != null) {
                $scope.room.ThumbnailImage = e.response;
            }
        };
        $scope.onSuccessDropzone = function (e) {
            if (e.operation == "upload") {
                console.log(e.files);
                for (var i = 0; i < e.files.length; i++) {
                    $scope.room.MoreImages.push(e.files[i].name);
                }
                console.log($scope.room.MoreImages);
            }
        };
        $scope.init = function () {
            if ($rootScope.userInfomation.IsAuthenticated) {
                $scope.registerControl();
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
                            $scope.room.lat = results[0].geometry.location.lat();
                            $scope.room.lng = results[0].geometry.location.lng();
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
                if ($scope.room.MoreImages != null) {
                    $scope.room.MoreImages = JSON.stringify($scope.room.MoreImages);
                }
                if ($scope.room.convenient != null) {
                    $scope.room.convenient = JSON.stringify($scope.room.convenient);
                }
                if ($scope.room.tags != null) {
                    $scope.room.tag = '';
                    angular.forEach($scope.room.tags, function (value, key) {
                        $scope.room.tag += value.text + ",";
                    })
                }
                var data = {
                    RoomName: $scope.room.RoomName,
                    Alias: $scope.room.Alias,
                    RoomTypeID: $scope.searchInfo.roomtype,
                    WardID: $scope.searchInfo.wardID,
                    DistrictID: $scope.searchInfo.districtID,
                    ProvinceID: $scope.searchInfo.provinceId,
                    VipID: "",
                    PaymentID: "",
                    ThumbnailImage: $scope.room.ThumbnailImage,
                    MoreImages: $scope.room.MoreImages,
                    Acreage: $scope.room.acreage,
                    Price: $scope.room.price,
                    Phone: $scope.room.phone,
                    Address: $scope.room.address,
                    UserName: $scope.room.username,
                    Email: $scope.room.email,
                    UserID: $rootScope.userInfomation.userID,
                    Description: $scope.room.description,
                    Content: $scope.room.content,
                    Lat: $scope.room.lat,
                    Lng: $scope.room.lng,
                    Tags: $scope.room.tag,
                    MoreInfomations: {
                        FloorNumber: $scope.room.numberroom,
                        ToiletNumber: $scope.room.toilet,
                        BedroomNumber: $scope.room.numberpeople,
                        Compass: $scope.room.compass,
                        ElectricPrice: $scope.room.ElectricPrice,
                        WaterPrice: $scope.room.WaterPrice,
                        Convenient: $scope.room.convenient,
                        Facade: "",
                    }
                }
                apiService.post('api/room/insertroom', data, function (respone) {
                    if (respone.data != null) {
                        $scope.isActive = '5';
                        BaseService.displaySuccess("Chúc mừng bạn đã đăng tin thành công", 5000);
                    } else {
                        BaseService.displayError("Đăng tin không thành công bạn vui lòng kiểm tra lại thành công", 5000);
                    }
                }, function (respone) {
                    BaseService.displayError("Đăng tin không thành công bạn vui lòng kiểm tra lại thành công", 5000);
                });
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


        $scope.showPopupPayment = function () {
            var modalInstance = $modal.open({
                animation: true,
                templateUrl: 'ModalPayment.html',
                controller: 'ModalPaymentController',
                size: 'md',
                backdrop: 'static',
                keyboard: false,
                resolve: {
                    items: function () {
                        return $scope.account;
                    }
                }
            });

            modalInstance.result.then(function (response) {
            }, function () {
                $log.info('Modal dismissed at: ' + new Date());
            });
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