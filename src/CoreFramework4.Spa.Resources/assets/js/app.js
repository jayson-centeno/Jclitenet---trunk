define(['angular'
    , 'angularUIRoute'
    , 'angularCookies'
    , 'angularResource'
    , 'angularAnimate'
    , 'angularMessages',
    , 'moduleController'
    , 'moduleService'
    , 'moduleDirective'
],
    function (ng) {

        console.log('app loaded');

        return ng.module("spaweb", ['ngCookies', 'ngResource', 'ngAnimate', 'ui.router', 'spaweb.controllers', 'spaweb.services', 'spaweb.directives']);
});