PublicationsControllers.controller('DocumentDetailsController',
    ['$scope', '$routeParams', 'NodeService', 'DocumentService',
        function ($scope, $routeParams, nodeService, documentService) {
            $scope.parentFolder;
            $scope.currentDocument;

            init();

            function init() {
                if ($routeParams.currentNodeID != null) {
                    documentService.getDocument($routeParams.currentNodeID).then(function (data) {
                        $scope.currentDocument = data;

                        nodeService.getNode($scope.currentDocument.ParentFolderID).then(function (data) {
                            $scope.parentFolder = data;
                        });
                    });
                }
            }
        }]);