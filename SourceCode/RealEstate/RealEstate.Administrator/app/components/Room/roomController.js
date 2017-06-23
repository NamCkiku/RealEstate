(function (app) {
    app.controller('roomController', roomController);

    roomController.$inject = ['$scope', '$modal', 'BaseService', 'apiService', '$rootScope', '$window', '$timeout', 'ENUMS', 'blockUI', '$filter'];

    function roomController($scope, $modal, BaseService, apiService, $rootScope, $window, $timeout, ENUMS, blockUI, $filter) {
        $scope.enums = ENUMS;
        $scope.enums = ENUMS;
        $scope.pageSize = 10;
        $scope.filter = {
            Keywords: "",
            StartDate: "",
            EndDate: "",
            searchByStartDate: true,
            searchByEndDate: true,
            Status: true,
            RoomTypeID: "",
            ProvinceID: "",
            DistrictID: "",
            lstWard: "",
        }
        $scope.data = {
            lstRoomType: [],
            lstProvince: [],
            lstDistrict: [],
            lstWard: [],
        }
        $scope.lstRoomTypeTree = [];
        $scope.GetAllRoomType = function () {
            var myBlockUI = blockUI.instances.get('BlockUIRoom');
            myBlockUI.start();
            apiService.post('RoomType/LoadAllRoomType', true, null, function (respone) {
                if (respone.data.success == true) {
                    $scope.lstRoomTypeTree = respone.data.lstData;
                    $scope.lstRoomTypeComboboxTree = BaseService.getTree($scope.lstRoomTypeTree, { idKey: 'ID', parentKey: 'ParentID' });
                    $scope.lstRoomTypeComboboxTree.forEach(function (item) {
                        recur(item, 0, $scope.data.lstRoomType);
                    });
                    $scope.GetAllProvince();
                    myBlockUI.stop();
                } else {
                    myBlockUI.stop();
                    BaseService.displayError("Không lấy được dữ liệu Loại Phòng", 3000);
                }
            }, function (respone) {
                myBlockUI.stop();
                BaseService.displayError("Không lấy được dữ liệu Loại Phòng", 3000);
            });
        }
        $scope.isDistrict = true;
        $scope.isWard = true;
        $scope.GetAllProvince = function () {
            var myBlockUI = blockUI.instances.get('BlockUIRoom');
            myBlockUI.start();
            apiService.post('Management/LoadAllProvince', true, null, function (respone) {
                if (respone.data.success == true) {
                    $scope.data.lstProvince = respone.data.lstData;
                    console.log($scope.data.lstProvince);
                    myBlockUI.stop();
                } else {
                    myBlockUI.stop();
                    BaseService.displayError("Không lấy được dữ liệu Tỉnh Thành", 3000);
                }
            }, function (respone) {
                myBlockUI.stop();
                BaseService.displayError("Không lấy được dữ liệu Tỉnh Thành", 3000);
            });
        }
        $scope.GetAllDistrict = GetAllDistrict;
        function GetAllDistrict(id) {
            var myBlockUI = blockUI.instances.get('BlockUIRoom');
            myBlockUI.start();
            apiService.post('Management/LoadAllDistrict', true, null, function (respone) {
                if (respone.data.success == true) {
                    $scope.data.lstDistrict = $filter('filter')(respone.data.lstData, { ProvinceId: id }, true);
                    $scope.isDistrict = false;
                    myBlockUI.stop();
                } else {
                    myBlockUI.stop();
                    BaseService.displayError("Không lấy được dữ liệu Quận huyện", 3000);
                }
            }, function (respone) {
                myBlockUI.stop();
                BaseService.displayError("Không lấy được dữ liệu Quận huyện", 3000);
            });
        }
        $scope.GetAllWard = GetAllWard;
        function GetAllWard(id) {
            apiService.post('Management/LoadAllWard', true, null, function (respone) {
                if (respone.data.success == true) {
                    $scope.data.lstWard = $filter('filter')(respone.data.lstData, { DistrictID: id }, true);
                    $scope.isWard = false;
                } else {
                    myBlockUI.stop();
                    BaseService.displayError("Không lấy được dữ liệu xã phường", 3000);
                }
            }, function (respone) {
                myBlockUI.stop();
                BaseService.displayError("Không lấy được dữ liệu xã phường", 3000);
            });
        }
        $scope.GetAllRoomType();

        function filterData() {
            var myBlockUI = blockUI.instances.get('BlockUIRoom');
            myBlockUI.start();
            $scope.mainGridOptions = {
                transport: {
                    read: function (options) {
                        if ($scope.filter.StartDate == null) {
                            $scope.filter.StartDate = '';

                        }

                        if ($scope.filter.StartDate == null) {
                            $scope.filter.StartDate = '';
                        }
                        var startDate;
                        var endDate;
                        if ($scope.filter.searchByStartDate == false) {
                            startDate = '';
                        }
                        if ($scope.filter.searchByStartDate == true) {
                            startDate = $scope.filter.StartDate;
                        }
                        if ($scope.filter.searchByEndDate == false) {
                            endDate = '';
                        }
                        if ($scope.filter.searchByEndDate == true) {
                            endDate = $scope.filter.EndDate;
                        }
                        var filter = {
                            Keywords: $scope.filter.Keywords,
                            StartDate: startDate,
                            EndDate: endDate,
                            Status: $scope.filter.Status,
                            Province: $scope.filter.ProvinceID,
                            District: $scope.filter.DistrictID,
                            Ward: $scope.filter.WardID,
                            RoomType: $scope.filter.RoomTypeID,
                            page: options.data.page - 1,
                            pageSize: options.data.pageSize
                        }
                        apiService.post('Room/LoadAllRoomPaging', true, filter, function (respone) {
                            if (respone.data.success == true) {
                                options.success(respone.data.lstData);
                                console.log(respone.data.lstData)
                                myBlockUI.stop();
                            } else {
                            }
                        }, function (respone) {
                            console.log('Load product failed.');
                            options.error(respone.data);
                            myBlockUI.stop();
                        });
                    }
                },
                //group: [{ field: "ProvinceName" }, { field: "RoomTypeName" }],
                serverPaging: true,
                sortable: true,
                selectable: "multiple",
                pageable: {
                    refresh: true,
                    pageSizes: true,
                    messages: {
                        last: "Trước",
                        empty: "Không có bản ghi nào",
                        display: "Từ {0} đến {1} trong tổng {2} bản ghi",
                        allPages: "Tất cả",
                        page: "Trang",
                        of: "tổng {0}",
                        itemsPerPage: "Số bản ghi trên mỗi trang",
                        first: "Chuyển trang tiếp theo",
                        previous: "Chuyển về trang trước",
                        next: "Về trang đầu tiên",
                        last: "Tới trang cuối",
                        refresh: "Làm mới"
                    }
                },
                groupable: true,
                filterable: true,
                columnMenu: {
                    messages: {
                        sortAscending: "Sắp xếp tăng dần",
                        sortDescending: "Sắp xếp giảm dần",
                        filter: "Bộ lọc",
                        columns: "Cột"
                    }
                },
                reorderable: true,
                resizable: true,
                sortable: {
                    mode: "single",
                    allowUnsort: false
                },
                pageSize: 5,
                schema: {
                    data: "Items",
                    total: "TotalCount"
                }
            };
        };
        $scope.gridColumns = [
            //{
            //    field: "Image", title: "Ảnh",
            //    width: "60px",
            //    template: "# if (Image != '')" + "{#<div class='customer-photo'" +
            //    "style='background-image: url(http://localhost:6568/Content/images/#:data.Image#);'></div>#}#",
            //},
            { field: "RoomName", title: "Tên Phòng" },
            { field: "Address", title: "Địa Chỉ" },
            {
                field: "Phone", title: "Số điện thoại",
            },
            {
                field: "Price", title: "Gía tiền(vnđ)",
                template: "#=Price == null ? '' : kendo.toString(Price, 'n0') #" + " (Vnđ)"
            },
            {
                field: "Acreage", title: "Diện tích(m2)",
                template: "#=Acreage == null ? '' : kendo.toString(Acreage, 'n0') #" + "<span style=\"margin-left:5px;\">m<sup>2</sup></span>"
            },
            //{
            //    field: "UserAvatar", title: "Người đăng",
            //    template: "# if (UserAvatar != null)"
            //    + "{#<div class='customer-photo'"
            //    + "style='background-image: url(#:data.UserAvatar#);'></div>"
            //    + "<div class='customer-name'>#: FullName #</div>#}#"
            //    + "# if(UserAvatar ==null)"
            //    + "{#<div class='customer-photo'"
            //    + "style='background-image: url(http://localhost:15144/Content/img/boy-512.png);'></div>"
            //    + "<div class='customer-name'>#: FullName #</div>#}#",
            //},
            //{ field: "RoomTypeName", title: "Loại phòng" },
            {
                field: "CreateDate", title: "Ngày đăng",
                template: "#=CreateDate == null ? '' : kendo.toString(kendo.parseDate(CreateDate, 'yyyy-MM-dd'), '" + $rootScope.RootScopeDateFormat + "') #"
            },

            //{ field: "ProvinceName", title: "Tỉnh/Thành phố" },
            {
                field: "", title: "Chức năng",
                width: "200px",
                template: "<button class=\"btn btn-xs btn-primary\" ng-click=\"openModal(this.dataItem);\" style=\"margin-right:5px;\"><i style=\"margin-right:5px;\" class=\"fa fa-eye\" aria-hidden=\"true\"></i>Xem</button>" +
                //"<button class=\"btn btn-xs btn-info\" style=\"margin-right:5px;\"><i style=\"margin-right:5px;\" class=\"fa fa-pencil-square-o\" aria-hidden=\"true\"></i>Sửa</button>" +
                "<button class=\"btn btn-xs btn-danger\" style=\"margin-right:5px;\"><i style=\"margin-right:5px;\" class=\"fa fa-trash-o\" aria-hidden=\"true\"></i>Xóa</button>"

            },
        ];
        $scope.Search = Search;
        function Search() {
            filterData();
        }
        $scope.Search();
        function times(n, str) {
            var result = '';
            for (var i = 0; i < n; i++) {
                result += str;
            }
            return result;
        };
        function recur(item, level, arr) {
            arr.push({
                Name: times(level, '–') + ' ' + item.RoomTypeName,
                ID: item.ID,
                Level: level,
                Indent: times(level, '–')
            });
            if (item.children) {
                item.children.forEach(function (item) {
                    recur(item, level + 1, arr);
                });
            }
        };
    }
})(angular.module('myApp'));