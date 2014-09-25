publication.factory('DocumentService', ['$http', '$q', function ($http, $q) {
    return {
        getDocument: _getDocument,
        saveDocument: _saveDocument,
        downloadDocument: _downloadDocument,
        uploadDocuments: _uploadDocuments,
        saveUploadedDocuments: _saveUploadedDocuments,
        searchDocuments: _searchDocuments
    };

    function _getDocument(documentID) {
        var deferred = $q.defer();

        $http({ method: 'GET', url: config.generateApiUrl('Api/Document'), params: { "documentID": documentID == null ? "" : documentID } }).
            success(function (data, status, headers, config) {
                deferred.resolve(data);
            });

        return deferred.promise;
    }

    function _downloadDocument(documentID) {
        $http({ method: 'GET', url: config.generateApiUrl('Api/Download'), responseType: "arraybuffer", params: { "documentID": documentID == null ? "" : documentID } }).
            success(function (data, status, headers, config) {
                headers = headers();
                var contentType = headers["content-type"];
                var fileName = headers["x-filename"];

                if (navigator.msSaveBlob) {
                    var blob = new Blob([data], { type: contentType });
                    navigator.msSaveBlob(blob, fileName);
                }
                else {
                    var urlCreator = window.URL || window.webkitURL || window.mozURL || window.msURL;
                    if (urlCreator) {
                        var link = document.createElement("a");
                        var blob = new Blob([data], { type: contentType });
                        var url = urlCreator.createObjectURL(blob);
                        link.setAttribute("href", url);
                        link.setAttribute("download", fileName);

                        // Simulate clicking the download link
                        var event = document.createEvent('MouseEvents');
                        event.initMouseEvent('click', true, true, window, 1, 0, 0, 0, 0, false, false, false, false, 0, null);
                        link.dispatchEvent(event);
                    }
                }
            });
    }

    function _uploadDocuments(files) {
        var deferred = $q.defer();

        $http({
            method: 'POST',
            url: config.generateApiUrl('Api/Document'),
            data: files,
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        }).
            success(function (data, status, headers, config) {
                deferred.resolve(data);
            });

        return deferred.promise;
    }

    function _saveUploadedDocuments(parentFolderID, documents) {
        var deferred = $q.defer();

        $http({ method: 'PUT', url: config.generateApiUrl('Api/Document'), data: JSON.stringify({ parentFolderID: parentFolderID, documents: documents }) }).
           success(function (data, status, headers, config) {
               deferred.resolve(data);
           });

        return deferred.promise;
    }

    function _saveDocument(document) {
        var deferred = $q.defer();

        $http({ method: 'POST', url: config.generateApiUrl('Api/DocumentEdit'), data: JSON.stringify(document) }).
           success(function (data, status, headers, config) {
               deferred.resolve(data);
           });

        return deferred.promise;
    }

    function _searchDocuments(parentFolderID, searchingTerm) {
        var deferred = $q.defer();

        $http({ method: 'GET', url: config.generateApiUrl('Api/DocumentSearch'), params: { "parentFolderID": parentFolderID == null ? "" : parentFolderID, "searchingTerm": searchingTerm } }).
            success(function (data, status, headers, config) {
                deferred.resolve(data);
            });

        return deferred.promise;
    }

}]);