var WebAPI = angular.module('WebAPI', ['ngRoute']);
WebAPI.config(function ($routeProvider) {
    $routeProvider.when('/',
        {
            redirectTo: '/MyHome' //za sada
            }).when('/MyHome',
            {
                controller: 'MyHomeController',
                templateUrl: 'HTMLStranice/MyHome.html',
                activeTab: 'Home'
                //}).
            }).when('/Register',
                {
                    controller: 'JSRegILogController',
                    templateUrl: 'HTMLStranice/Register.html',
                    activeTab: 'Register'
                })
            .when('/Login',
            {
                controller: 'JSRegILogController',
                templateUrl: 'HTMLStranice/Login.html',
                activeTab: 'Login'
            }).when('/Profile',
            {
                controller: 'ProfileController',
                templateUrl: 'HTMLStranice/Profile.html',
                activeTab: 'Profile'
            }).when('/Profile/:username',
            {
                controller: 'ProfileController',
                templateUrl: 'HTMLStranice/Profile.html',
                activetab: 'Profile'
            }).when('/DriveGet/:username',
            {
                controller: 'ProfileController',
                templateUrl: 'HTMLStranice/DriveGet.html',
                activetab: 'none'
            }).when('/DriverGet/:username',
            {
                controller: 'JSRegILogController',
                templateUrl: 'HTMLStranice/DriverGet.html',
                activetab: 'none'
            }).when('/Edit/:username',
            {
                controller: 'ProfileController',
                templateUrl: 'HTMLStranice/Edit.html',
                activeTab: 'Edit'
            }).when('/DodajKomentar',
            {
                controller: 'CommentController',
                templateUrl: 'HTMLStranice/Comment.html',
                activeTab: 'none'
            }).when('/ZavrsiVoznju',
            {
                controller: 'CommentController',
                templateUrl: 'HTMLStranice/ZavrsiVoznju.html',
                activeTab: 'none'
            })
});