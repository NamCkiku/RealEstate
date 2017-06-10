(function (app) {
    app.factory('apiService', apiService);

    apiService.$inject = ['$http', 'BaseService', '$rootScope'];

    function apiService($http, BaseService, $rootScope) {
        var baseUrl = $rootScope.baseUrl;
        return {
            get: get,
            post: post,
            put: put,
            del: del
        }
        function del(url, isValidateToken, data, success, failure) {
            if (isValidateToken) {
                $http.delete(baseUrl + url, data, { headers: { 'RequestVerificationToken': $(':input:hidden[id*="antiForgeryToken"]').val() } }).then(function (result) {
                    success(result);
                }, function (error) {
                    console.log(error.status)
                    if (error.status === 401) {
                        BaseService.displayError('Authenticate is required.');
                    }
                    else if (failure != null) {
                        failure(error);
                    }

                });
            }
            else {
                $http.delete(baseUrl + url, data).then(function (result) {
                    success(result);
                }, function (error) {
                    console.log(error.status)
                    if (error.status === 401) {
                        notificationService.displayError('Authenticate is required.');
                    }
                    else if (failure != null) {
                        failure(error);
                    }

                });
            }

        }
        function post(url, isValidateToken, data, success, failure) {
            if (isValidateToken) {
                $http.post(baseUrl + url, data, { headers: { 'RequestVerificationToken': $(':input:hidden[id*="antiForgeryToken"]').val() } }).then(function (result) {
                    success(result);
                }, function (error) {
                    console.log(error.status)
                    if (error.status === 401) {
                        BaseService.displayError('Authenticate is required.');
                    }
                    else if (failure != null) {
                        failure(error);
                    }

                });
            }
            else {
                $http.post(baseUrl + url, data).then(function (result) {
                    success(result);
                }, function (error) {
                    console.log(error.status)
                    if (error.status === 401) {
                        BaseService.displayError('Authenticate is required.');
                    }
                    else if (failure != null) {
                        failure(error);
                    }

                });
            }
        }
        function put(url, isValidateToken, data, success, failure) {
            if (isValidateToken) {
                $http.put(baseUrl + url, data, { headers: { 'RequestVerificationToken': $(':input:hidden[id*="antiForgeryToken"]').val() } }).then(function (result) {
                    success(result);
                }, function (error) {
                    console.log(error.status)
                    if (error.status === 401) {
                        BaseService.displayError('Authenticate is required.');
                    }
                    else if (failure != null) {
                        failure(error);
                    }

                });
            }
            else {
                $http.put(baseUrl + url, data).then(function (result) {
                    success(result);
                }, function (error) {
                    console.log(error.status)
                    if (error.status === 401) {
                        BaseService.displayError('Authenticate is required.');
                    }
                    else if (failure != null) {
                        failure(error);
                    }

                });
            }
        }
        function get(url, isValidateToken, params, success, failure) {
            if (isValidateToken) {
                $http.get(baseUrl + url, params, { headers: { 'RequestVerificationToken': $(':input:hidden[id*="antiForgeryToken"]').val() } }).then(function (result) {
                    success(result);
                }, function (error) {
                    failure(error);
                });
            }
            else {
                $http.get(baseUrl + url, params).then(function (result) {
                    success(result);
                }, function (error) {
                    failure(error);
                });
            }

        }
    }
})(angular.module('myApp'));