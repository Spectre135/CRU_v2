var app = angular.module("CRUManagement", ['ngRoute', 'ngSanitize', 'ui.select', 'ui.bootstrap']);

// configure app routes
app.config(function ($routeProvider) {
    $routeProvider

        // route for home page
        .when('/home', {
            templateUrl: '/'
        })

        // route for the aplikacije page
        .when('/aplikacije', {
            templateUrl: 'Pages/aplikacije/aplikacije.html',
            controller: 'aplikacijaController'
        })

        // route for the pravice page
        .when('/pravice', {
            templateUrl: 'Pages/pravice/pravice.html',
            controller: 'praviceController'
        });

});


