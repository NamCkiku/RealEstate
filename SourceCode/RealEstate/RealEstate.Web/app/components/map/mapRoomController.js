(function (app) {
    app.controller('MapRoomController', MapRoomController);

    MapRoomController.$inject = ['$scope', 'BaseService', 'apiService', '$rootScope', '$window', '$timeout', 'blockUI', '$modal', '$log', 'authData', 'authenticationService', 'loginService', '$filter', '$sce'];

    function MapRoomController($scope, BaseService, apiService, $rootScope, $window, $timeout, blockUI, $modal, $log, authData, authenticationService, loginService, $filter, $sce) {
        $scope.baseUrl = $rootScope.baseUrl;
        $scope.markers = [];
        var placesIDs = new Array();
        var transportationsMarkers = new Array();
        var supermarketsMarkers = new Array();
        var schoolsMarkers = new Array();
        var librariesMarkers = new Array();
        var pharmaciesMarkers = new Array();
        var hospitalsMarkers = new Array();
        var drgflag = true;
        var is_mobile = false;
        if (/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent)) {
            drgflag = false;
            is_mobile = true;
        }
        $scope.myLatlng = new google.maps.LatLng(21.0029317912212212, 105.820226663232323);
        var mapOptions = {
            zoom: 14,
            mapTypeId: google.maps.MapTypeId.ROADMAP,
            panControl: true,
            draggable: drgflag,
            zoomControl: true,
            mapTypeControl: true,
            scaleControl: true,
            streetViewControl: true,
            overviewMapControl: true,
            center: $scope.myLatlng,
            zoomControlOptions: {
                style: google.maps.ZoomControlStyle.SMALL,
                position: google.maps.ControlPosition.RIGHT_BOTTOM
            },
            streetViewControlOptions: {
                position: google.maps.ControlPosition.RIGHT_BOTTOM
            }
        }
        $scope.infobox = new InfoBox({
            disableAutoPan: true, //false
            maxWidth: 275,
            alignBottom: true,
            pixelOffset: new google.maps.Size(-122, -48),
            zIndex: null,
            closeBoxMargin: "0 0 -16px -16px",
            //closeBoxURL: infoboxClose,
            infoBoxClearance: new google.maps.Size(1, 1),
            isHidden: false,
            pane: "floatPane",
            enableEventPropagation: false
        });
        $scope.poiInfo = new InfoBox({
            disableAutoPan: false,
            maxWidth: 250,
            pixelOffset: new google.maps.Size(-72, -70),
            zIndex: null,
            boxStyle: {
                'background': '#ffffff',
                'opacity': 1,
                'padding': '6px',
                'box-shadow': '0 1px 2px 0 rgba(0, 0, 0, 0.12)',
                'width': '145px',
                'text-align': 'center',
                'border-radius': '4px'
            },
            closeBoxMargin: "28px 26px 0px 0px",
            closeBoxURL: "",
            infoBoxClearance: new google.maps.Size(1, 1),
            pane: "floatPane",
            enableEventPropagation: false
        });
        $scope.map = new google.maps.Map(document.getElementById("map-canvas"), mapOptions);
        var markerCluster = null;
        var current_marker = 0;
        $scope.data = {
            listRoom: [],
        }
        var populationOptions = {
            strokeColor: '#67cfd8',
            strokeOpacity: 0.6,
            strokeWeight: 1,
            fillColor: '#67cfd8',
            fillOpacity: 0.2,
            center: $scope.myLatlng,
            map: $scope.map,
            radius: Math.sqrt(2500000)
        };
        var cityCircle = new google.maps.Circle(populationOptions);
        $scope.searchInfo = {
            roomtype: "",
            provinceId: null,
            districtID: null,
            wardID: null,
            priceFrom: 0,
            priceTo: 0,
            acreageFrom: 0,
            acreageTo: 0,
        }
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


        /*--------------------------------------------------------------------------
         *  Houzez Add Marker
         * -------------------------------------------------------------------------*/
        $scope.AddMarkers = function (props, map) {
            $.each(props, function (i, prop) {

                var latlng = new google.maps.LatLng(prop.lat, prop.lng);

                var marker_url = 'https://cdn1.iconfinder.com/data/icons/communication-social-media-set-2/512/15-512.png';
                var marker_size = new google.maps.Size(44, 56);
                if (window.devicePixelRatio > 1.5) {
                    if (prop.retinaIcon) {
                        marker_url = 'https://cdn1.iconfinder.com/data/icons/communication-social-media-set-2/512/15-512.png';
                        marker_size = new google.maps.Size(44, 56);
                    }
                }

                var marker_icon = {
                    url: 'https://cdn1.iconfinder.com/data/icons/communication-social-media-set-2/512/15-512.png',
                    size: marker_size,
                    scaledSize: new google.maps.Size(44, 56),
                    origin: new google.maps.Point(0, 0),
                    anchor: new google.maps.Point(7, 27)
                };

                $scope.marker = new google.maps.Marker({
                    position: latlng,
                    map: map,
                    icon: marker_icon,
                    draggable: false,
                    animation: google.maps.Animation.DROP,
                    //title: 'marker-'+prop.sanitizetitle
                });

                var prop_title = prop.data ? prop.data.post_title : prop.title;

                var infoboxContent = document.createElement("div");
                infoboxContent.className = 'property-item item-grid map-info-box';
                infoboxContent.innerHTML = '' +
                    '<div class="figure-block">' +
                    '<figure class="item-thumb">' +
                    '<div class="price hide-on-list">' +
                    '<span class="item-price">' + prop.price + '</span>' +
                    '</div>' +
                    '<a href="' + prop.url + '" class="hover-effect" tabindex="0">' + prop.thumbnail + '</a>' +
                    '</figure>' +
                    '</div>' +
                    '<div class="item-body">' +
                    '<div class="body-left">' +
                    '<div class="info-row">' +
                    '<h2><a target="_blank" href="' + prop.url + '">' + prop_title + '</a></h2>' +
                    '<h4>' + prop.address + '</h4>' +
                    '</div>' +
                    '<div class="table-list full-width info-row">' +
                    '<div class="cell">' +
                    '<div class="info-row amenities">' + prop.prop_meta +
                    '<p>' + prop.type + '</p>' +
                    '</div>' +
                    '</div>' +
                    '</div>' +
                    '</div>' +
                    '</div>';

                google.maps.event.addListener($scope.marker, 'click', (function (marker, i) {
                    return function () {
                        var scale = Math.pow(2, map.getZoom());
                        var offsety = ((100 / scale) || 0);
                        var projection = map.getProjection();
                        var markerPosition = marker.getPosition();
                        var markerScreenPosition = projection.fromLatLngToPoint(markerPosition);
                        var pointHalfScreenAbove = new google.maps.Point(markerScreenPosition.x, markerScreenPosition.y - offsety);
                        var aboveMarkerLatLng = projection.fromPointToLatLng(pointHalfScreenAbove);
                        map.setCenter(aboveMarkerLatLng);
                        $scope.infobox.setContent(infoboxContent);
                        $scope.infobox.open(map, marker);

                    }
                })($scope.marker, i));

                $scope.markers.push($scope.marker);
            });
        }


        $scope.getPOIs = function (position, poiMap, poiType) {
            var service = new google.maps.places.PlacesService(poiMap);
            var bounds = poiMap.getBounds();
            var types = new Array();
            switch (poiType) {
                case 'transportations':
                    types = ['bus_station', 'subway_station', 'train_station', 'airport'];
                    break;
                case 'supermarkets':
                    types = ['grocery_or_supermarket', 'shopping_mall'];
                    break;
                case 'schools':
                    types = ['school', 'university'];
                    break;
                case 'libraries':
                    types = ['library'];
                    break;
                case 'pharmacies':
                    types = ['pharmacy'];
                    break;
                case 'hospitals':
                    types = ['hospital'];
                    break;
            }

            service.nearbySearch({
                location: position,
                bounds: bounds,
                radius: 2000,
                types: types
            }, function poiCallback(results, status) {
                if (status === google.maps.places.PlacesServiceStatus.OK) {
                    for (var i = 0; i < results.length; i++) {
                        if (jQuery.inArray(results[i].place_id, placesIDs) == -1) {
                            $scope.createPOI(results[i], poiMap, poiType);
                            placesIDs.push(results[i].place_id);
                        }
                    }
                }
            });
        }

        $scope.createPOI = function (place, poiMap, type) {
            var marker;

            switch (type) {
                case 'transportations':
                    marker = new google.maps.Marker({
                        map: poiMap,
                        position: place.geometry.location,
                        icon: 'http://houzez01.favethemes.com/wp-content/themes/houzez/images/map/transportation.png'
                    });
                    transportationsMarkers.push(marker);
                    break;
                case 'supermarkets':
                    marker = new google.maps.Marker({
                        map: poiMap,
                        position: place.geometry.location,
                        icon: 'http://houzez01.favethemes.com/wp-content/themes/houzez/images/map/supermarket.png'
                    });
                    supermarketsMarkers.push(marker);
                    break;
                case 'schools':
                    marker = new google.maps.Marker({
                        map: poiMap,
                        position: place.geometry.location,
                        icon: 'http://houzez01.favethemes.com/wp-content/themes/houzez/images/map/school.png'
                    });
                    schoolsMarkers.push(marker);
                    break;
                case 'libraries':
                    marker = new google.maps.Marker({
                        map: poiMap,
                        position: place.geometry.location,
                        icon: 'http://houzez01.favethemes.com/wp-content/themes/houzez/images/map/libraries.png'
                    });
                    librariesMarkers.push(marker);
                    break;
                case 'pharmacies':
                    marker = new google.maps.Marker({
                        map: poiMap,
                        position: place.geometry.location,
                        icon: 'http://houzez01.favethemes.com/wp-content/themes/houzez/images/map/pharmacy.png'
                    });
                    pharmaciesMarkers.push(marker);
                    break;
                case 'hospitals':
                    marker = new google.maps.Marker({
                        map: poiMap,
                        position: place.geometry.location,
                        icon: 'http://houzez01.favethemes.com/wp-content/themes/houzez/images/map/hospital.png'
                    });
                    hospitalsMarkers.push(marker);
                    break;
            }

            google.maps.event.addListener(marker, 'mouseover', function () {
                $scope.poiInfo.setContent(place.name);
                $scope.poiInfo.open(poiMap, this);
            });
            google.maps.event.addListener(marker, 'mouseout', function () {
                $scope.poiInfo.open(null, null);
            });
        }
        $scope.tooglePOIs = function (poiMap, type) {
            for (var i = 0; i < type.length; i++) {
                if (type[i].getMap() != null) {
                    type[i].setMap(null);
                } else {
                    type[i].setMap(poiMap);
                }
            }
        }

        $scope.poiControls = function (controlDiv, poiMap, center) {
            controlDiv.style.clear = 'both';
            var map_icons_path = 'http://houzez01.favethemes.com/wp-content/themes/houzez/images/map/';
            // Set CSS for transportations POI
            var transportationUI = document.createElement('div');
            transportationUI.id = 'transportation';
            transportationUI.class = 'transportation';
            transportationUI.title = 'transportation';
            controlDiv.appendChild(transportationUI);
            var transportationIcon = document.createElement('div');
            transportationIcon.id = 'transportationIcon';
            transportationIcon.innerHTML = '<div class="icon"><img src="' + map_icons_path + 'transportation-panel-icon.png" alt=""></div><span>transportation</span>';
            transportationUI.appendChild(transportationIcon);


            // Set CSS for supermarkets POI
            var supermarketsUI = document.createElement('div');
            supermarketsUI.id = 'supermarkets';
            supermarketsUI.title = 'supermarkets';
            controlDiv.appendChild(supermarketsUI);
            var supermarketsIcon = document.createElement('div');
            supermarketsIcon.id = 'supermarketsIcon';
            supermarketsIcon.innerHTML = '<div class="icon"><img src="' + map_icons_path + 'supermarket-panel-icon.png" alt=""></div><span>supermarket</span>';
            supermarketsUI.appendChild(supermarketsIcon);

            // Set CSS for schools POI
            var schoolsUI = document.createElement('div');
            schoolsUI.id = 'schools';
            schoolsUI.title = 'schools';
            controlDiv.appendChild(schoolsUI);
            var schoolsIcon = document.createElement('div');
            schoolsIcon.id = 'schoolsIcon';
            schoolsIcon.innerHTML = '<div class="icon"><img src="' + map_icons_path + 'school-panel-icon.png" alt=""></div><span>schools</span>';
            schoolsUI.appendChild(schoolsIcon);

            // Set CSS for libraries POI
            var librariesUI = document.createElement('div');
            librariesUI.id = 'libraries';
            librariesUI.title = 'libraries';
            controlDiv.appendChild(librariesUI);
            var librariesIcon = document.createElement('div');
            librariesIcon.id = 'librariesIcon';
            librariesIcon.innerHTML = '<div class="icon"><img src="' + map_icons_path + 'libraries-panel-icon.png" alt=""></div><span>libraries</span>';
            librariesUI.appendChild(librariesIcon);

            // Set CSS for pharmacies POI
            var pharmaciesUI = document.createElement('div');
            pharmaciesUI.id = 'pharmacies';
            pharmaciesUI.title = 'pharmacies';
            controlDiv.appendChild(pharmaciesUI);
            var pharmaciesIcon = document.createElement('div');
            pharmaciesIcon.id = 'pharmaciesIcon';
            pharmaciesIcon.innerHTML = '<div class="icon"><img src="' + map_icons_path + 'pharmacy-panel-icon.png" alt=""></div><span>pharmacies</span>';
            pharmaciesUI.appendChild(pharmaciesIcon);

            // Set CSS for hospitals POI
            var hospitalsUI = document.createElement('div');
            hospitalsUI.id = 'hospitals';
            hospitalsUI.title = 'hospitals';
            controlDiv.appendChild(hospitalsUI);
            var hospitalsIcon = document.createElement('div');
            hospitalsIcon.id = 'hospitalsIcon';
            hospitalsIcon.innerHTML = '<div class="icon"><img src="' + map_icons_path + 'hospital-panel-icon.png" alt=""></div><span>hospitals</span>';
            hospitalsUI.appendChild(hospitalsIcon);

            transportationUI.addEventListener('click', function () {
                var transportationUI_ = this;
                if ($(this).hasClass('active')) {
                    $(this).removeClass('active');

                    $scope.tooglePOIs(poiMap, transportationsMarkers);
                } else {
                    $(this).addClass('active');

                    $scope.getPOIs(center, poiMap, 'transportations');
                    $scope.tooglePOIs(poiMap, transportationsMarkers);
                }
                google.maps.event.addListener(poiMap, 'bounds_changed', function () {
                    if ($(transportationUI_).hasClass('active')) {
                        var newCenter = poiMap.getCenter();
                        $scope.getPOIs(newCenter, poiMap, 'transportations');
                    }
                });
            });
            supermarketsUI.addEventListener('click', function () {
                var supermarketsUI_ = this;
                if ($(this).hasClass('active')) {
                    $(this).removeClass('active');

                    $scope.tooglePOIs(poiMap, supermarketsMarkers);
                } else {
                    $(this).addClass('active');

                    $scope.getPOIs(center, poiMap, 'supermarkets');
                    $scope.tooglePOIs(poiMap, supermarketsMarkers);
                }
                google.maps.event.addListener(poiMap, 'bounds_changed', function () {
                    if ($(supermarketsUI_).hasClass('active')) {
                        var newCenter = poiMap.getCenter();
                        $scope.getPOIs(newCenter, poiMap, 'supermarkets');
                    }
                });
            });
            schoolsUI.addEventListener('click', function () {
                var schoolsUI_ = this;
                if ($(this).hasClass('active')) {
                    $(this).removeClass('active');

                    $scope.tooglePOIs(poiMap, schoolsMarkers);
                } else {
                    $(this).addClass('active');

                    $scope.getPOIs(center, poiMap, 'schools');
                    $scope.tooglePOIs(poiMap, schoolsMarkers);
                }
                google.maps.event.addListener(poiMap, 'bounds_changed', function () {
                    if ($(schoolsUI_).hasClass('active')) {
                        var newCenter = poiMap.getCenter();
                        $scope.getPOIs(newCenter, poiMap, 'schools');
                    }
                });
            });
            librariesUI.addEventListener('click', function () {
                var librariesUI_ = this;
                if ($(this).hasClass('active')) {
                    $(this).removeClass('active');

                    $scope.tooglePOIs(poiMap, librariesMarkers);
                } else {
                    $(this).addClass('active');

                    $scope.getPOIs(center, poiMap, 'libraries');
                    $scope.tooglePOIs(poiMap, librariesMarkers);
                }
                google.maps.event.addListener(poiMap, 'bounds_changed', function () {
                    if ($(librariesUI_).hasClass('active')) {
                        var newCenter = poiMap.getCenter();
                        $scope.getPOIs(newCenter, poiMap, 'libraries');
                    }
                });
            });
            pharmaciesUI.addEventListener('click', function () {
                var pharmaciesUI_ = this;
                if ($(this).hasClass('active')) {
                    $(this).removeClass('active');

                    $scope.tooglePOIs(poiMap, pharmaciesMarkers);
                } else {
                    $(this).addClass('active');

                    $scope.getPOIs(center, poiMap, 'pharmacies');
                    $scope.tooglePOIs(poiMap, pharmaciesMarkers);
                }
                google.maps.event.addListener(poiMap, 'bounds_changed', function () {
                    if ($(pharmaciesUI_).hasClass('active')) {
                        var newCenter = poiMap.getCenter();
                        houzezGetPOIs(newCenter, poiMap, 'pharmacies');
                    }
                });
            });
            hospitalsUI.addEventListener('click', function () {
                var hospitalsUI_ = this;
                if ($(this).hasClass('active')) {
                    $(this).removeClass('active');

                    $scope.tooglePOIs(poiMap, hospitalsMarkers);
                } else {
                    $(this).addClass('active');

                    $scope.getPOIs(center, poiMap, 'hospitals');
                    $scope.tooglePOIs(poiMap, hospitalsMarkers);
                }
                google.maps.event.addListener(poiMap, 'bounds_changed', function () {
                    if ($(hospitalsUI_).hasClass('active')) {
                        var newCenter = poiMap.getCenter();
                        $scope.getPOIs(newCenter, poiMap, 'hospitals');
                    }
                });
            });
        }

        $scope.setPOIControls = function (poiMap, center) {
            var poiControlDiv = document.createElement('div');
            var poiControl = $scope.poiControls(poiControlDiv, poiMap, center);

            poiControlDiv.index = 1;
            poiControlDiv.style['padding-left'] = '10px';
            poiMap.controls[google.maps.ControlPosition.LEFT_BOTTOM].push(poiControlDiv);
        }

        $scope.search = function () {
            $scope.FilterRoom();
        }
        $scope.FilterRoom = function () {
            var myBlockUI = blockUI.instances.get('BlockUIMap');
            myBlockUI.start();
            var config = {
                params: {
                    RoomTypeID: $scope.searchInfo.roomtype.toString(),
                    PriceFrom: $scope.sliderFrice.minValue * 1000000,
                    PriceTo: $scope.sliderFrice.maxValue * 1000000,
                    WardID: $scope.searchInfo.wardID,
                    DistrictID: $scope.searchInfo.districtID,
                    ProvinceID: $scope.searchInfo.provinceId,
                    Keywords: null,
                    StartDate: null,
                    EndDate: null,
                    Status: true,
                    page: 1,
                    pageSize: 10,
                    sort: ""
                }
            }
            apiService.get('api/room/getallroomfullsearch', config, function (respone) {
                $scope.data.listRoom = respone.data.items;
                $scope.setPOIControls($scope.map, $scope.map.getCenter());
                $scope.AddMarkers(respone.data.items,$scope.map)
                myBlockUI.stop();
            }, function (respone) {
                myBlockUI.stop();
                BaseService.displayError("Không lấy được dữ liệu phòng", 3000);
            });
        }
        $scope.FilterRoom();
    }
})(angular.module('myApp'));