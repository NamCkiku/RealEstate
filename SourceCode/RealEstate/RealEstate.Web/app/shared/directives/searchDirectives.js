(function (app) {
    'use strict';   
    app.directive('searchControl', searchControl);
    searchControl.$inject = ['$rootScope'];
    function searchControl($rootScope) {
        var controller = ['$scope', 'BaseService', 'apiService', '$rootScope', '$window', '$timeout', 'blockUI', '$modal', '$log', 'authData', 'authenticationService', 'loginService', '$filter',
            function ($scope, BaseService, apiService, $rootScope, $window, $timeout, blockUI, $modal, $log, authData, authenticationService, loginService, $filter) {
                $scope.data = {
                    lstRoomType: [],
                    lstProvince: [],
                    lstDistrict: [],
                    lstWard: [],
                    lstDistrictAll: [],
                    lstWardAll: [],
                }
                $scope.searchType = null;
                $scope.fillter = {
                    roomtype: null,
                    provinceId: null,
                    districtID: null,
                    wardID: null,
                }
                $scope.init = function () {
                    $scope.GetAllRoomType();
                    $scope.GetAllProvince();
                    getLocation();
                };

                //Hàm lấy ra commbobox loại phòng
                $scope.GetAllRoomType = function () {
                    apiService.get('api/roomtype/getallroomtype', null, function (respone) {
                        $scope.data.lstRoomType = respone.data;
                    }, function (respone) {
                        BaseService.displayError("Không lấy được dữ liệu Loại Phòng", 3000);
                    });
                }

                //Hàm lấy ra commbobox tỉnh thành
                $scope.GetAllProvince = function () {
                    apiService.get('api/management/getallprovince', null, function (respone) {
                        $scope.data.lstProvince = respone.data;
                    }, function (respone) {
                        BaseService.displayError("Không lấy được dữ liệu Tỉnh Thành", 3000);
                    });
                }


                //Hàm lấy ra commbobox quận huyện theo tỉnh thành
                $scope.GetAllDistrict = GetAllDistrict;

                function GetAllDistrict(id) {
                    if ($scope.data.lstDistrictAll != null && $scope.data.lstDistrictAll.length > 0) {
                        $scope.data.lstDistrict = $filter('filter')($scope.data.lstDistrictAll, { provinceId: id }, true);
                    }
                    else {
                        apiService.get('api/management/getalldistrict', null, function (respone) {
                            $scope.data.lstDistrictAll = respone.data;
                            $scope.data.lstDistrict = $filter('filter')(respone.data, { provinceId: id }, true);
                            $scope.data.lstWard = [];
                        }, function (respone) {
                            BaseService.displayError("Không lấy được dữ liệu Quận huyện", 3000);
                        });
                    }
                }


                //Hàm lấy ra commbobox xã phường theo quận huyện
                $scope.GetAllWard = GetAllWard;
                function GetAllWard(id) {
                    if ($scope.data.lstWardAll != null && $scope.data.lstWardAll.length > 0) {
                        $scope.data.lstWard = $filter('filter')($scope.data.lstWardAll, { districtID: id }, true);
                    }
                    else {
                        apiService.get('api/management/getallward', null, function (respone) {
                            $scope.data.lstWardAll = respone.data;
                            $scope.data.lstWard = $filter('filter')(respone.data, { districtID: id }, true);
                        }, function (respone) {
                            BaseService.displayError("Không lấy được dữ liệu xã phường", 3000);
                        });
                    }
                }
                $scope.init();
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

                function getLocation() {
                    if (navigator.geolocation) {
                        navigator.geolocation.getCurrentPosition(showPosition);

                    } else {
                        x.innerHTML = "Geolocation is not supported by this browser.";
                    }
                };

                function showPosition(position) {
                    //alert(position.coords.latitude + "-" + position.coords.longitude);
                    var request = new XMLHttpRequest();

                    var method = 'GET';
                    var url = 'http://maps.googleapis.com/maps/api/geocode/json?latlng=' + position.coords.latitude + ',' + position.coords.longitude + '&sensor=true';
                    var async = true;

                    request.open(method, url, async);
                    request.onreadystatechange = function () {
                        if (request.readyState == 4 && request.status == 200) {
                            var data = JSON.parse(request.responseText);
                            var address = data.results[0];
                            //alert(address.address_components[3].long_name + '-' + address.address_components[2].long_name + '-' + address.address_components[1].long_name);
                            //$scope.fillter.provinceId = address.address_components[3].long_name;
                            //$scope.fillter.districtID = address.address_components[2].long_name;
                            //$scope.fillter.wardID = address.address_components[1].long_name;
                        }
                    };
                    request.send();
                };

            }]
        return {
            restrict: 'EA', //Default in 1.3+
            scope: {
                fillter: '=searchInfo',
                searchType: '=?searchtype'
            },
            controller: controller,
            link: function (scope, elem, attr) {

            },
            templateUrl: '/app/shared/directives/searchDirectives.html',
        };
    }

})(angular.module('myApp'));