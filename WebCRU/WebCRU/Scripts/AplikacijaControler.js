'user strict';

var app = angular.module("CRUManagement");

app.controller("aplikacijaController", function ($scope, $modal, apiService) {

    $scope.searchString;
    $scope.rowNumber = -1;
    $scope.data = [];


    //getAplikacijeData
    $scope.getData = function () {
        apiService.getAplikacije($scope.searchString, $scope.pageIndex, $scope.pageSizeSelected, $scope.sortKey, $scope.asc)
            .then(function (data) {
                    $scope.data = data.DataList;
                    $scope.totalCount = data.RowsCount;
            }, function (response) {
                    //error is handled in Service
                    //window.localStorage.setItem('error', response.message);
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
            $scope.getData();
        }, function () { });

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
        apiService.saveData(url, $scope.editDto);
        $modalInstance.close();
    };

    //delete
    $scope.delete = function () {
        url = '/api/aplikacije/delete/';
        apiService.deleteRecord(url, $scope.editDto);
        $modalInstance.close();
    };

    //cancel
    $scope.cancel = function () {
        $modalInstance.dismiss('cancel');
    };
});
