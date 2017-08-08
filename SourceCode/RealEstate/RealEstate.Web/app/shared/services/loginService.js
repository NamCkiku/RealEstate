﻿(function (app) {
    'use strict';
    app.service('loginService', ['$http', '$q', 'authenticationService', 'authData', 'apiService', '$rootScope',
        function ($http, $q, authenticationService, authData, apiService, $rootScope) {
            var userInfo;
            var deferred;

            this.login = function (userName, password) {
                deferred = $q.defer();
                var data = "grant_type=password&username=" + userName + "&password=" + password;
                $http.post($rootScope.baseUrl + '/api/oauth/token', data, {
                    headers:
                    { 'Content-Type': 'application/x-www-form-urlencoded' }
                }).then(function (response) {
                    userInfo = {
                        accessToken: response.data.access_token,
                        username: response.data.username,
                        email: response.data.email,
                        avatar: response.data.avatar,
                        fullName: response.data.fullName,
                        userID: response.data.userID,
                    };
                    authenticationService.setTokenInfo(userInfo);
                    authData.authenticationData.IsAuthenticated = true;
                    authData.authenticationData.accessToken = userInfo.accessToken;
                    authData.authenticationData.userName = userInfo.username;
                    authData.authenticationData.email = userInfo.email;
                    authData.authenticationData.avatar = userInfo.avatar;
                    authData.authenticationData.fullName = userInfo.fullName;
                    authData.authenticationData.userID = userInfo.userID;

                    deferred.resolve(null);
                }, function (err, status) {
                    authData.authenticationData.IsAuthenticated = false;
                    authData.authenticationData.userName = "";
                    deferred.resolve(err);
                })
                return deferred.promise;
            }

            this.logOut = function () {
                apiService.post('api/account/logout', null, function (response) {
                    authenticationService.removeToken();
                    authData.authenticationData.IsAuthenticated = false;
                    authData.authenticationData.userName = "";
                    authData.authenticationData.accessToken = "";

                }, null);

            }
        }]);
})(angular.module('myApp'));