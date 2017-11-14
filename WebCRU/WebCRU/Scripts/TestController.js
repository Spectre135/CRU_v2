'user strict';

var app = angular.module("CRUManagement");

app.controller("testController", function ($scope, $modal, apiService) {

    $scope.token;
    $scope.sessionToken;
    $scope.isValidUser;

    $scope.getAuthToken = function () {
        var url = '/api/auth/createsession/';
        apiService.getData(url,'MABA0974')
            .then(function (data) {
                $scope.token = data.SessionAuthToken;
                $scope.isValidUser = data.IsUserValidInAD;
                sessionStorage.setItem("AuthSessionToken",$scope.token);
                console.log($scope.token);
            }, function (response) {
            });
    };


    $scope.readSessionToken = function () {
        $scope.sessionToken = sessionStorage.getItem("AuthSessionToken");
    };


});