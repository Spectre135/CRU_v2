'user strict';

var app = angular.module("CRUManagement", ['ngSanitize', 'ui.select', 'ui.bootstrap']);

//Query data
app.controller("CRUController", function ($scope, $modal, apiService) {

    $scope.searchString;
    $scope.rowNumber = -1;
    $scope.data = [];
    $scope.sifranti = [];
    $scope.accountNumberTypeRead = 'false';
    $scope.countryCodeListRead = 'false';
    $scope.reportTypeListRead = 'false';
    $scope.accountNumberTypeList;
    $scope.countryCodeList;
    sifrant:[];

    //pagination
    $scope.PageSizeList = [5, 10, 25, 50];
    $scope.maxSize = 5;
    $scope.totalCount = 0;
    $scope.pageIndex = 1;
    $scope.sortKey='null';
    $scope.asc='null';
    $scope.pageSizeSelected = {selected: 5};


    $scope.pageChanged = function () {
        $scope.getData();
    };

    $scope.changePageSize = function () {
        $scope.pageIndex = 1;
        $scope.getData();
    };
    
    //sort data
    $scope.sort = function(keyname){
        $scope.sortKey = keyname;   //set the sortKey to the param passed
        $scope.asc = !$scope.asc; //if true make it false and vice versa
        $scope.getData();
    };
    
    $scope.getSortClass = function() {
        if ($scope.asc){
            return 'glyphicon glyphicon-triangle-top';
        } else{
            return 'glyphicon glyphicon-triangle-bottom';
        }
    };

    //getAplikacije
    $scope.getAplikacije = function () {
        apiService.getAplikacije($scope.searchString, $scope.pageIndex, $scope.pageSizeSelected,$scope.sortKey,$scope.asc)
                .then(function (data) {
                    $scope.data = data.DataList;
                    $scope.totalCount = data.RowsCount;
                }, function (response) {
                    window.localStorage.setItem('error', response.message);
                });
    };

    //new Aplikacija
    $scope.newAplikacija = function () {
        var newDto = {};
        this.openModal(newDto);
    };

    //openModal for edit
    $scope.openModal = function (dto) {
        window.onload = grayOut(true);
        $modal.open({
            templateUrl: 'editAplikacije.html',
            controller: 'AplEditCtrl',
            controllerAs: 'vm',
            scope: $scope,
            backdrop: 'static',
            resolve: {
                dto: function () {
                    return dto;
                }
            }
        });
    };

    //Row selected
    $scope.rowselected = function (row) {
        $scope.rowNumber = row;
    };

    //Sifranti//
    $scope.getCountryCodeListSelected = function (id) {
        if (typeof $scope.countryCodeList === 'undefined') {
            $scope.countryCodeList = [{id: id}];
        } else {
            $scope.countryCodeList.push[{id: id}];
        }

    };

    $scope.getAccountNumberTypeListSelected = function (id) {
        if (typeof $scope.accountNumberTypeList === 'undefined') {
            $scope.accountNumberTypeList = [{id: id}];
        } else {
            $scope.accountNumberTypeList.push[{id: id}];
        }

    };

    $scope.getAccountNumberTypeList = function () {
        if ($scope.accountNumberTypeRead === 'false') {
            apiService.getSifranti("accountNumberType")
                    .then(function (data) {
                        $scope.accountNumberTypeList = data;
                        $scope.accountNumberTypeRead = 'true';
                    }, function (error) {
                        console.log('error', error);
                    });
        }
    };

    $scope.getCountryCodeList = function () {
        if ($scope.countryCodeListRead === 'false') {
            $scope.countryCodeList = [{id: 'Loading...'}];
            apiService.getSifranti("countryCode")
                    .then(function (data) {
                        $scope.countryCodeList = data;
                        $scope.countryCodeListRead = 'true';
                    }, function (error) {
                        console.log('error', error);
                    });
        }
    };

    $scope.getReportTypeList = function () {
        if ($scope.reportTypeListRead === 'false') {
            $scope.countryCodeList = [{id: 'Loading...'}];
            apiService.getSifranti("reportType")
                    .then(function (data) {
                        $scope.reportTypeList = data;
                        $scope.reportTypeListRead = 'true';
                    }, function (error) {
                        console.log('error', error);
                    });
        }
    };

});

//Edit data
app.controller('AplEditCtrl', function ($scope, $modalInstance, dto, apiService) {
    $scope.editDto = angular.copy(dto);

    //save
    $scope.save = function () {
        apiService.saveData($scope.editDto);
    };

    //delete
    $scope.delete = function () {
        apiService.deleteRecord($scope.editDto);
    };

    //cancel
    $scope.cancel = function () {
        $scope.editDto;
        $modalInstance.dismiss('cancel');
    };
});

//Directives
app.directive('smartFloat', function ($filter) {
    var FLOAT_REGEXP_1 = /^\$?\d+.(\d{3})*(\,\d*)$/; //Numbers like: 1.123,56
    var FLOAT_REGEXP_2 = /^\$?\d+,(\d{3})*(\.\d*)$/; //Numbers like: 1,123.56
    var FLOAT_REGEXP_3 = /^\$?\d+(\.\d*)?$/; //Numbers like: 1123.56
    var FLOAT_REGEXP_4 = /^\$?\d+(\,\d*)?$/; //Numbers like: 1123,56

    return {
        require: 'ngModel',
        link: function (scope, elm, attrs, ctrl) {
            ctrl.$parsers.unshift(function (viewValue) {
                if (FLOAT_REGEXP_1.test(viewValue)) {
                    ctrl.$setValidity('float', true);
                    return parseFloat(viewValue.replace('.', '').replace(',', '.'));
                } else if (FLOAT_REGEXP_2.test(viewValue)) {
                    ctrl.$setValidity('float', true);
                    return parseFloat(viewValue.replace(',', ''));
                } else if (FLOAT_REGEXP_3.test(viewValue)) {
                    ctrl.$setValidity('float', true);
                    return parseFloat(viewValue);
                } else if (FLOAT_REGEXP_4.test(viewValue)) {
                    ctrl.$setValidity('float', true);
                    return parseFloat(viewValue.replace(',', '.'));
                } else {
                    ctrl.$setValidity('float', false);
                    return undefined;
                }
            });

            ctrl.$formatters.unshift(
                    function (modelValue) {
                        return $filter('number')(parseFloat(modelValue), 2);
                    }
            );
        }
    };
});  