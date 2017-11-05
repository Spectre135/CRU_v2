﻿'user strict';

var app = angular.module("CRUManagement");

//Query data
app.controller("praviceController", function ($scope, $modal, apiService) {

    $scope.aplikacijaKLJ=0;
    $scope.vlogaKLJ = 0;
    $scope.new = 'false';
    $scope.data = [];

    //getPraviceData
    $scope.getDataAplikacijaKLJ = function (id) {
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

    //new Pravica
    $scope.newPravica = function () {
        var newDto = {};
        $scope.new = 'true';
        console.log($scope.new);
        this.openModal(newDto);
    };

    //openModal for edit
    $scope.openModal = function (dto) {

        window.onload = grayOut(true);

        var modalInstance = $modal.open({
            templateUrl: '/Pages/pravice/editPravice.html',
            controller: 'praviceEditCtrl',
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
            console.log("refresh");
            $scope.new = 'false';
            $scope.getData();
        }, function () { });

    };

});

//Edit data
app.controller('praviceEditCtrl', function ($scope, $modalInstance, dto, apiService) {
    $scope.editDto = angular.copy(dto);

    //save
    $scope.save = function () {
        url = '/api/pravice/save/';
        apiService.saveData(url, $scope.editDto);
        $modalInstance.close();
    };

    //delete
    $scope.delete = function () {
        url = '/api/pravice/delete/';
        apiService.deleteRecord(url, $scope.editDto);
        $modalInstance.close();
    };

    //cancel
    $scope.cancel = function () {
        $modalInstance.dismiss('cancel');
    };
});