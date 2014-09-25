publication.factory('ShareNodeService', ['$http', '$q', function ($http, $q) {
    return {
        getNodeUsers: _getNodeUsers,
        addNodeUser: _addNodeUser,
        saveNodeUsers: _saveNodeUsers
    };

    function _getNodeUsers(nodeID) {
        var deferred = $q.defer();

        $http({ method: 'GET', url: config.generateApiUrl('Api/ShareNode'), params: { "nodeID": nodeID } }).
           success(function (data, status, headers, config) {
               deferred.resolve(data);
           });

        return deferred.promise;
    }

    function _addNodeUser(userEmail) {
        var deferred = $q.defer();

        $http({ method: 'PUT', url: config.generateApiUrl('Api/ShareNode'), params: { "userEmail": userEmail } }).
           success(function (data, status, headers, config) {
               if (data === "null") {
                   data = null;
               }
               deferred.resolve(data);
           });

        return deferred.promise;
    }

    function _saveNodeUsers(nodeID, userIDs) {
        var deferred = $q.defer();

        $http({ method: 'POST', url: config.generateApiUrl('Api/ShareNode'), data: JSON.stringify({ NodeID: nodeID, UserIDs: userIDs }) }).
           success(function (data, status, headers, config) {
               deferred.resolve(data);
           });

        return deferred.promise;
    }
}]);