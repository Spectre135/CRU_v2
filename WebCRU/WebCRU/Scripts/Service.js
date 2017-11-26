'use strict';

var app = angular.module("CRUManagement");

//config Header we add AuthToken read from sessionStorage
app.factory('httpRequestInterceptor', function () {
    return {
        request: function (config) {

            config.headers['AuthToken'] = sessionStorage.getItem("AuthToken");
            return config;
        }
    };
});

app.config(function ($httpProvider) {
    $httpProvider.interceptors.push('httpRequestInterceptor');
});

//Services
app.factory('apiService', function ($q, $http) {

    // set BackEnd Url
    var backEndUrl = 'http://localhost:31207';
    var service = {};

    //GET Data 
    service.getData = function (apiUrl,params) {
        var deferred = $q.defer();
        //posivim ekran
        window.onload = grayOut(true);

        $http({
            method: 'GET',
            url: backEndUrl + apiUrl,
            params

        }).success(function (data) {
            deferred.resolve(data);

        }).error(function (response) {
            writeError(response);
            deferred.reject(response);

        }).finally(function () {
            window.onload = grayOut(false);
        });

        return deferred.promise;
    };

    //POST data
    service.postData = function (apiUrl, dto) {
        var deferred = $q.defer();
        window.onload = grayOut(true);

        $http({
            method: 'POST',
            url: backEndUrl + apiUrl,
            data: dto
        })
            .success(function (data) {
                deferred.resolve(data);
            }).error(function (response) {
                writeError(response);
                deferred.reject(response);
            }).finally(function () {
                window.onload = grayOut(false);
            });

        return deferred.promise;
    };

    //error message handling
    function writeError(response) {
        //Remove errors
        window.localStorage.removeItem("error");
        window.localStorage.removeItem("status");

        if (response != null) {
            window.localStorage.setItem('error', response.Message + " " + response.ExceptionMessage);
            window.localStorage.setItem('status', response.status);
        } else {
            window.localStorage.setItem('error', 'Failed to load resource: the server responded with a status of 404 (Not Found)');
        }
    }

    return service;
});

