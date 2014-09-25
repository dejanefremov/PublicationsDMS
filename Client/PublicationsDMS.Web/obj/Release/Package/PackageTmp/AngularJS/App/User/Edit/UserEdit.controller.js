PublicationsControllers.controller('UserEditController',
    ['$scope', '$routeParams', 'UserService',
        function ($scope, $routeParams, userService) {
            $scope.saveUser = _saveUser;
            
            $scope.user;
            $scope.password;

            init();

            function init() {
                if ($routeParams.userID != null) {
                    userService.getUser($routeParams.userID).then(function (data) {
                        $scope.user = data;
                    });
                }
            }

            function _saveUser() {
                var currentUser = {
                    UserData: $scope.user
                };

                userService.saveUser(currentUser).then(function () {
                    document.location = "/#/";
                });
            }
        }]);