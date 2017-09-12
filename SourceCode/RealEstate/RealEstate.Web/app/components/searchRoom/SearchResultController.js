(function (app) {
    app.controller('SearchResultController', SearchResultController);

    SearchResultController.$inject = ['$scope', 'blockUI', '$modal', '$rootScope', 'BaseService', 'apiService', '$window', '$filter'];

    function SearchResultController($scope, blockUI, $modal, $rootScope, BaseService, apiService, $window, $filter) {
        $scope.data = {
            lstRoom: [],
        }
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.totalCount = 0;
        $scope.filter = {};

        $scope.baseUrl = $rootScope.baseUrl;
        $scope.pageLoad = function () {
            //if (QueryString.searchIndex == 'true') {
            //    if (QueryString.roomtype != null) {
            //        $scope.searchInfo.roomtype = QueryString.roomtype.split(",");
            //    }
            //    $scope.searchInfo.provinceId = parseInt(QueryString.province);
            //    $scope.searchInfo.districtID = parseInt(QueryString.district);
            //    $scope.searchInfo.wardID = parseInt(QueryString.ward);
            //    $scope.sliderAcreage.minValue = QueryString.acreageFrom;
            //    $scope.sliderAcreage.maxValue = QueryString.acreageTo;
            //    $scope.sliderFrice.minValue = QueryString.priceFrom;
            //    $scope.sliderFrice.maxValue = QueryString.priceTo;
            //}
            $scope.loadAllRoomByQueryString();
        }
        angular.element(document).ready(function () {
            $scope.pageLoad();
        });


        $scope.$on('fireLoadFillterEvent', function (event, searchObj) {
            $scope.filter = searchObj;
            // Lọc phòng theo tiêu chí
            $scope.FilterRoom = function (page) {
                var myBlockUI = blockUI.instances.get('BlockUIRoom');
                myBlockUI.start();
                page = page || 1;
                var dataSort = "";
                var config = {
                    params: {
                        RoomTypeID: searchObj.roomtype,
                        PriceFrom: searchObj.priceFrom * 1000000,
                        PriceTo: searchObj.priceTo * 1000000,
                        AcreageFrom: searchObj.acreageFrom,
                        AcreageTo: searchObj.acreageTo,
                        WardID: searchObj.ward,
                        DistrictID: searchObj.district,
                        ProvinceID: searchObj.province,
                        page: page,
                        pageSize: 20,
                        sort: ""
                    }
                }
                apiService.get('api/room/getallroomfullsearch', config, function (respone) {
                    $scope.data.listRoom = respone.data.items;
                    $scope.page = respone.data.page;
                    $scope.pagesCount = respone.data.totalPages;
                    $scope.totalCount = respone.data.totalCount;
                    myBlockUI.stop();
                }, function (respone) {
                    myBlockUI.stop();
                    BaseService.displayError("Không lấy được dữ liệu phòng", 3000);
                });
            }
            $scope.FilterRoom();
        });

        $scope.DataSort = function (data) {
            $scope.FilterRoom(data);
        }

        $scope.loadAllRoomByQueryString = function () {
            if (QueryString.searchIndex == 'true') {
                var myBlockUI = blockUI.instances.get('BlockUIRoom');
                myBlockUI.start();
                var config = {
                    params: {
                        RoomTypeID: QueryString.roomtype,
                        PriceFrom: QueryString.priceFrom * 1000000,
                        PriceTo: QueryString.priceTo * 1000000,
                        AcreageFrom: QueryString.acreageFrom,
                        AcreageTo: QueryString.acreageTo,
                        WardID: QueryString.ward,
                        DistrictID: QueryString.district,
                        ProvinceID: QueryString.province,
                        page: 1,
                        pageSize: 20,
                        sort: ""
                    }
                }
                apiService.get('api/room/getallroomfullsearch', config, function (respone) {
                    $scope.data.listRoom = respone.data.items;
                    $scope.page = respone.data.page;
                    $scope.pagesCount = respone.data.totalPages;
                    $scope.totalCount = respone.data.totalCount;
                    myBlockUI.stop();
                }, function (respone) {
                    myBlockUI.stop();
                    BaseService.displayError("Không lấy được dữ liệu phòng", 3000);
                });
            }
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
    }
})(angular.module('myApp'));