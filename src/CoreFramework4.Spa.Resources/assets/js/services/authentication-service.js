define(['angular', 'moduleService'], function (ng, moduleService) {

    'user strict';

    return moduleService.service('AuthenticationService', ['$http', function ($http) {
        
        this.authenticate = function (email, password) {
            return $http.get('api/authentication/auth/' + email + '/' + password);
        };

    }]);

});