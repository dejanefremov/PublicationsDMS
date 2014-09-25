window.publication = angular.module('publication', ['ngRoute', 'LocalStorageModule', 'angular-loading-bar', 'PublicationsControllers']);

publication.config(['$routeProvider', '$httpProvider', function ($routeProvider, $httpProvider) {
    $routeProvider
        .when('/list', { templateUrl: 'AngularJS/App/Node/List/NodeList.view.html', controller: 'NodeListController' })
        .when('/list/:parentNodeID', { templateUrl: 'AngularJS/App/Node/List/NodeList.view.html', controller: 'NodeListController' })

        .when('/', { templateUrl: 'AngularJS/App/Node/PublicList/PublicNodeList.view.html', controller: 'PublicNodeListController' })
        .when('/public', { templateUrl: 'AngularJS/App/Node/PublicList/PublicNodeList.view.html', controller: 'PublicNodeListController' })
        .when('/public/:parentNodeID', { templateUrl: 'AngularJS/App/Node/PublicList/PublicNodeList.view.html', controller: 'PublicNodeListController' })

        .when('/folder/edit', { templateUrl: 'AngularJS/App/Folder/Edit/FolderEdit.view.html', controller: 'FolderEditController' })
        .when('/folder/edit/:parentNodeID', { templateUrl: 'AngularJS/App/Folder/Edit/FolderEdit.view.html', controller: 'FolderEditController' })
        .when('/folder/edit/:parentNodeID/:currentNodeID', { templateUrl: 'AngularJS/App/Folder/Edit/FolderEdit.view.html', controller: 'FolderEditController' })

        .when('/document/edit/:currentNodeID', { templateUrl: 'AngularJS/App/Document/Edit/DocumentEdit.view.html', controller: 'DocumentEditController' })
        .when('/document/details/:currentNodeID', { templateUrl: 'AngularJS/App/Document/Details/DocumentDetails.view.html', controller: 'DocumentDetailsController' })

        .when('/document/upload', { templateUrl: 'AngularJS/App/Document/Upload/DocumentUpload.view.html', controller: 'DocumentUploadController' })
        .when('/document/upload/:parentNodeID', { templateUrl: 'AngularJS/App/Document/Upload/DocumentUpload.view.html', controller: 'DocumentUploadController' })

        .when('/document/search/:searchingTerm', { templateUrl: 'AngularJS/App/Document/Search/DocumentSearch.view.html', controller: 'DocumentSearchController' })
        .when('/document/search/:searchingTerm/:parentNodeID', { templateUrl: 'AngularJS/App/Document/Search/DocumentSearch.view.html', controller: 'DocumentSearchController' })

        .when('/node/share/:parentNodeID/:nodeID', { templateUrl: 'AngularJS/App/Node/Share/NodeShare.view.html', controller: 'NodeShareController' })

        .when('/user/list', { templateUrl: 'AngularJS/App/User/List/UserList.view.html', controller: 'UserListController' })
        .when('/user/add', { templateUrl: 'AngularJS/App/User/Add/UserAdd.view.html', controller: 'UserAddController' })
        .when('/user/edit', { templateUrl: 'AngularJS/App/User/Edit/UserEdit.view.html', controller: 'UserEditController' })
        .when('/user/edit/:userID', { templateUrl: 'AngularJS/App/User/Edit/UserEdit.view.html', controller: 'UserEditController' })
        .when('/user/changepassword/:userID', { templateUrl: 'AngularJS/App/User/ChangePassword/ChangePassword.view.html', controller: 'ChangePasswordController' })

        .when('/login', { templateUrl: 'AngularJS/App/User/Login/Login.view.html', controller: 'LoginController' });

    $httpProvider.interceptors.push('authorizationInterceptor');
}]);


window.PublicationsControllers = angular.module('PublicationsControllers', []);
