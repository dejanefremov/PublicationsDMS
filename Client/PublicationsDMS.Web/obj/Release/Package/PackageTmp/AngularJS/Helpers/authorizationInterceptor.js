publication.factory('authorizationInterceptor', ['$rootScope', '$q', '$location', 'localStorageService', function ($rootScope, $q, $location, localStorageService) {
    return {
        request: function (config) {
            config.headers = config.headers || {};

            var authData = localStorageService.get('authorizationData');
            if (authData) {
                config.headers.Authorization = 'Bearer ' + authData.token;
            }

            return config;
        },

        responseError: function (rejection) {
            switch (rejection.status) {
                case 401: {
                    $location.url('/login');
                    break;
                }
                default: {
                    break;
                }
            }

            return $q.reject(rejection);
        }
    };
}]);