PublicationsControllers.controller('LoginController',
    ['$scope', '$routeParams', 'AuthenticationService', 'localStorageService',
        function ($scope, $routeParams, authenticationService, localStorageService) {
            $scope.Login = _login;
            
            $scope.loginEmail;
            $scope.loginPassword;

            function _login() {
                var loginUser = {
                    Email: $scope.loginEmail,
                    Password: $scope.loginPassword
                };

                authenticationService.Login(loginUser).then(function () {
                    document.location = "#/";
                });
            }

        }]);