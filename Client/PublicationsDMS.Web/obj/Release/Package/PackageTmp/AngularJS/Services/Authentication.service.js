publication.factory('AuthenticationService', ['$http', '$q', 'localStorageService',
    function ($http, $q, localStorageService) {
        return {
            Login: _Login,
            Logout: _Logout
        };

        function _Login(loginUser) {
            var data = "grant_type=password&username=" + loginUser.Email + "&password=" + loginUser.Password;
            var deferred = $q.defer();

            $http({ method: 'POST', url: config.generateApiUrl('token'), data: data }).
               success(function (data, status, headers, config) {
                   localStorageService.set('authorizationData', { token: data.access_token });

                   deferred.resolve(data);
               });

            return deferred.promise;
        }

        function _Logout() {
            var deferred = $q.defer();

            $http({ method: 'GET', url: config.generateApiUrl('/Api/Auth') }).
                success(function (data, status, headers, config) {
                    localStorageService.remove('authorizationData');

                    deferred.resolve(data);
                });

            return deferred.promise;
        }
    }]);