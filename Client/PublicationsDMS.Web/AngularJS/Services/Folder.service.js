publication.factory('FolderService', ['$http', '$q', function ($http, $q) {
    return {
        getFolder: _getFolder,
        saveFolder: _saveFolder
    };

    function _getFolder(folderID) {
        var deferred = $q.defer();

        $http({ method: 'GET', url: config.generateApiUrl('Api/Folder'), params: { "folderID": folderID == null ? "" : folderID } }).
            success(function (data, status, headers, config) {
                deferred.resolve(data);
            });

            return deferred.promise;
    }

    function _saveFolder(folder) {
        var deferred = $q.defer();

        $http({ method: 'POST', url: config.generateApiUrl('Api/Folder'), data: JSON.stringify(folder) }).
           success(function (data, status, headers, config) {
               deferred.resolve(data);
           });

           return deferred.promise;
    }

}]);