PublicationsControllers.controller('ChangePasswordController',
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
                else {
                    document.location = "/#/user/list/";
                }
            }

            function _saveUser() {
                var currentUser = {
                    UserData: $scope.user,
                    Password: $scope.password
                };

                userService.changeUserPassword(currentUser).then(function () {
                    document.location = "/#/";
                });
            }
        }]);