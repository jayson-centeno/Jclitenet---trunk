'use strict';

requirejs.config({

    baseUrl: "../assets/js",

    paths: {

        "angular": "lib/angular/angular.min",
        "angularRoute": "lib/angular/angular-route.min",
        "angularResource": "lib/angular/angular-resource.min",
        "angularCookies": "lib/angular/angular-cookies.min",
        "angularAnimate": "lib/angular/angular-animate.min",
        //"jquery": [
        //    "//ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min", // CDN
        //    "lib/jquery/jquery-2.1.1.min"],

        "q": "lib/q/q.min",
        "underscore": "lib/underscore/underscore.min",

        //modules
        "moduleController": "modules/module-controller",
        "moduleService": "modules/module-service",

        //constants
        "app-constants": "constants/app-constants",

        //controllers
        "homeController": "controllers/home-controller",
        "loginController": "controllers/login-controller",
        "tutorialController": "controllers/tutorial-controller",

        //services
        "baseService": "services/base-service",
        "tutorialService": "services/tutorial-service",
        "authenticationService": "services/authentication-service"
    },

    waitSeconds: 0,

    shim: {
        "angular": {
            //deps : ["jquery"],
            exports: "angular"
        },
        'jquery': {
            exports: '$'
        },
        'underscore': {
            exports: '_'
        },
		"angularRoute": ['angular'],
		"angularResource": ['angular'],
		"angularCookies": ['angular'],
		"angularAnimate": ['angular']
    },

    priority: ["angular"]

});

requirejs(["angular", "app", "routes", "appRun"], function (ng, app) {

    "use strict";

    try {

        ng.element(document).ready(function () {
            ng.bootstrap(document, [app['name']]);
        });

    } catch (e) {
        console.error(e.stack || e.message || e);
    }

})