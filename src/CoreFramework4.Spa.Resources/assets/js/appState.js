'use strict'

define([
    'angular',
    'app',
    'homeController',
    'loginController',
],

    function (ng, app) {
     
        return app.config(function ($stateProvider, $urlRouterProvider) {

            console.log("appState loaded");

            // For any unmatched url, redirect to /state1
            $urlRouterProvider.otherwise('/');
            //.when('/tutorial/:id', '/tutorial/:id')
            //.when('/user/:id', '/contacts/:id')
            // If the url is ever invalid, e.g. '/asdf', then redirect to '/' aka the home state

            $stateProvider

                .state('index', {
                    url: '/',
                    templateUrl: '../assets/views/index.html',
                    controller: 'HomeController',
                    controllerAs: 'vm'
                })
                //.state('index.home', {
                //    url: '/',
                //    views: {
                //        tutorials: {
                //            templateUrl: '../assets/views/partial/tutorials.html',
                //            controller: 'HomeController',
                //            controllerAs: 'vm'
                //        }
                //    }
                //})

                .state('login', {
                    url: '/login',
                    templateUrl: '../assets/views/login.html',
                    controller: 'LoginController',
                    controllerAs: 'vm'
                })

                //.state('tutorial', {
                //     url: '/tutorial/:id',
                //     templateUrl: '../assets/views/tutorials/tutorial.html',
                //     controller: 'TutorialController',
                //     controllerAs: 'vm'
                //})

        });

    }
)