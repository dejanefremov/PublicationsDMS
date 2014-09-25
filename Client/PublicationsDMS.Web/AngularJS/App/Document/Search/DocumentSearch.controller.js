PublicationsControllers.controller('DocumentSearchController',
    ['$scope', '$routeParams', 'DocumentService', 'NodeService',
        function ($scope, $routeParams, documentService, nodeService) {
            $scope.goBack = _goBack;
            $scope.searchDocuments = _searchDocuments;
            $scope.downloadDocument = _downloadDocument;

            $scope.parentNodeID;
            $scope.searchingTerm;
            $scope.originalSearchingTerm;

            $scope.nodes = [];
            $scope.breadcrumbNodes = [];

            init();


            function init() {
                $scope.parentNodeID = $routeParams.parentNodeID;
                $scope.searchingTerm = $routeParams.searchingTerm;
                $scope.originalSearchingTerm = $routeParams.searchingTerm;

                getNodes();
            }

            function getNodes() {
                documentService.searchDocuments($scope.parentNodeID, $scope.searchingTerm).then(function (data) {
                    $scope.nodes = data;
                });

                getBreadcrumbNodes($scope.parentNodeID);
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

            function _goBack() {
                document.location = "/#/list/" + ($scope.parentNodeID == null ? "" : $scope.parentNodeID);
            }

            function _searchDocuments() {
                if ($scope.searchingTerm.trim().length >= 3) {
                    window.location.href = '#/document/search/' + $scope.searchingTerm.trim() + "/" + $scope.parentNodeID;
                }
            }

            function _downloadDocument(node) {
                documentService.downloadDocument(node.ID);
            }
        }]);