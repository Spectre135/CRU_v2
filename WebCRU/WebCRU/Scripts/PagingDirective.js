'user strict';
/*
    Paging directive
*/

var app = angular.module("CRUManagement");

app.directive('paging', function () {

    var controller = ['$scope', function ($scope) {

        //pagination
        $scope.PageSizeList = [5, 10, 25, 50];
        $scope.maxSize = 5;
        $scope.totalCount = 0;
        $scope.pageIndex = 1;
        $scope.sortKey = 'null';
        $scope.asc = 'null';
        $scope.pageSizeSelected = { selected: 5 };

        $scope.pageChanged = function () {
            $scope.getData(); //getData function must be on controller
        };

        $scope.changePageSize = function () {
            $scope.pageIndex = 1;
            $scope.getData(); //getData function must be on controller
        };

        //sort data
        $scope.sort = function (keyname) {
            $scope.sortKey = keyname;   //set the sortKey to the param passed
            $scope.asc = !$scope.asc; //if true make it false and vice versa
            $scope.getData(); //getData function must be on controller
        };

        $scope.getSortClass = function () {
            if ($scope.asc) {
                return 'glyphicon glyphicon-triangle-top';
            } else {
                return 'glyphicon glyphicon-triangle-bottom';
            }
        };
    }];


    return {
        restrict: 'EA',
        controller: controller,
        templateUrl: 'Pages/paging/Paging.html'
    };
});