(function (app) {
    app.controller('SearchResultController', SearchResultController);

    SearchResultController.$inject = ['$scope', 'blockUI', '$modal', '$rootScope', 'BaseService', 'apiService', '$window', '$filter'];

    function SearchResultController($scope, blockUI, $modal, $rootScope, BaseService, apiService, $window, $filter) {
        $scope.data = {
            lstRoom: [],
        }
        $scope.pageLoad = function () {
            $scope.loadAllRoomByQueryString();
        }
        angular.element(document).ready(function () {
            $scope.pageLoad();
        });

        $scope.$on('fireLoadFilterEvent', function (event, searchObj) {
            $scope.init = function () {
                $scope.FilterRoom();
            };
            // Lọc phòng theo tiêu chí
            $scope.FilterRoom = function () {
                var myBlockUI = blockUI.instances.get('BlockUIRoom');
                myBlockUI.start();
                var config = {
                    params: {
                        RoomTypeID: searchObj.roomtype,
                        PriceFrom: searchObj.minValue,
                        PriceTo: searchObj.maxValue,
                        WardID: searchObj.wardID,
                        DistrictID: searchObj.districtID,
                        ProvinceID: searchObj.provinceId,
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
                    BaseService.displayError("Không lấy được dữ liệu phòng", 3000);
                });
            }
            $scope.init();
        });
        $scope.loadAllRoomByQueryString = function () {
            if (QueryString.province != null) {
                var myBlockUI = blockUI.instances.get('BlockUIRoom');
                myBlockUI.start();
                var config = {
                    params: {
                        RoomTypeID: QueryString.priceTo,
                        PriceFrom: QueryString.priceFrom,
                        PriceTo: QueryString.maxValue,
                        WardID: QueryString.ward,
                        DistrictID: QueryString.district,
                        ProvinceID: QueryString.province,
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