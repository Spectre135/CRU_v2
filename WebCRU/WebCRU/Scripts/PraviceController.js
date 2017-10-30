'user strict';

var app = angular.module("CRUManagement");

//Query data
app.controller("praviceController", function ($scope, $modal, apiService) {

    $scope.aplikacijaKLJ;

    //getPraviceData
    $scope.getData = function () {
        console.log("pravice klic");
        var url = '/api/pravice/';
        apiService.getData(url, $scope.aplikacijaKLJ)
            .then(function (data) {
                $scope.data = data;
            }, function (response) {
                window.localStorage.setItem('error', response.message);
            });
    };
});