define(['angular', 'moduleService', 'util'], function (ng, moduleService, util) {

    'user strict';

    return moduleService.service('AuthenticationService', ['$http', '$q', '$log', '$rootScope', '$cookies', 'APP_CONST',

    function ($http, $q, $log, $rootScope, $cookies, APP_CONST) {
        
        console.log("AuthenticationService loaded");

        return  {
            authenticate: authenticate,
            isAuthenticated: isAuthenticated,
            clearCredentials: clearCredentials,
            setCredentials: setCredentials
        };

        //pubilc functions
        function authenticate(email, password) {

            var self = this;
            var deferred = $q.defer();

            $http.get(APP_CONST.url.authentication + "/" + email + '/' + password)
                 .success(function (data) {

                     if (data) self.setCredentials(email, password);
                     deferred.resolve(data);

                 }).error(function (data) {
                     deferred.reject(data);
                     $log.error('error retrieving tutorials');
                 });

            return deferred.promise;
        };

        function isAuthenticated()
        {
            $rootScope.globals = $cookies.getObject(APP_CONST.auth.name);

            var wasAuthenticated = ng.isDefined($rootScope.globals) &&
                                    ng.isDefined($rootScope.globals.currentUser);

            if (wasAuthenticated)
                WriteToHttpHeader($rootScope.globals.currentUser.authData);

            return wasAuthenticated;
        }

        function clearCredentials()
        {
            $rootScope.globals = {};    
            $http.defaults.headers.common['Authorization'] = 'Basic';
            $cookies.remove(APP_CONST.auth.name);
        }

        //private functions
        function WriteToHttpHeader(authData)
        {
            $http.defaults.headers.common['Authorization'] = 'Basic ' + authData;
        }

        function setCredentials(email, password)
        {
            var auth = util.Base64.encode(email + ':' + password);

            $rootScope.globals = {
                currentUser: {
                    email: email,
                    authData: auth
                }
            }

            WriteToHttpHeader(auth);
            $cookies.putObject(APP_CONST.auth.name, $rootScope.globals);
        }

    }]);

});