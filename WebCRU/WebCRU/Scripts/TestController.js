'user strict';

var app = angular.module("CRUManagement");

app.controller("testController", function ($scope, $modal, apiService) {

    $scope.token;
    $scope.user;
    $scope.sessionToken;
    $scope.isValidUser;
    $scope.isSessionValid;

    $scope.getAuthToken = function () {
        var url = '/api/auth/createsession/';
        var params = {
            "uporabnikID": $scope.user
        };
        apiService.getData(url,params)
            .then(function (data) {
                $scope.token = data.SessionAuthToken;
                $scope.isValidUser = data.IsUserValidInAD;
                sessionStorage.setItem("AuthSessionToken",$scope.token);
                console.log($scope.token);
            }, function (response) {
            });
    };

    $scope.getSessionValid = function () {
        var url = '/api/auth/validatetoken/';
        var params= {
            "sessionToken": $scope.token
        };
        apiService.getData(url,params)
            .then(function (data) {
                $scope.isSessionValid = data.SessionValid;
                console.log($scope.token);
            }, function (response) {
            });
    };


    $scope.readSessionToken = function () {
        $scope.sessionToken = sessionStorage.getItem("AuthSessionToken");
    };

});