(function (app) {
    app.controller('RoomDetailController', RoomDetailController);

    RoomDetailController.$inject = ['$scope', 'BaseService', 'apiService', '$rootScope', '$window', '$timeout', 'blockUI', '$modal', '$log', 'authData', 'authenticationService', 'loginService', '$filter', '$sce'];

    function RoomDetailController($scope, BaseService, apiService, $rootScope, $window, $timeout, blockUI, $modal, $log, authData, authenticationService, loginService, $filter, $sce) {
        $scope.baseUrl = $rootScope.baseUrl;
        $scope.isShowPhone = false;
        $scope.init = function () {
            $scope.getRoomById();
        }
        $scope.roomObj = {

        }
        $scope.showPhoneNumber = function () {
            $scope.isShowPhone = true;
        }
        $scope.renderHtml = function (htmlCode) {
            return $sce.trustAsHtml(htmlCode);
        };
        $scope.getRoomById = function () {
            var myBlockUI = blockUI.instances.get('BlockUIRoomDetail');
            myBlockUI.start();
            var config = {
                params: {
                    roomId: $('#txtRoomId').val(),
                }
            }
            apiService.get('api/room/getroombyid', config, function (respone) {
                $scope.roomObj = respone.data;
                console.log($scope.roomObj);
                $scope.roomObj.convenient = JSON.parse(respone.data.convenient);
                $scope.roomObj.moreImages = JSON.parse(respone.data.moreImages);
                var pos = {
                    lat: respone.data.lat,
                    lng: respone.data.lng
                };
                var myLatlng = new google.maps.LatLng(pos.lat, pos.lng);
                var mapOptions = {
                    zoom: 14,
                    center: myLatlng
                }
                var map = new google.maps.Map(document.getElementById("map-canvas"), mapOptions);

                var marker = new google.maps.Marker({
                    position: myLatlng,
                    title: "Hello World!"
                });
                var populationOptions = {
                    strokeColor: '#67cfd8',
                    strokeOpacity: 0.6,
                    strokeWeight: 1,
                    fillColor: '#67cfd8',
                    fillOpacity: 0.2,
                    center: myLatlng,
                    map: map,
                    radius: Math.sqrt(2500000)
                };
                var cityCircle = new google.maps.Circle(populationOptions);
                // To add the marker to the map, call setMap();
                marker.setMap(map);
                myBlockUI.stop();
            }, function (respone) {
                myBlockUI.stop();
                BaseService.displayError("Không lấy được dữ liệu phòng", 3000);
            });
        }


        $scope.init();
    }
})(angular.module('myApp'));