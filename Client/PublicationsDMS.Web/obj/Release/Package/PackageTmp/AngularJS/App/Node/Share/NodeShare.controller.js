PublicationsControllers.controller('NodeShareController',
    ['$scope', '$routeParams', '$timeout', 'NodeService', 'ShareNodeService',
        function ($scope, $routeParams, $timeout, nodeService, shareNodeService) {
            $scope.addUser = _addUser;
            $scope.removeUser = _removeUser;
            $scope.saveUsers = _saveUsers;

            $scope.node;
            $scope.emailToAdd;
            $scope.parentNodeID;
            $scope.users = [];
            $scope.breadcrumbNodes = [];

            init();

            function init() {
                $scope.parentNodeID = $routeParams.parentNodeID;

                getBreadcrumbNodes($scope.parentNodeID);

                nodeService.getNode($routeParams.nodeID).then(function (data) {
                    $scope.node = data;
                });

                shareNodeService.getNodeUsers($routeParams.nodeID).then(function (data) {
                    $scope.users = data;
                });
            }
            
            function _addUser() {
                shareNodeService.addNodeUser($scope.emailToAdd).then(function (data) {
                    if (data != null) {
                        $timeout(function () {
                            var userExist = false;
                            $scope.users.forEach(function (user) {
                                if (user.UserID == data.UserID) {
                                    userExist = true;
                                }
                            });
                            if (!userExist) {
                                $scope.users = $scope.users.concat(data);
                                $scope.emailToAdd = "";
                                $scope.$apply();
                            }
                        }, 0);
                    }
                });
            }

            function _removeUser(userID) {
                $scope.users = $scope.users.filter(function (item) {
                    return item.UserID != userID;
                });
            }

            function _saveUsers() {
                var userIDs = [];
                $scope.users.forEach(function (user) {
                    userIDs.push(user.UserID);
                });

                shareNodeService.saveNodeUsers($scope.node.ID, userIDs).then(function () {
                    document.location = "/#/list/" + $scope.parentNodeID;
                });
            }

            function getBreadcrumbNodes(parentID) {
                if (parentID) {
                    nodeService.getBreadcrumbNodes(parentID).then(function (data) {
                        $scope.breadcrumbNodes = data;
                    });
                }
                else {
                    $scope.breadcrumbNodes = [];
                }
            }
        }]);