'user strict';
/*
    Confirmation modal dialog directive
*/

var app = angular.module("CRUManagement");

app.directive('ngConfirmClick', ['$modal',
    function ($modal) {

        var ModalInstanceCtrl = function ($scope, $modalInstance) {
            $scope.ok = function () {
                $modalInstance.close();
            };

            $scope.cancel = function () {
                $modalInstance.dismiss('cancel');
            };
        };

        return {
            restrict: 'A',
            scope: {
                ngConfirmClick: "&",
                item: "="
            },
            link: function (scope, element, attrs) {
                element.bind('click', function () {

                    var modalHtml = '<div class="modal-body">'+
                        '<div class="modal-header"><h4 class="modal-title">' + attrs.ngMessage + '</h4></div>' +
                        '<div class="modal-footer">' +
                        '<button type="button" ng-click="ok()" class="buttonNkbm"><span class="glyphicon glyphicon-ok"></span> OK</button>' +
                        '<button type="button" ng-click="cancel()" class="buttonNkbm"><span class="glyphicon glyphicon-remove"></span> Cancel</button>' +
                        '</div></div>';

                    var modalInstance = $modal.open({
                        template: modalHtml,
                        controller: ModalInstanceCtrl,
                        windowClass: 'app-modal-window'
                    });

                    modalInstance.result.then(function () {
                        scope.ngConfirmClick({ item: scope.item }); 
                    }, function () {
                        //Modal dismissed
                    });

                });

            }
        }
    }
]);
