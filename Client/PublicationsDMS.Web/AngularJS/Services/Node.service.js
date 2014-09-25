publication.factory('NodeService', ['$http', '$q', function ($http, $q) {
    return {
        getNodes: _getNodes,
        getNode: _getNode,
        getBreadcrumbNodes: _getBreadcrumbNodes
    };


    function _getNodes(parentID) {
        var deferred = $q.defer();

        $http({ method: 'GET', url: config.generateApiUrl('Api/NodeList'), params: { "parentID": parentID == null ? "" : parentID } }).
            success(function (data, status, headers, config) {
                deferred.resolve(data);
            });

        return deferred.promise;
    }

    function _getNode(nodeID) {
        var deferred = $q.defer();

        $http({ method: 'GET', url: config.generateApiUrl('Api/Node'), params: { "nodeID": nodeID } }).
            success(function (data, status, headers, config) {
                deferred.resolve(data);
            });

        return deferred.promise;
    }

    function _getBreadcrumbNodes(parentID) {
        var deferred = $q.defer();

        $http({ method: 'GET', url: config.generateApiUrl('Api/BreadcrumbNode'), params: { "parentID": parentID } }).
            success(function (data, status, headers, config) {
                deferred.resolve(data);
            });

        return deferred.promise;
    }
}]);