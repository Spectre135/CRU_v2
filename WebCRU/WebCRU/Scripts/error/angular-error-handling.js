var HEADER_NAME = 'CRUManagement-Handle-Errors-Generically';
var specificallyHandleInProgress = false;

angular.module('CRUManagement').factory('RequestsErrorHandler', ['$q', function($q) {
    return {
        // --- The user's API for claiming responsiblity for requests ---
        specificallyHandled: function(specificallyHandledBlock) {
            specificallyHandleInProgress = true;
            try {
                return specificallyHandledBlock();
            } finally {
                specificallyHandleInProgress = false;
            }
        },

        // --- Response interceptor for handling errors generically ---
        responseError: function(rejection) {
            var shouldHandle = (rejection && rejection.config && rejection.config.headers
                && rejection.config.headers[HEADER_NAME]);

            if (shouldHandle) {
                window.location.href = '#error';
            }

            return $q.reject(rejection);
        }
    };
}]);

angular.module('CRUManagement').config(['$provide', '$httpProvider', function($provide, $httpProvider) {
    $httpProvider.interceptors.push('RequestsErrorHandler');

    // --- Decorate $http to add a special header by default ---

    function addHeaderToConfig(config) {
        config = config || {};
        config.headers = config.headers || {};

        // Add the header unless user asked to handle errors himself
        if (!specificallyHandleInProgress) {
            config.headers[HEADER_NAME] = true;
        }

        return config;
    }

    // The rest here is mostly boilerplate needed to decorate $http safely
    $provide.decorator('$http', ['$delegate', function($delegate) {
        function decorateRegularCall(method) {
            return function(url, config) {
                return $delegate[method](url, addHeaderToConfig(config));
            };
        }

        function decorateDataCall(method) {
            return function(url, data, config) {
                return $delegate[method](url, data, addHeaderToConfig(config));
            };
        }

        function copyNotOverriddenAttributes(newHttp) {
            for (var attr in $delegate) {
                if (!newHttp.hasOwnProperty(attr)) {
                    if (typeof($delegate[attr]) === 'function') {
                        newHttp[attr] = function() {
                            return $delegate[attr].apply($delegate, arguments);
                        };
                    } else {
                        newHttp[attr] = $delegate[attr];
                    }
                }
            }
        }

        var newHttp = function(config) {
            return $delegate(addHeaderToConfig(config));
        };

        newHttp.get = decorateRegularCall('get');
        newHttp.delete = decorateRegularCall('delete');
        newHttp.head = decorateRegularCall('head');
        newHttp.jsonp = decorateRegularCall('jsonp');
        newHttp.post = decorateDataCall('post');
        newHttp.put = decorateDataCall('put');

        copyNotOverriddenAttributes(newHttp);

        return newHttp;
    }]);
}]);
