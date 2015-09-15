define(['angular', 'moduleController', 'authenticationService'], function (ng, moduleController) {

    'use strict';

    return moduleController.controller('LoginController', ['$state', 'AuthenticationService', 'APP_CONST',

    function ($state, _authenticationService, APP_CONST) {

        console.log('LoginController loaded');

        var vm = this;
        vm.login = login;

        (function init() {
            _authenticationService.clearCredentials();
        })();

        function login() {

            vm.dataLoading = true;

            _authenticationService
                .authenticate(vm.email, vm.password)
                .then(function (response) {
                    if (response)
                        $state.go(APP_CONST.state.home);
                    else vm.dataLoading = false;
                });

        }

    }]);

});