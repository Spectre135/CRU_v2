'user strict';

var app = angular.module("CRUManagement");

//Query data
app.controller("AplikacijaController", function ($scope, $modal, apiService) {

    $scope.searchString;
    $scope.rowNumber = -1;
    $scope.data = [];
    /*
    $scope.sifranti = [];
    $scope.accountNumberTypeRead = 'false';
    $scope.countryCodeListRead = 'false';
    $scope.reportTypeListRead = 'false';
    $scope.accountNumberTypeList;
    $scope.countryCodeList;
    sifrant: [];
    */


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

    //new Aplikacija
    $scope.newAplikacija = function () {
        var newDto = {};
        this.openModal(newDto);
    };

    //openModal for edit
    $scope.openModal = function (dto) {
        window.onload = grayOut(true);
        $modal.open({
            templateUrl: '/Pages/aplikacije/editAplikacije.html',
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
        url = '/api/aplikacije/save/';
        apiService.saveData(url,$scope.editDto);
    };

    //delete
    $scope.delete = function () {
        url = '/api/aplikacije/delete/';
        apiService.deleteRecord(url,$scope.editDto);
    };

    //cancel
    $scope.cancel = function () {
        $scope.editDto;
        $modalInstance.dismiss('cancel');
    };
});
