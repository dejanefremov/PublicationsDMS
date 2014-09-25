PublicationsControllers.controller('DocumentUploadController',
    ['$scope', '$http', '$timeout', '$routeParams', 'DocumentService', 'FolderService',
        function ($scope, $http, $timeout, $routeParams, documentService, folderService) {
            $scope.saveUploadedDocuments = _saveUplodedDocuments;
            $scope.documentsUploaded = _documentsUploaded;
            $scope.removeUploadedDocument = _removeUploadedDocument;

            $scope.parentFolder;
            $scope.uploadingDocuments = [];

            init();

            function init() {
                if ($routeParams.parentNodeID != null) {
                    folderService.getFolder($routeParams.parentNodeID).then(function (data) {
                        $scope.parentFolder = data;
                    });
                }
            }

            function _saveUplodedDocuments() {
                var savingParentFolder = $scope.parentFolder == null ? null : $scope.parentFolder.ID;
                documentService.saveUploadedDocuments(savingParentFolder, $scope.uploadingDocuments).then(function () {
                    document.location = "/#/list/" + ($scope.parentFolder == null ? "" : $scope.parentFolder.ID);
                });
            }

            function _documentsUploaded(elem) {
                var formDataFiles = new FormData();
                angular.forEach(elem.files, function (file) {
                    formDataFiles.append('file', file);
                })

                documentService.uploadDocuments(formDataFiles).then(function (data) {
                    $timeout(function () {
                        $scope.uploadingDocuments = $scope.uploadingDocuments.concat(data);
                        $scope.$apply();
                    }, 0);
                });
            }

            function _removeUploadedDocument(fileID) {
                $scope.uploadingDocuments = $scope.uploadingDocuments.filter(function (item) {
                    return item.FileID != fileID;
                });
            }
        }]);