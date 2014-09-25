PublicationsControllers.controller('FolderEditController',
    ['$scope', '$routeParams', 'FolderService',
        function ($scope, $routeParams, folderService) {
            $scope.saveFolder = _saveFolder;

            $scope.parentFolder;
            $scope.currentFolder;

            init();

            function init() {
                if ($routeParams.parentNodeID != null) {
                    folderService.getFolder($routeParams.parentNodeID).then(function (data) {
                        $scope.parentFolder = data;
                    });

                    if ($routeParams.currentNodeID != null) {
                        folderService.getFolder($routeParams.currentNodeID).then(function (data) {
                            $scope.currentFolder = data;
                        });
                    }
                    else {
                        $scope.currentFolder = {
                            Title: "",
                            ParentFolderID: $routeParams.parentNodeID
                        }
                    }
                }
            }

            function _saveFolder() {
                folderService.saveFolder($scope.currentFolder).then(function () {
                    document.location = "/#/list/" + ($scope.parentFolder == null ? "" : $scope.parentFolder.ID);
                });
            }
        }]);