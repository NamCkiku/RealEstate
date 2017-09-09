(function (app) {
    app.controller('MapRoomController', MapRoomController);

    MapRoomController.$inject = ['$scope', 'BaseService', 'apiService', '$rootScope', '$window', '$timeout', 'blockUI', '$modal', '$log', 'authData', 'authenticationService', 'loginService', '$filter', '$sce'];

    function MapRoomController($scope, BaseService, apiService, $rootScope, $window, $timeout, blockUI, $modal, $log, authData, authenticationService, loginService, $filter, $sce) {
        $scope.baseUrl = $rootScope.baseUrl;
        $scope.markers = [];
        $scope.current_marker = 0;
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
            maxWidth: 200,
            alignBottom: true,
            boxStyle: {
                opacity: 1,
                width: "300px",
                height: "285px"
            },
            pixelOffset: new google.maps.Size(-135, -43),
            zIndex: 999999,
            closeBoxMargin: "0 0 -15px 0",
            closeBoxURL: 'http://multimonitorcomputer.com/images/icon-close.png',
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
            if (props.length == 0) {
                for (var i = 0; i < $scope.markers.length; i++) {
                    $scope.markers[i].setMap(null);
                }
                google.maps.event.trigger($scope.infobox, 'closeclick');
            }
            else {
                $.each(props, function (i, prop) {
                    var latlng = new google.maps.LatLng(prop.lat, prop.lng);
                    var marker_url = '/Content/img/iconmarker.png';
                    var marker_size = new google.maps.Size(44, 56);
                    var marker_icon = {
                        url: marker_url,
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
                    infoboxContent.className = 'box-room-info item-grid map-info-box';
                    infoboxContent.innerHTML = '<div class="col-lg-12 no-touch">' +
                        '<a href="/chi-tiet-phong/' + prop.alias + '-' + prop.id + '" class="card" id="card-309">' +
                        ' <div class="figure">' +
                        '<div class="featured-label">' +
                        '<div class="featured-label-left"></div>' +
                        '<div class="featured-label-content"><span class="fa fa-star"></span></div>' +
                        '<div class="featured-label-right"></div>' +
                        '<div class="clearfix"></div>' +
                        '</div>' +
                        '<div class="img" style="background-image:url(' + $scope.baseUrl + '' + prop.thumbnailImage + ');"></div>' +
                        '<div class="figCaption">' +
                        '<div><span class="fa fa-home" style="margin-right:10px"></span>' + prop.roomTypeName + '</div>' +
                        '<span><span class="fa fa-eye"></span>' + prop.viewCount + '</span>' +
                        '<span><span class="fa fa-heart-o"></span> 2</span>' +
                        '</div>' +
                        '<div class="figView"><span class="fa fa-eye"></span></div>' +
                        '<div class="figType">' + $filter('currency')(prop.price, "", 0) + ' đ/tháng</div>' +
                        '</div>' +
                        '<h2 title="Cho thuê phòng trọ tại Thái Thịnh Hà Nội">' + prop.roomName + '</h2>' +
                        '<div class="cardAddress">' +
                        '<span class="fa fa-map-marker" style="margin-right:10px;"></span>' + prop.address + '' +
                        '</div>' +
                        '<ul class="cardFeat">' +
                        '<li><i class="fa fa-clock-o" style="margin-right:5px;" aria-hidden="true"></i><span>' + $filter('timeago')(prop.createdDate, true) + '</span></li>' +
                        '<li><span class="fa fa-info"></span><span style="margin-left:5px;">' + prop.acreage + ' m<sup>2</sup></span></li>' +
                        '<li><span class="fa fa-male"></span><span style="margin-left:5px;">' + prop.userName + '</span></li>' +
                        '</ul>' +
                        '<div class="clearfix"></div>' +
                        '</a>' +
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
                var markerCluster = new MarkerClusterer(map, $scope.markers,
                    {
                        maxZoom: 14,
                        gridSize: 60,
                        styles: [
                            {
                                url: 'http://houzez01.favethemes.com/wp-content/themes/houzez/images/map/cluster-icon.png',
                                width: 48,
                                height: 48,
                                textColor: "#fff"
                            }
                        ]
                    });
            }

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


        $scope.nextRoomMap = function () {
            $scope.current_marker++;
            if ($scope.current_marker > $scope.markers.length) {
                $scope.current_marker = $scope.markers.length;
            }
            while ($scope.markers[$scope.current_marker - 1].visible === false) {
                $scope.current_marker++;
                if ($scope.current_marker > $scope.markers.length) {
                    $scope.current_marker = 1;
                }
            }
            if ($scope.map.getZoom() < 15) {
                $scope.map.setZoom(15);
            }
            google.maps.event.trigger($scope.markers[$scope.current_marker - 1], 'click');
        }

        $scope.previousRoomMap = function () {
            $scope.current_marker--;
            if ($scope.current_marker < 1) {
                $scope.current_marker = $scope.markers.length;
            }
            while ($scope.markers[$scope.current_marker - 1].visible === false) {
                $scope.current_marker--;
                if ($scope.current_marker > markers.length) {
                    $scope.current_marker = 1;
                }
            }
            if ($scope.map.getZoom() < 15) {
                $scope.map.setZoom(15);
            }
            google.maps.event.trigger($scope.markers[$scope.current_marker - 1], 'click');
        }

        $scope.GeoLocation = function () {

            // get my location useing HTML5 geolocation

            var googleGeoProtocol = true;
            var isChrome = !!window.chrome && !!window.chrome.webstore;

            if (isChrome) {

                if (document.location.protocol === 'http:') {

                    googleGeoProtocol = false;

                }

            }

            if (googleGeoProtocol) {

                if (navigator.geolocation) {

                    navigator.geolocation.getCurrentPosition(function (position) {

                        var pos = {
                            lat: position.coords.latitude,
                            lng: position.coords.longitude
                        };

                        var geocoder = new google.maps.Geocoder;
                        //var infowindow = new google.maps.InfoWindow;

                        // var latLng   = new google.maps.LatLng( position.coords.latitude, position.coords.longitude );

                        geocoder.geocode({ 'location': pos }, function (results, status) {
                            if (status === 'OK') {
                                if (results[1]) {
                                    console.log(results[1]);
                                    // map.setZoom(11);
                                    var marker = new google.maps.Marker({
                                        position: pos,
                                        map: $scope.map
                                    });
                                    /*infowindow.setContent(results[1].formatted_address);
                                    infowindow.open(map, marker);*/
                                } else {
                                    window.alert('No results found');
                                }
                            } else {
                                window.alert('Geocoder failed due to: ' + status);
                            }
                        });


                        // alert( 'icon : ' + clusterIcon );

                        var circle = new google.maps.Circle({
                            strokeColor: '#67cfd8',
                            strokeOpacity: 0.6,
                            strokeWeight: 1,
                            fillColor: '#67cfd8',
                            fillOpacity: 0.2,
                            center: $scope.myLatlng,
                            map: $scope.map,
                            radius: Math.sqrt(2500000)
                        });

                        // circle.bindTo('center', marker, 'position');
                        $scope.map.fitBounds(circle.getBounds());
                        // map.setCenter(pos);

                    }, function () {

                        handleLocationError(true, $scope.map, $scope.map.getCenter());

                    });

                }

            } else {

                $.getJSON('http://ipinfo.io', function (data) {
                    // console.log(data);
                    var localtion = data.loc;
                    var localtion = localtion.split(",");

                    var localtion = {
                        lat: localtion[0] * 1,
                        lng: localtion[1] * 1
                    };

                    var circle = new google.maps.Circle({
                        strokeColor: '#67cfd8',
                        strokeOpacity: 0.6,
                        strokeWeight: 1,
                        fillColor: '#67cfd8',
                        fillOpacity: 0.2,
                        center: $scope.myLatlng,
                        map: $scope.map,
                        radius: Math.sqrt(2500000)
                    });

                    // circle.bindTo('center', marker, 'position');
                    $scope.map.fitBounds(circle.getBounds());

                    var marker = new google.maps.Marker({
                        position: localtion,
                        animation: google.maps.Animation.DROP,
                        // icon: clusterIcon,
                        map: $scope.map
                    });
                    $scope.map.setCenter(localtion);
                });

            }

        }

        $scope.search = function () {
            $scope.FilterRoom();
        }
        $scope.FilterRoom = function () {
            var myBlockUI = blockUI.instances.get('BlockUIMap');
            myBlockUI.start();
            var config = {
                params: {
                    RoomTypeID: $scope.searchInfo.roomtype,
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
                $scope.AddMarkers(respone.data.items, $scope.map);
                if ($scope.data.listRoom.length > 0) {
                    $scope.nextRoomMap();
                }
                myBlockUI.stop();
            }, function (respone) {
                myBlockUI.stop();
                BaseService.displayError("Không lấy được dữ liệu phòng", 3000);
            });
        }
        $scope.FilterRoom();
        $scope.setPOIControls($scope.map, $scope.map.getCenter());
    }
})(angular.module('myApp'));