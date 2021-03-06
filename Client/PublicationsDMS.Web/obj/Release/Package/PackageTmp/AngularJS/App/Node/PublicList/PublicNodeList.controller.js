﻿PublicationsControllers.controller('PublicNodeListController',
    ['$scope', '$routeParams', 'AuthenticationService', 'NodeService', 'DocumentService', 'UserService',
        function ($scope, $routeParams, authenticationService, nodeService, documentService, userService) {
            $scope.logout = _logout;
            $scope.goTo = _goTo;
            $scope.goToFolder = _goToFolder;
            $scope.searchDocuments = _searchDocuments;
            $scope.getNodeTypeGlyphicon = _getNodeTypeGlyphicon;

            $scope.parentNode;
            $scope.searchingTerm;
            $scope.userInfo;

            $scope.nodes = [];
            $scope.breadcrumbNodes = [];

            init();


            function init() {
                getUserInfo();

                if ($routeParams.parentNodeID != null && $routeParams.parentNodeID != 0) {
                    getNodes($routeParams.parentNodeID);
                    setParentNode($routeParams.parentNodeID);
                }
                else {
                    getNodes();
                }
            }

            function getNodes(parentID) {
                nodeService.getNodes(parentID).then(function (data) {
                    $scope.nodes = data;
                });

                getBreadcrumbNodes(parentID);
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

            function setParentNode(nodeID) {
                $scope.parentNode = null;
                if (nodeID != null) {
                    nodeService.getNode(nodeID).then(function (data) {
                        $scope.parentNode = data;
                    });
                }
            }

            function _goToFolder(node) {
                getNodes(node.ID);
                setParentNode(node.ID);
            }

            function _goTo(node) {
                if (node.TypeName == "Folder") {
                    _goToFolder(node);
                }
                else {
                    documentService.downloadDocument(node.ID);
                }
            }

            function _searchDocuments() {
                if ($scope.searchingTerm.trim().length >= 3) {
                    window.location.href = '#/document/search/' + $scope.searchingTerm.trim() + "/" + ($scope.parentNode == null ? "" : $scope.parentNode.ID);
                }
            }

            function getUserInfo() {
                $scope.userInfo = null;

                userService.getUserInfo().then(function (data) {
                    if (data.IsAdministrator == true) {
                        var parentNodeID = '';
                        if ($routeParams.parentNodeID != null && $routeParams.parentNodeID != 0) {
                            parentNodeID = $routeParams.parentNodeID;
                        }
                        window.location.href = '#/list/' + parentNodeID;
                    }
                    $scope.userInfo = data;
                });
            }

            function _getNodeTypeGlyphicon(node) {
                if (node.TypeName == "Folder") {
                    return "glyphicon glyphicon-folder-open";
                }
                else {
                    return "glyphicon glyphicon-file";
                }
            }
            
            function _logout() {
                authenticationService.Logout().then(function () {
                    window.location.href = '/#/login/';
                });
            }

            function refresh() {
                getNodes();
            }
        }]);