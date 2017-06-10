(function (app) {
    app.filter("formatDate", ['$log', '$filter', '$rootScope', function ($log, $filter, $rootScope) {
        return function (sDate) {
            if (sDate != "" && sDate != undefined) {
                return $filter('jsDate')(sDate, $filter('uppercase')($rootScope.RootScopeDateFormat));
            } else {
                return "";
            }
            return $filter('jsDate')(sDate, $filter('uppercase')($rootScope.RootScopeDateFormat));

        };
    }]);
    app.filter("jsDate", function ($log) {
        return function (x, formatDate) {
            return moment(x).format(formatDate);

        };
    });
})(angular.module('myApp'));