PublicationsControllers.controller('UserAddController',
    ['$scope', '$routeParams', 'UserService',
        function ($scope, $routeParams, userService) {
            $scope.saveUser = _saveUser;
            
            $scope.user;
            $scope.password;

            init();

            function init() {
                $scope.user = {
                    Name: "",
                    Email: ""
                }
            }

            function _saveUser() {
                var currentUser = {
                    UserData: $scope.user,
                    Password: $scope.password
                };

                userService.saveUser(currentUser).then(function () {
                    document.location = "/#/";
                });
            }
        }]);