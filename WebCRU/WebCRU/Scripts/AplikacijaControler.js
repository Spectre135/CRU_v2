'user strict';

var app = angular.module("CRUManagement");

app.controller("aplikacijaController", function ($scope, $modal, apiService) {

    $scope.searchString;
    $scope.rowNumber = -1;
    $scope.data = [];
    window.onload = grayOut(false);

    function nullToUndefined(input) {
        if (input === null || input === '') {
            return 'undefined';
        }

        return input;
    };

    //getAplikacijeData
    $scope.getData = function () {
        var url = '/api/aplikacije2/' + nullToUndefined($scope.searchString);
        var params = {
            "asc": $scope.asc,
            "pageIndex": $scope.pageIndex,
            "pageSizeSelected": $scope.pageSizeSelected.selected,
            "sortKey": $scope.sortKey
        };
        apiService.getData(url,params)
            .then(function (data) {
                    $scope.data = data.DataList;
                    $scope.totalCount = data.RowsCount;
            }, function (response) {
                    //error is handled in Service
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

        var modalInstance = $modal.open({
            templateUrl: '/Pages/aplikacije/editAplikacije.html',
            controller: 'aplEditCtrl',
            controllerAs: 'vm',
            scope: $scope,
            backdrop: 'static',
            resolve: {
                dto: function () {
                    return dto;
                }
            }
        });
        //we refresh data after modal close 
        modalInstance.result.then(function () {
            window.onload = grayOut(false);
        }, function () {});

    };

    //Row selected
    $scope.rowselected = function (row) {
        $scope.rowNumber = row;
    };

});

//Edit data
app.controller('aplEditCtrl', function ($scope, $modalInstance, dto, apiService) {
    $scope.editDto = angular.copy(dto);

    //save
    $scope.save = function () {
        url = '/api/aplikacije/save/';
        apiService.postData(url, $scope.editDto).then(function (response) { 
            $scope.getData();
            $modalInstance.close();
;        });
    };

    //delete
    $scope.delete = function () {
        url = '/api/aplikacije/delete/';
        apiService.postData(url, $scope.editDto).then(function (response) {
            $scope.getData();
            $modalInstance.close();
        });
    };

    //cancel
    $scope.cancel = function () {
        $modalInstance.dismiss('cancel');
    };
});
