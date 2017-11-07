'user strict';

var app = angular.module("CRUManagement");

app.controller("testController", function ($scope, $modal, apiService) {

    $scope.token;
    $scope.sessionToken;
    $scope.isValidUser;

    $scope.getAuthToken = function () {
        var url = '/api/auth/';
        apiService.getData(url,'MABA0974')
            .then(function (data) {
                $scope.token = data.SessionAuthToken;
                $scope.isValidUser = data.IsUserValidInAD;
                sessionStorage.setItem("AuthSessionToken",$scope.token);
                console.log($scope.token);
            }, function (response) {
                window.localStorage.setItem('error', response.message);
            });
    };


    $scope.readSessionToken = function () {
        $scope.sessionToken = sessionStorage.getItem("AuthSessionToken");
    };


});