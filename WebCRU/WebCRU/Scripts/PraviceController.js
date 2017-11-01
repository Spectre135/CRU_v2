'user strict';

var app = angular.module("CRUManagement");

//Query data
app.controller("praviceController", function ($scope, $modal, apiService) {

    $scope.aplikacijaKLJ;
    $scope.vlogaKLJ;

    //getPraviceData
    $scope.getDataAplikacijaKLJ = function (id) {
        $scope.aplikacijaKLJ = id;
        var url = '/api/pravice/';
        apiService.getData(url, $scope.aplikacijaKLJ, $scope.vlogaKLJ)
            .then(function (data) {
                $scope.data = data;
            }, function (response) {
                window.localStorage.setItem('error', response.message);
            });
    };

    $scope.getDataVlogaKLJ = function (id) {
        $scope.vlogaKLJ = id;
        var url = '/api/pravice/';
        apiService.getData(url, $scope.aplikacijaKLJ, $scope.vlogaKLJ)
            .then(function (data) {
                $scope.data = data;
            }, function (response) {
                window.localStorage.setItem('error', response.message);
            });
    };
});