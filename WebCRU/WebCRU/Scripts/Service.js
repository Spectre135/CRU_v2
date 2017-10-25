'use strict';

var app = angular.module("CRUManagement");

//config Header v katerega vpišemo nkbmAuthToken iz sessionStorage
app.factory('httpRequestInterceptor', function () {
    return {
        request: function (config) {

            config.headers['NkbmAuthToken'] = sessionStorage.getItem("NkbmAuthToken");
            return config;
        }
    };
});

app.config(function ($httpProvider) {
    $httpProvider.interceptors.push('httpRequestInterceptor');
});

//Services
app.factory('apiService', function ($q, $http) {
    var service = {};

    //Get apikacije
    service.getAplikacije = function (searchString, pageIndex, pageSizeSelected, sortKey, asc) {
        var deferred = $q.defer();
        //posivim ekran
        window.onload = grayOut(true);

        $http({
            method: 'GET',
            url: '/api/aplikacije2/' + searchString,
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
    service.saveData = function (dto) {
        window.onload = grayOut(true);

        $http({
            method: 'POST',
            url: '/api/aplikacije/save/',
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
            url: '/api/aplikacije/delete/',
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
            url: 'rest/list/' + id
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

