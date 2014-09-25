PublicationsControllers.controller('DocumentEditController',
    ['$scope', '$routeParams', 'DocumentService', 'FolderService',
        function ($scope, $routeParams, documentService, folderService) {
            $scope.saveDocument = _saveDocument;

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
            
            function _saveDocument() {
                documentService.saveDocument($scope.currentDocument).then(function () {
                    document.location = "/#/list/" + ($scope.parentFolder == null ? "" : $scope.parentFolder.ID);
                });
            }
        }]);