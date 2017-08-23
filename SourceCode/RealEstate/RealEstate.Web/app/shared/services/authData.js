(function (app) {
    'use strict';
    app.factory('authData', [function () {
        var authDataFactory = {};

        var authentication = {
            IsAuthenticated: false,
            userName: "",
            email: "",
            avatar: "",
            fullName: "",
            userID: "",
            address: "",
            phonenumber: "",
        };
        authDataFactory.authenticationData = authentication;

        return authDataFactory;
    }]);
})(angular.module('myApp'));
