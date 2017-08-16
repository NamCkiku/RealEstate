(function (app) {
    app.controller('SearchRoomController', SearchRoomController);

    SearchRoomController.$inject = ['$scope', 'BaseService', 'apiService', '$rootScope', '$window', '$timeout', 'blockUI', '$modal', '$log', 'authData', 'authenticationService', 'loginService','$filter'];

    function SearchRoomController($scope, BaseService, apiService, $rootScope, $window, $timeout, blockUI, $modal, $log, authData, authenticationService, loginService, $filter) {
        $scope.fillter = {

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
        $scope.sliderArge = {
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

        $scope.init();
    }
})(angular.module('myApp'));