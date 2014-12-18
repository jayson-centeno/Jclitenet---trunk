'use strict'

define([
    'angular',
    'app',
    'homeController',
    "loginController",
    'tutorialController'],

    function (ng, app) {

        return app.config(function ($routeProvider) {

            $routeProvider

                .when('/', {
                    title: 'Home',
                    templateUrl: '../assets/views/index.html',
                    controller: 'HomeController',
                    cssClass: 'page-home'
                })
                .when('/login', {
                    title: 'Login',
                    templateUrl: '../assets/views/login.html',
                    controller: 'LoginController',
                    cssClass: 'page-login'
                })
                //.when('/tutorial/:id/:test', { //implement multiple params
                .when('/tutorial/:id', {
                    title: 'Update Tutorial',
                    templateUrl: '../assets/views/tutorials/tutorial.html',
                    controller: 'TutorialController',
                    cssClass: 'page-edittutorial'
                })
                .when('/createtutorial', {
                    title: 'Create Tutorial',
                    templateUrl: '../assets/views/tutorials/createtutorial.html',
                    controller: 'TutorialController',
                    cssClass: 'page-createtutorial'
                })
                .otherwise({
                    redirectTo: '../assets/views/login.html',
                });
        });

    }
)