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
    
    function nullToUndefined (input) {
        if (input === null || input === '') {
            return 'undefined';
        }

        return input;
    };

    //Get Data generic
    service.getData = function (url) {
        var deferred = $q.defer();
        //posivim ekran
        window.onload = grayOut(true);

        $http({
            method: 'GET',
            url: backEndUrl + url

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

    //Get aplikacije
    service.getAplikacije = function (searchString, pageIndex, pageSizeSelected, sortKey, asc) {
        var deferred = $q.defer();
        //posivim ekran
        window.onload = grayOut(true);

        $http({
            method: 'GET',
            url: backEndUrl + '/api/aplikacije2/' + nullToUndefined(searchString),
            params: {
                pageIndex: pageIndex,
                pageSizeSelected: pageSizeSelected.selected,
                sortKey: sortKey,
                asc: asc
            }

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

    //Get VlogePravice
    service.getVlogePravice = function (aplikacijaKLJ,vlogaKLJ) {
        var deferred = $q.defer();
        //posivim ekran
        window.onload = grayOut(true);

        $http({
            method: 'GET',
            url: backEndUrl + '/api/pravice/',
            params: {
                aplikacijaKLJ: aplikacijaKLJ,
                vlogaKLJ: vlogaKLJ
            }

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

    //Save data
    service.saveData = function (apiUrl,dto) {
        window.onload = grayOut(true);

        $http({
            method: 'POST',
            //url: backEndUrl + '/api/aplikacije/save/',
            url : backEndUrl + apiUrl,
            data: dto

        }).success(function (data) {
            window.localStorage.setItem('status', status);

        }).error(function (response) {
            writeError(response);

        }).finally(function () {
            window.onload = grayOut(false);
        });

    };

    //Delete record
    service.deleteRecord = function (dto) {
        window.onload = grayOut(true);

        $http({
            method: 'POST',
            //url: backEndUrl + '/api/aplikacije/delete/',
            url: backEndUrl + apiUrl,
            data: dto

        }).success(function (data) {
            window.localStorage.setItem('status', status);

        }).error(function (response) {
            writeError(response);

        }).finally(function () {
            window.onload = grayOut(false);
        });

    };

    //Šifranti
    service.getSifranti = function (id) {
        var deferred = $q.defer();
        window.onload = grayOut(true);
        $http({
            method: 'GET',
            url: backEndUrl + '/api/sifranti/' + id
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

