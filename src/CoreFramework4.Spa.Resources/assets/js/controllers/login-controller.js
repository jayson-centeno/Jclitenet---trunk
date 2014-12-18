define(['angular', 'moduleController', 'authenticationService'], function (ng, moduleController) {

    'use strict';

    return moduleController.controller('LoginController', ['$scope', '$location', 'AuthenticationService', function ($scope, $location, AuthenticationService) {
        
        $scope.login = function () {

            var email = $scope.model.email;
            var password = $scope.model.password;

            AuthenticationService
                .authenticate(email, password)
                .success(function () {

                    $location.path("/");

                }).error(function (error) {

                    alert('error');

                });

        }

    }]);

});