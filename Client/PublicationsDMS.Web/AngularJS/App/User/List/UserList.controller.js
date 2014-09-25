PublicationsControllers.controller('UserListController',
    ['$scope', '$routeParams', 'UserService',
        function ($scope, $routeParams, userService) {
            $scope.users = [];

            init();

            function init() {
                getUsers();
            }

            function getUsers() {
                userService.getAllUsers().then(function (data) {
                    $scope.users = data;
                });
            }
        }]);