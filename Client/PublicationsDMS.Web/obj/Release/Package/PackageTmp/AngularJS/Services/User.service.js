publication.factory('UserService', ['$http', '$q', function ($http, $q) {
    return {
        getUserInfo: _getUserInfo,
        getAllUsers: _getAllUsers,
        getUser: _getUser,
        saveUser: _saveUser,
        changeUserPassword: _changeUserPassword
    };

    function _getUserInfo() {
        var deferred = $q.defer();

        $http({ method: 'GET', url: config.generateApiUrl('Api/UserInfo') }).
            success(function (data, status, headers, config) {
                deferred.resolve(data);
            });

        return deferred.promise;
    }

    function _getAllUsers() {
        var deferred = $q.defer();

        $http({ method: 'GET', url: config.generateApiUrl('Api/UserList') }).
           success(function (data, status, headers, config) {
               deferred.resolve(data);
           });

           return deferred.promise;
    }

    function _getUser(userID) {
        var deferred = $q.defer();

        $http({ method: 'GET', url: config.generateApiUrl('Api/UserEdit'), params: { "userID": userID == null ? "" : userID } }).
            success(function (data, status, headers, config) {
                deferred.resolve(data);
            });

            return deferred.promise;
    }

    function _saveUser(user) {
        var deferred = $q.defer();

        var httpMethod = "POST";
        if (!user.UserData.UserID || user.UserData.UserID == 0) {
            httpMethod = "PUT";
        }

        $http({ method: httpMethod, url: config.generateApiUrl('Api/UserEdit'), data: JSON.stringify(user) }).
           success(function (data, status, headers, config) {
               deferred.resolve(data);
           });

           return deferred.promise;
    }

    function _changeUserPassword(user) {
        var deferred = $q.defer();

        $http({ method: 'POST', url: config.generateApiUrl('Api/ChangePassword'), data: JSON.stringify(user) }).
           success(function (data, status, headers, config) {
               deferred.resolve(data);
           });

           return deferred.promise;
    }
}]);