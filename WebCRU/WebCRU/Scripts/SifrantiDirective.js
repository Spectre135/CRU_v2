'user strict';
/*
    Šifranti directive
*/

var app = angular.module("CRUManagement");

app.directive('sifranti', function ($compile, apiService) {

    //var html = "";

    var controller = ['$scope', function ($scope) {

        $scope.sifranti = [];
        $scope.sifrantList;
        $scope.aplikacijeListRead = 'false';
        $scope.aplikacijeList;
        $scope.aplikacijaSelected = [{ id: '*' }];
        $scope.sifrantSelected = [{ id: '*' }];
        sifrant: [];

        //Šifrant aplikacij
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

    }];

    return {
        restrict: 'EA',
        controller: controller,
        scope: {
            model: '@',
            click: '@',
            list:  '@'
        },
        link: function (scope, element, attrs) {

            var template = '<ui-select ng-model="' + scope.model + '" ng-click="' + scope.click + '">' +
                '<ui-select-match> {{$select.selected.Naziv}}</ui-select-match >' +
                '<ui-select-choices repeat="sifrant.id as sifrant in ' + scope.list + ' | filter:$select.search">' +
                '<div ng-bind="sifrant.Naziv"></div></ui-select-choices></ui-select >';

            console.log(template);
            element.append(template);
            $compile(template)(scope);

        }
    };

});