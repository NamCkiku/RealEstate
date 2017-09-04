(function (app) {
    app.controller('SearchRoomController', SearchRoomController);

    SearchRoomController.$inject = ['$scope', 'BaseService', 'apiService', '$rootScope', '$window', '$timeout', 'blockUI', '$modal', '$log', 'authData', 'authenticationService', 'loginService', '$filter'];

    function SearchRoomController($scope, BaseService, apiService, $rootScope, $window, $timeout, blockUI, $modal, $log, authData, authenticationService, loginService, $filter) {
        $scope.searchInfo = {
            roomtype: null,
            provinceId: null,
            districtID: null,
            wardID: null,
            priceFrom: 0,
            priceTo: 0,
            acreageFrom: 0,
            acreageTo: 0,
        }
        $scope.init = function () {
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
        //Hàm tìm kiếm phòng
        $scope.searchRoom = function () {
            var params = {
                roomtype: $scope.searchInfo.roomtype,
                province: $scope.searchInfo.provinceId,
                district: $scope.searchInfo.districtID,
                ward: $scope.searchInfo.wardID,
                priceFrom: $scope.sliderFrice.minValue,
                priceTo: $scope.sliderFrice.maxValue,
                acreageFrom: $scope.sliderAcreage.minValue,
                acreageTo: $scope.sliderAcreage.maxValue,
                searchIndex: true
            };
            var queryString = [];
            for (var key in params) {
                if (params[key] !== undefined && params[key] !== null) {
                    queryString.push(key + '=' + params[key]);
                }
            }
            $window.location.href = '/danh-sach-phong?' + queryString.join('&');
        }

        $scope.fireLoadFilterEvent = function () {
            $scope.fillter = {
                roomtype: $scope.searchInfo.roomtype,
                province: $scope.searchInfo.provinceId,
                district: $scope.searchInfo.districtID,
                ward: $scope.searchInfo.wardID,
                priceFrom: $scope.sliderFrice.minValue,
                priceTo: $scope.sliderFrice.maxValue,
                acreageFrom: $scope.sliderAcreage.minValue,
                acreageTo: $scope.sliderAcreage.maxValue,
            }
            $scope.$broadcast('fireLoadFillterEvent', $scope.fillter);
        };



        $scope.showFilter = function () {
            $('#open-filters').toggleClass('openf');
            $('.dqdt-sidebar').toggleClass('openf');
        }
        $scope.init();
    }
})(angular.module('myApp'));