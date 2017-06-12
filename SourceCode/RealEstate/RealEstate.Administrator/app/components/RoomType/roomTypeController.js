(function (app) {
    app.controller('roomTypeController', roomTypeController);

    roomTypeController.$inject = ['$scope', '$modal', 'BaseService', 'apiService', '$rootScope', '$window', '$timeout', 'ENUMS', 'blockUI'];

    function roomTypeController($scope, $modal, BaseService, apiService, $rootScope, $window, $timeout, ENUMS, blockUI) {
        $scope.enums = ENUMS;
        $scope.pageSize = 10;
        $scope.filter = {
            Keywords: "",
            StartDate: "",
            EndDate: "",
            searchByStartDate: true,
            searchByEndDate: true,
            Status: true
        }
        $scope.data = {
            lstRoomType: [],
        }
        $scope.roomType = {
            isDelete: false,
            Status: true,
            HomeFlag: false,
        }
        $scope.filterData = function () {
            var myBlockUI = blockUI.instances.get('BlockUIRoomType');
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
                            Status: $scope.filter.Status
                        }
                        apiService.post('RoomType/LoadAllRoomType', false, filter, function (respone) {
                            if (respone.data.success) {
                                options.success(respone.data);
                                console.log(respone.data)
                                $scope.lstRoomTypeTree = [];
                                $scope.data.lstRoomType = respone.data.lstData;
                                $scope.data.lstModuleComboboxTree = BaseService.getTree($scope.data.lstRoomType, { idKey: 'ID', parentKey: 'ParentID' });
                                $scope.data.lstModuleComboboxTree.forEach(function (item) {
                                    recur(item, 0, $scope.lstRoomTypeTree);
                                });
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
                pageSize: 5,
                page: 1,
                serverPaging: false,
                serverFiltering: false,
                serverSorting: false,
                schema: {
                    data: "lstData",
                    total: "lstData.length"
                },
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
                }
            };
        };

        $scope.gridColumns = [
                        {
                            field: "DisplayOrder", title: "Thứ tự",
                        },
                        {
                            field: "RoomTypeName", title: "Tên loại phòng",
                        },
                        {
                            field: "ParentID", title: "Cha",
                        },
                        {
                            field: "ImageIcon", title: "Icon",
                            template: "<i class=\"fa #=ImageIcon#\" style=\"margin-right:5px;\" aria-hidden=\"true\"></i>#=ImageIcon#"
                        },
                        {
                            field: "CreatedDate", title: "Ngày đăng",
                            template: "#=CreatedDate == null ? '' : kendo.toString(kendo.parseDate(CreatedDate, 'yyyy-MM-dd'), '" + $rootScope.RootScopeDateFormat + "') #"
                        },
                       {
                           field: "", title: "Chức năng",
                           width: "200px",
                           template: "<button class=\"btn btn-xs btn-primary\" ng-click=\"openModalRoomType(this.dataItem,true);\" style=\"margin-right:5px;\"><i style=\"margin-right:5px;\" class=\"fa fa-eye\" aria-hidden=\"true\"></i>Sửa</button>" +
                                     //"<button class=\"btn btn-xs btn-info\" style=\"margin-right:5px;\"><i style=\"margin-right:5px;\" class=\"fa fa-pencil-square-o\" aria-hidden=\"true\"></i>Sửa</button>" +
                                     "<button class=\"btn btn-xs btn-danger\" style=\"margin-right:5px;\"><i style=\"margin-right:5px;\" class=\"fa fa-trash-o\" aria-hidden=\"true\"></i>Xóa</button>"

                       },
        ];
        $scope.Search = function () {
            $scope.filterData();
        }
        $scope.Search();
        $scope.GetSeoTitle = GetSeoTitle;
        function GetSeoTitle() {
            $scope.roomType.Alias = BaseService.getSeoTitle($scope.roomType.RoomTypeName);
        }
        $scope.openModalRoomType = function (item, isEdit) {
            $scope.isEdit = isEdit;
            $scope.modalInstance = $modal.open({
                animation: true,
                templateUrl: 'ModalRoomType.html',
                backdrop: 'static',
                windowClass: 'center-modal',
                scope: $scope,
                keyboard: false,
                size: 'lg'
            });
            if (isEdit) {
                $scope.roomType = angular.copy(item);
                $scope.roomType.CreatedDate = BaseService.formatDate($scope.roomType.CreatedDate);
                $scope.roomType.UpdatedDate = BaseService.formatDate($scope.roomType.UpdatedDate);
            }
            else {
                $scope.ModuleType = {};
            }
            $scope.ok = function () {
                var myBlockUI = blockUI.instances.get('BlockUIFrmRoomType');
                myBlockUI.start();
                BaseService.ValidatorForm("#frmRoomType");
                var frmAdd = angular.element(document.querySelector('#frmRoomType'));
                var formValidation = frmAdd.data('formValidation').validate();
                if (formValidation.isValid()) {
                    if (isEdit) {
                        $scope.roomType.UpdatedDate = new Date();
                        apiService.post('RoomType/UpdateRoomType', false, $scope.roomType, function (respone) {
                            if (respone.data.success == true) {
                                $scope.Search();
                                BaseService.displaySuccess("Chúc mừng bạn đã sửa thành công");
                                $scope.modalInstance.dismiss('cancel');
                            } else {
                                BaseService.displayError("Thêm không thành công");
                            }
                            myBlockUI.stop();
                        }, function (respone) {
                        });
                    }
                    else {
                        apiService.post('RoomType/InsertRoomType', false, $scope.roomType, function (respone) {
                            if (respone.data.success == true) {
                                $scope.Search();
                                BaseService.displaySuccess("Chúc mừng bạn đã thêm thành công");
                                $scope.modalInstance.dismiss('cancel');
                            } else {
                                BaseService.displayError("Thêm không thành công");
                            }
                            myBlockUI.stop();
                        }, function (respone) {
                        });
                    }
                }
                else {
                    myBlockUI.stop();
                }
            };
            $scope.close = function () {
                $scope.modalInstance.dismiss('cancel');
            };
        }


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