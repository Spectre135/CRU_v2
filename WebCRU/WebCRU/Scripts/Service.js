'use strict';

var app = angular.module("FatcaManagement");

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

    //Get data
    service.getData = function (searchString,reportTypeSelected,pageIndex,pageSizeSelected,sortKey,asc) {
        var deferred = $q.defer();
        //posivim ekran
        window.onload = grayOut(true);
        
        $http({
            method: 'GET',
            url: 'http://localhost:18546/RestService.svc/Porocanje/' + searchString,
            params: {/*reportType:reportTypeSelected.id,*/
                     pageIndex: pageIndex,
                     pageSizeSelected: pageSizeSelected.selected,
                     sortKey :sortKey,
                     asc:asc}
                 
        }).success(function (data) {
            deferred.resolve(data);
            
        }).error(function (response) {
            deferred.reject(response);
            
        }).finally(function () {
            window.onload = grayOut(false);
        });

        return deferred.promise;
    };
    
    //Save data
    service.saveData = function (dto) {  
       window.onload = grayOut(true);
       $http.post("rest/save/", angular.toJson(dto)).then(function (status) {
            window.onload = grayOut(false);
            window.localStorage.setItem('status', status);
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

