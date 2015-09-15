'use strict';

requirejs.config({

    baseUrl: "../assets/js",

    paths: {

        "angular": [ "//ajax.googleapis.com/ajax/libs/angularjs/1.4.3/angular.min",
                     "lib/angular/angular.min"],

        "angularUIRoute": ["//cdnjs.cloudflare.com/ajax/libs/angular-ui-router/0.2.15/angular-ui-router",
                           "lib/angular/angular-ui-router.min"],

        "angularResource":["//ajax.googleapis.com/ajax/libs/angularjs/1.4.3/angular-resource.min",
                           "lib/angular/angular-resource.min"],

        "angularCookies": ["//ajax.googleapis.com/ajax/libs/angularjs/1.4.3/angular-cookies.min", 
                           "lib/angular/angular-cookies.min"],

        "angularAnimate": ["//ajax.googleapis.com/ajax/libs/angularjs/1.4.3/angular-animate.min",
                           "lib/angular/angular-animate.min"],

        "angularMessages": ["//ajax.googleapis.com/ajax/libs/angularjs/1.4.3/angular-messages.min",
                            "lib/angular/angular-messages.min"],

        "q": ["//cdnjs.cloudflare.com/ajax/libs/q.js/0.9.2/q.min",
              "lib/q/q.min"],

        "underscore": ["//cdnjs.cloudflare.com/ajax/libs/underscore.js/1.8.3/underscore-min",
                       "lib/underscore/underscore-min"],

        //modules
        "moduleController":"modules/module-controller",
        "moduleService": "modules/module-service",

        //constants
        "app-constants": "constants/app-constants",

        //controllers
        "homeController": "controllers/home-controller",
        "loginController": "controllers/login-controller",
        "tutorialController": "controllers/tutorial-controller",

        //services
        "tutorialService": "services/tutorial-service",
        "authenticationService": "services/authentication-service",

        //utilities
        "util": "utilities/utilities",
    },

    waitSeconds: 0,

    shim: {
        "angular": {
            exports: "angular"
        },
        'underscore': {
            exports: '_'
        },
		"angularResource": ['angular'],
		"angularCookies": ['angular'],
		"angularAnimate": ['angular'],
		"angularMessages": ['angular'],
		"angularUIRoute": {
            deps: ['angular']
        }
    },

    priority: ["angular"]

});

requirejs(["angular", "app", "appState", "appRun"], function (ng, app) {

    "use strict";

    try {

        ng.element(document).ready(function () {
            ng.bootstrap(document, [app['name']]);
        });

    } catch (e) {
        console.error(e.stack || e.message || e);
    }

})