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
                        }
                        apiService.post('RoomType/LoadAllRoomType', false, filter, function (respone) {
                            if (respone.data.success) {
                                options.success(respone.data);
                                console.log(respone.data)
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
                    pageSizes: true
                },
                filterable: true,
                columnMenu: true,
                reorderable: true,
                resizable: true,
                toolbar: ["excel"],
                excel: {
                    fileName: "Kendo UI Grid Export.xlsx",
                    proxyURL: "https://demos.telerik.com/kendo-ui/service/export",
                    filterable: true
                },
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
                        },
                        {
                            field: "CreatedDate", title: "Ngày đăng",
                            template: "#=CreatedDate == null ? '' : kendo.toString(kendo.parseDate(CreatedDate, 'yyyy-MM-dd'), '" + $rootScope.RootScopeDateFormat + "') #"
                        },
                       {
                           field: "", title: "Chức năng",
                           width: "200px",
                           template: "<button class=\"btn btn-xs btn-primary\" ng-click=\"openModal(this.dataItem);\" style=\"margin-right:5px;\"><i style=\"margin-right:5px;\" class=\"fa fa-eye\" aria-hidden=\"true\"></i>Xem</button>" +
                                     //"<button class=\"btn btn-xs btn-info\" style=\"margin-right:5px;\"><i style=\"margin-right:5px;\" class=\"fa fa-pencil-square-o\" aria-hidden=\"true\"></i>Sửa</button>" +
                                     "<button class=\"btn btn-xs btn-danger\" style=\"margin-right:5px;\"><i style=\"margin-right:5px;\" class=\"fa fa-trash-o\" aria-hidden=\"true\"></i>Xóa</button>"

                       },
        ];
        $scope.Search = function () {
            $scope.filterData();
        }
        $scope.Search();
        $scope.openModalRoomType = function () {
            $scope.modalInstance = $modal.open({
                animation: true,
                templateUrl: 'ModalRoomType.html',
                backdrop: 'static',
                windowClass: 'center-modal',
                scope: $scope,
                keyboard: false,
                size: 'lg'
            });
        }
        $scope.ok = function () {
            $scope.modalInstance.dismiss('cancel');
        };
        $scope.close = function () {
            $scope.modalInstance.dismiss('cancel');
        };
        //var myBlockUI = blockUI.instances.get('BlockUIRoomType');
        //myBlockUI.start();
    }
})(angular.module('myApp'));