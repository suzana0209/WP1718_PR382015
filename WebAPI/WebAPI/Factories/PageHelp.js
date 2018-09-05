var WebAPI = angular.module('WebAPI', ['ngRoute']);
WebAPI.config(function ($routeProvider) {
    $routeProvider.when('/',
        {
            redirectTo: '/MyHome'
            }).when('/MyHome',
            {
                controller: 'MyHomeController',
                templateUrl: 'HTMLStranice/MyHome.html',
                activeTab: 'Home'
            })
            .when('/Register',
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
            }).when('/DriveGet',
            {
                controller: 'ProfileController',
                templateUrl: 'HTMLStranice/DriveGet.html',
                activetab: 'none'
            }).when('/DriverGet',
            {
                controller: 'JSRegILogController',
                templateUrl: 'HTMLStranice/DriverGet.html',
                activetab: 'none'
            }).when('/Edit',
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
            }).when('/IzmeniVoznju',
            {
                controller: 'ProfileController',
                templateUrl: 'HTMLStranice/IzmeniVoznju.html',
                activeTab: 'none'
            }).when('/IzmeniLokaciju',
            {
                controller: 'ProfileController',
                templateUrl: 'HTMLStranice/IzmeniLokaciju.html',
                activeTab: 'none'
            }).when('/ObradiVoznju',
            {
                controller: 'ProfileController',
                templateUrl: 'HTMLStranice/ObradiVoznju.html',
                activeTab: 'none'
            }).when('/BlockUsers',
            {
                controller: 'BlockController',
                templateUrl: 'HTMLStranice/BlockUsers.html',
                activeTab: 'none'
            })

});