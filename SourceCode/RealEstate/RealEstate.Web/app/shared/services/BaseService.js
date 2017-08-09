(function (app) {
    var BaseService = function ($rootScope, $http, $q, $filter) {
        function ValidatorForm(form) {
            $(form).formValidation({
                framework: 'bootstrap',
                //icon: {
                //    valid: 'fa fa-check',
                //    invalid: 'fa fa-times',
                //    validating: 'fa fa-refresh'
                //},
                excluded: ':disabled',
                fields: {

                },
            })
                .off('success.form.fv')
                .on('success.form.fv', function (e) {
                    var $form = $(e.target),
                        fv = $(e.target).data('formValidation');
                    fv.defaultSubmit();

                })
                .on('err.field.fv', function (e, data) {
                    if (data.fv.getSubmitButton()) {
                        data.fv.disableSubmitButtons(false);
                    }
                })
                .on('success.field.fv', function (e, data) {
                    if (data.fv.getSubmitButton()) {
                        data.fv.disableSubmitButtons(false);
                    }
                });
        };

        function formatDate(sDate) {
            if (sDate != "" && sDate != undefined) {
                return $filter('jsDate')(sDate, $filter('uppercase')($rootScope.RootScopeDateFormat));
            } else {
                return "";
            }
        }

        function formatMonth(sDate) {
            if (sDate != "" && sDate != undefined) {
                return $filter('jsDate')(sDate, $filter('uppercase')($rootScope.RootScopeMonthFormat));
            } else {
                return "";
            }
        }


        function formatFullDateTime(sDate) {
            if (sDate != "" && sDate != undefined) {
                return $filter('jsDate')(sDate, $filter('uppercase')($rootScope.RootScopeDateFormat) + ' HH:mm');
            } else {
                return "";
            }

        }

        function canculateAgeByDOB(dateString) {
            var today = new Date();
            var birthDate = new Date(dateString);
            var age = today.getFullYear() - birthDate.getFullYear();
            var m = today.getMonth() - birthDate.getMonth();
            if (m < 0 || (m === 0 && today.getDate() < birthDate.getDate())) {
                age--;
            }

            return age;
        }
        //var showCommonDialog = function (msgInfor) {
        //    var defer = $q.defer();
        //    var modalInstance = $modal.open({
        //        animation: true,
        //        size: msgInfor.size,
        //        backdrop: 'static',
        //        windowClass: 'center-modal',
        //        templateUrl: 'NotificationCommonModal.html',
        //        controller: function ($scope, $modalInstance) {
        //            $scope.title = msgInfor.headerText;
        //            $scope.message = msgInfor.bodyMsg;
        //            $scope.showBtnOK = msgInfor.showBtnOK == null ? true : msgInfor.showBtnOK;
        //            $scope.showBtnCancel = msgInfor.showBtnCancel == null ? true : msgInfor.showBtnCancel;
        //            $scope.btnOK = msgInfor.btnOK == null ? 'OK' : msgInfor.btnOK;
        //            $scope.btnCancel = msgInfor.btnCancel == null ? 'Cancel' : msgInfor.btnCancel;
        //            $scope.type = msgInfor.type;
        //            $scope.ok = function () {
        //                //modalInstance.close();
        //                defer.resolve(modalInstance);
        //            };
        //            $scope.cancel = function () {
        //                modalInstance.close();
        //                defer.reject();
        //            };
        //        }
        //    });
        //    return defer.promise;
        //}

        function displaySuccess(message, timeOut) {
            toastr.options = {
                "closeButton": true,
                "debug": false,
                "newestOnTop": false,
                "progressBar": true,
                "positionClass": "toast-top-right",
                "onclick": null,
                "fadeIn": 300,
                "fadeOut": 1000,
                "timeOut": timeOut == null ? 3000 : timeOut,
                "extendedTimeOut": 1000,
                "showMethod": "slideDown",
                "hideMethod": "slideUp"
            };
            toastr.success(message);
        }

        function displayError(error, timeOut) {
            toastr.options = {
                "closeButton": true,
                "debug": false,
                "newestOnTop": false,
                "progressBar": true,
                "positionClass": "toast-top-right",
                "onclick": null,
                "fadeIn": 300,
                "fadeOut": 1000,
                "timeOut": timeOut == null ? 3000 : timeOut,
                "extendedTimeOut": 1000,
                "showMethod": "slideDown",
                "hideMethod": "slideUp"
            };
            if (Array.isArray(error)) {
                error.each(function (err) {
                    toastr.error(err);
                });
            }
            else {
                toastr.error(error);
            }
        }

        function displayWarning(message, timeOut) {
            toastr.options = {
                "closeButton": true,
                "debug": false,
                "newestOnTop": false,
                "progressBar": true,
                "positionClass": "toast-top-right",
                "onclick": null,
                "fadeIn": 300,
                "fadeOut": 1000,
                "timeOut": timeOut == null ? 3000 : timeOut,
                "extendedTimeOut": 1000,
                "showMethod": "slideDown",
                "hideMethod": "slideUp"
            };
            toastr.warning(message);
        }

        function displayInfo(message, timeOut) {
            toastr.options = {
                "closeButton": true,
                "debug": false,
                "newestOnTop": false,
                "progressBar": true,
                "positionClass": "toast-top-right",
                "onclick": null,
                "fadeIn": 300,
                "fadeOut": 1000,
                "timeOut": timeOut == null ? 3000 : timeOut,
                "extendedTimeOut": 1000,
                "showMethod": "slideDown",
                "hideMethod": "slideUp"
            };
            toastr.info(message);
        }


        function getSeoTitle(input) {
            if (input == undefined || input == '')
                return '';
            //Đổi chữ hoa thành chữ thường
            var slug = input.toLowerCase();

            //Đổi ký tự có dấu thành không dấu
            slug = slug.replace(/á|à|ả|ạ|ã|ă|ắ|ằ|ẳ|ẵ|ặ|â|ấ|ầ|ẩ|ẫ|ậ/gi, 'a');
            slug = slug.replace(/é|è|ẻ|ẽ|ẹ|ê|ế|ề|ể|ễ|ệ/gi, 'e');
            slug = slug.replace(/i|í|ì|ỉ|ĩ|ị/gi, 'i');
            slug = slug.replace(/ó|ò|ỏ|õ|ọ|ô|ố|ồ|ổ|ỗ|ộ|ơ|ớ|ờ|ở|ỡ|ợ/gi, 'o');
            slug = slug.replace(/ú|ù|ủ|ũ|ụ|ư|ứ|ừ|ử|ữ|ự/gi, 'u');
            slug = slug.replace(/ý|ỳ|ỷ|ỹ|ỵ/gi, 'y');
            slug = slug.replace(/đ/gi, 'd');
            //Xóa các ký tự đặt biệt
            slug = slug.replace(/\`|\~|\!|\@|\#|\||\$|\%|\^|\&|\*|\(|\)|\+|\=|\,|\.|\/|\?|\>|\<|\'|\"|\:|\;|_/gi, '');
            //Đổi khoảng trắng thành ký tự gạch ngang
            slug = slug.replace(/ /gi, "-");
            //Đổi nhiều ký tự gạch ngang liên tiếp thành 1 ký tự gạch ngang
            //Phòng trường hợp người nhập vào quá nhiều ký tự trắng
            slug = slug.replace(/\-\-\-\-\-/gi, '-');
            slug = slug.replace(/\-\-\-\-/gi, '-');
            slug = slug.replace(/\-\-\-/gi, '-');
            slug = slug.replace(/\-\-/gi, '-');
            //Xóa các ký tự gạch ngang ở đầu và cuối
            slug = '@' + slug + '@';
            slug = slug.replace(/\@\-|\-\@|\@/gi, '');

            return slug;
        }
        function getTree(data, options) {
            options = options || {};
            var ID_KEY = options.idKey;
            var PARENT_KEY = options.parentKey;
            var CHILDREN_KEY = options.childrenKey || 'children';

            var tree = [],
                childrenOf = {};
            var item, id, parentId;

            for (var i = 0, length = data.length; i < length; i++) {
                item = data[i];
                id = item[ID_KEY];
                parentId = item[PARENT_KEY] || null;
                // every item may have children
                childrenOf[id] = childrenOf[id] || [];
                // init its children
                item[CHILDREN_KEY] = childrenOf[id];
                if (parentId != null) {
                    // init its parent's children object
                    childrenOf[parentId] = childrenOf[parentId] || [];
                    // push it into its parent's children object
                    childrenOf[parentId].push(item);
                } else {
                    tree.push(item);
                }
            };

            return tree;
        }
        return {
            ValidatorForm: ValidatorForm,
            canculateAgeByDOB: canculateAgeByDOB,
            formatDate: formatDate,
            formatMonth: formatMonth,
            formatFullDateTime: formatFullDateTime,
            //showCommonDialog: showCommonDialog,
            displaySuccess: displaySuccess,
            displayError: displayError,
            displayWarning: displayWarning,
            displayInfo: displayInfo,
            getSeoTitle: getSeoTitle,
            getTree: getTree,
        };
    }
    BaseService.$inject = ['$rootScope', '$http', '$q', '$filter'];
    app.service('BaseService', BaseService);
})(angular.module('myApp'));