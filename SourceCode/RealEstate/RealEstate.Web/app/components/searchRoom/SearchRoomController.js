(function (app) {
    app.controller('SearchRoomController', SearchRoomController);

    SearchRoomController.$inject = ['$scope', 'BaseService', 'apiService', '$rootScope', '$window', '$timeout', 'blockUI', '$modal', '$log', 'authData', 'authenticationService', 'loginService', '$filter'];

    function SearchRoomController($scope, BaseService, apiService, $rootScope, $window, $timeout, blockUI, $modal, $log, authData, authenticationService, loginService, $filter) {
        $scope.fillter = {
            roomtype: null,
            provinceId: null,
            districtID: null,
            wardID: null,
            priceFrom: 0,
            priceTo: 0,
            acreageFrom: 0,
            acreageTo: 0,
        }
        $scope.data = {
            lstRoomType: [],
            lstProvince: [],
            lstDistrict: [],
            lstWard: [],
            listRoom : []
        }
        $scope.init = function () {

            $scope.GetAllRoomType();
            $scope.GetAllProvince();
            $scope.FilterRoom();
        };
        $scope.sliderFrice = {
            minValue: 0,
            maxValue: 300,
            options: {
                floor: 0,
                ceil: 300,
                step: 0.5,
                precision: 1,
                hideLimitLabels: true,
                hidePointerLabels: true,
            }
        };
        $scope.sliderAcreage = {
            minValue: 0,
            maxValue: 500,
            options: {
                floor: 0,
                ceil: 500,
                step: 5,
                precision: 1,
                hideLimitLabels: true,
                hidePointerLabels: true,
            }
        };
        //Hàm lấy ra commbobox loại phòng
        $scope.GetAllRoomType = function () {
            var myBlockUI = blockUI.instances.get('BlockUIRoom');
            myBlockUI.start();
            apiService.get('api/roomtype/getallroomtype', null, function (respone) {
                console.log(respone.data)
                $scope.data.lstRoomType = respone.data;
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



        //Hàm tìm kiếm phòng
        $scope.searchRoom = function () {
            var params = {
                roomtype: $scope.fillter.roomtype,
                province: $scope.fillter.provinceId,
                district: $scope.fillter.districtID,
                ward: $scope.fillter.wardID,
                priceFrom: $scope.sliderFrice.minValue,
                priceTo: $scope.sliderFrice.maxValue,
                acreageFrom: $scope.sliderAcreage.minValue,
                acreageTo: $scope.sliderAcreage.maxValue,
            };
            var queryString = [];
            for (var key in params) {
                if (params[key] !== undefined) {
                    queryString.push(key + '=' + params[key]);
                }
            }
            $window.location.href = '/RoomList/Index?' + queryString.join('&');
        }

        // Lọc phòng theo tiêu chí
        $scope.FilterRoom = function () {
            var myBlockUI = blockUI.instances.get('BlockUIRoom');
            myBlockUI.start();
            alert($scope.sortKey);
            var config = {
                params: {
                    RoomTypeID: $scope.fillter.roomtype,
                    PriceFrom: $scope.sliderFrice.minValue,
                    PriceTo: $scope.sliderFrice.maxValue,
                    WardID: $scope.fillter.wardID,
                    DistrictID: $scope.fillter.districtID,
                    ProvinceID: $scope.fillter.provinceId,
                    Keywords: null,
                    StartDate: null,
                    EndDate: null,
                    Status: true,
                    page: 1,
                    pageSize: 10,
                    sort: "ASC"
                }
            }
            apiService.get('api/room/getallroomfullsearch', config, function (respone) {
                $scope.data.listRoom = respone.data.items;
                console.log($scope.data.listRoom);
                myBlockUI.stop();
            }, function (respone) {
                myBlockUI.stop();
                BaseService.displayError("Không lấy được dữ liệu xã phường", 3000);
            });
        }

        $scope.showFilter = function () {
            $('#open-filters').toggleClass('openf');
            $('.dqdt-sidebar').toggleClass('openf');
        }

        var QueryString = function () {
            // This function is anonymous, is executed immediately and 
            // the return value is assigned to QueryString!
            var query_string = {};
            var query = window.location.search.substring(1);
            var vars = query.split("&");
            for (var i = 0; i < vars.length; i++) {
                var pair = vars[i].split("=");
                // If first entry with this name
                if (typeof query_string[pair[0]] === "undefined") {
                    query_string[pair[0]] = decodeURIComponent(pair[1]);
                    // If second entry with this name
                } else if (typeof query_string[pair[0]] === "string") {
                    var arr = [query_string[pair[0]], decodeURIComponent(pair[1])];
                    query_string[pair[0]] = arr;
                    // If third or later entry with this name
                } else {
                    query_string[pair[0]].push(decodeURIComponent(pair[1]));
                }
            }
            return query_string;
        }();

        $scope.init();
    }
})(angular.module('myApp'));