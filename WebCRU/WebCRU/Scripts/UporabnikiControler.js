'user strict';

var app = angular.module("CRUManagement");

app.controller("uporabnikiController", function ($scope, $modal, apiService) {

    $scope.searchString;
    $scope.rowNumber = -1;
    $scope.data = [];
    window.onload = grayOut(false);
    var url = '/api/uporabniki/';


    //getUporabnikiData
    $scope.getData = function () {
        apiService.getData(url)
            .then(function (data) {
                $scope.data = data;
            }, function (response) {
               //error is handled in Service
            });
    };

    //new Uporabnik
    $scope.newUporabnik = function () {
        var newDto = {};
        this.openModal(newDto);
    };

    //openModal for edit
    $scope.openModal = function (dto) {

        window.onload = grayOut(true);

        var modalInstance = $modal.open({
            templateUrl: '/Pages/uporabniki/editUporabniki.html',
            controller: 'uprbEditCtrl',
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
app.controller('uprbEditCtrl', function ($scope, $modalInstance, dto, apiService) {
    $scope.editDto = angular.copy(dto);

    //save
    $scope.save = function () {
        url = '/api/uporabniki/save/';
        apiService.postData(url, $scope.editDto).then(function (response) {
            $scope.getData();
            $modalInstance.close();
        });
    };

    //delete
    $scope.delete = function () {
        url = '/api/uporabniki/delete/';
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
