'user strict';
/*
    Šifranti directive
*/

var app = angular.module("CRUManagement");

app.directive('sifranti', function ($compile, apiService) {

    var controller = ['$scope', function ($scope) {

        //Šifrant aplikacij
        $scope.aplikacijeListRead = 'false'; // use to read once from database
        $scope.aplikacijeList; //list
        $scope.aplikacija = [{ id: '*' }];  //model

        $scope.getAplikacijeList = function () {
            if ($scope.aplikacijeListRead === 'false') {
                $scope.aplikacijeList = [{ Naziv: 'Loading...' }];
                apiService.getSifranti("APL")
                    .then(function (data) {
                        $scope.aplikacijeList = data;
                        $scope.aplikacijeListRead = 'true';
                    }, function (error) {
                        console.log('error', error);
                    });
            }
        };

        //Šifrant vlog
        $scope.vlogeListRead = 'false';
        $scope.vlogeList;
        $scope.vloge = [{ id: '*' }];

        $scope.getVlogeList = function () {
            if ($scope.vlogeListRead === 'false') {
                $scope.vlogeList = [{ Naziv: 'Loading...' }];
                apiService.getSifranti("VLOGE")
                    .then(function (data) {
                        $scope.vlogeList = data;
                        $scope.vlogeListRead = 'true';
                    }, function (error) {
                        console.log('error', error);
                    });
            }
        };

    }];

    return {
        restrict: 'EA',
        controller: controller,
        scope: {
            model: '@', //text
            click: '@',
            list: '@',
            onchange: '&' //method
        },
        link: function (scope, element, attrs) {
            //create html element 'ui-select'' 
            //note expression binding (&), you need to explicitely call it with a JSON containing !!on-select="data({id: $select.selected.Id})
            var html = '<ui-select ng-model="' + scope.model + '.id" ng-click="' + scope.click + '" on-select="onchange({id: $select.selected.Id})" >' +
                '<ui-select-match> {{$select.selected.Naziv}}</ui-select-match >' +
                '<ui-select-choices repeat="sifrant.Id as sifrant in ' + scope.list + ' | filter:$select.search">' +
                '<lang ng-bind="sifrant.Naziv"></lang></ui-select-choices></ui-select >';

            var el = $compile(html)(scope);
            element.append(el);


        }
    };

});