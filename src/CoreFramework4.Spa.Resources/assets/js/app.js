define(['angular'
    , 'angularRoute'
    , 'angularCookies'
    , 'angularResource'
    , 'angularAnimate'
    , 'moduleController'
    , 'moduleService'
],
    function (ng) {
        return ng.module("spaweb", ['spaweb.controllers', 'spaweb.services', 'ngRoute', 'ngCookies', 'ngResource', 'ngAnimate']);
});