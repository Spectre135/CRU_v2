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

    //Get apikacije
    service.getAplikacije = function (searchString, pageIndex, pageSizeSelected, sortKey, asc) {
        var deferred = $q.defer();
        //posivim ekran
        window.onload = grayOut(true);

        $http({
            method: 'GET',
            url: backEndUrl +'/api/aplikacije2/' + searchString,
            params: {
                pageIndex: pageIndex,
                pageSizeSelected: pageSizeSelected.selected,
                sortKey: sortKey,
                asc: asc
            }

        }).success(function (data) {
            deferred.resolve(data);

        }).error(function (response) {
            window.localStorage.setItem('error', response.Message);
            window.localStorage.setItem('status', status);
            deferred.reject(response);

        }).finally(function () {
            window.onload = grayOut(false);
        });

        return deferred.promise;
    };

    //Get Data
    service.getData = function (searchString, pageIndex, pageSizeSelected, sortKey, asc) {
        var deferred = $q.defer();
        //posivim ekran
        window.onload = grayOut(true);

        $http({
            method: 'GET',
            url: backEndUrl + '/api/aplikacije2/' + searchString,
            params: {
                pageIndex: pageIndex,
                pageSizeSelected: pageSizeSelected.selected,
                sortKey: sortKey,
                asc: asc
            }

        }).success(function (data) {
            deferred.resolve(data);

        }).error(function (response) {
            window.localStorage.setItem('error', response.Message);
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
            window.localStorage.setItem('status', status);
            window.localStorage.setItem('error', response.Message);

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
            window.localStorage.setItem('status', status);
            window.localStorage.setItem('error', response.Message);

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

