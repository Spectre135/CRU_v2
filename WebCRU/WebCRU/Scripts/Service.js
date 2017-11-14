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
    service.getData = function (url,searchString) {
        var deferred = $q.defer();
        //posivim ekran
        window.onload = grayOut(true);

        $http({
            method: 'GET',
            url: backEndUrl + url + (searchString === null ? 'undefined' : searchString)

        }).success(function (data) {
            deferred.resolve(data);

        }).error(function (response) {
            window.localStorage.setItem('error', response.Message + " " + response.ExceptionMessage);
            window.localStorage.setItem('status', status);
            deferred.reject(response);

        }).finally(function () {
            window.onload = grayOut(false);
        });

        return deferred.promise;
    };

    //Get apikacije
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
            window.localStorage.setItem('error', response.Message + " " + response.ExceptionMessage);
            window.localStorage.setItem('status', status);
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
            window.localStorage.setItem('error', response.Message + " " + response.ExceptionMessage);
            window.localStorage.setItem('status', status);
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
            window.localStorage.setItem('error', response.Message + " " + response.ExceptionMessage);
            window.localStorage.setItem('status', status);

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
            window.localStorage.setItem('error', response.Message + " " + response.ExceptionMessage);
            window.localStorage.setItem('status', status);

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
        }).error(function () {
            deferred.reject();
        }).finally(function () {
            window.onload = grayOut(false);
        });

        return deferred.promise;
    };

    return service;
});

