'user strict';

var app = angular.module("CRUManagement");

//Query data
app.controller("PraviceController", function ($scope, $modal, apiService) {

    //getAplikacijeData
    $scope.getData = function () {
        apiService.getAplikacije($scope.searchString, $scope.pageIndex, $scope.pageSizeSelected, $scope.sortKey, $scope.asc)
            .then(function (data) {
                $scope.data = data.DataList;
                $scope.totalCount = data.RowsCount;
            }, function (response) {
                window.localStorage.setItem('error', response.message);
            });
    };
});