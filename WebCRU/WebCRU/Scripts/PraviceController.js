'user strict';

var app = angular.module("CRUManagement");

//Query data
app.controller("praviceController", function ($scope, $modal, apiService) {

    $scope.aplikacijaKLJ=0;
    $scope.vlogaKLJ=0;

    //getPraviceData
    $scope.getDataAplikacijaKLJ = function (id) {
        console.log(id);
        $scope.aplikacijaKLJ = id;

        apiService.getVlogePravice($scope.aplikacijaKLJ, $scope.vlogaKLJ)
            .then(function (data) {
                $scope.data = data;
            }, function (response) {
                window.localStorage.setItem('error', response.message);
            });
    };

    $scope.getDataVlogaKLJ = function (id) {
        console.log(id);
        $scope.vlogaKLJ = id;

        apiService.getVlogePravice($scope.aplikacijaKLJ, $scope.vlogaKLJ)
            .then(function (data) {
                $scope.data = data;
            }, function (response) {
                window.localStorage.setItem('error', response.message);
            });
    };
});