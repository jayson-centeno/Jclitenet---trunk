define(['angular', 'moduleService', 'underscore'], function (ng, moduleService, _) {

    'use strict'

    return moduleService.factory('TutorialService', ['$resource', '$http', '$q', '$log', 'APP_CONST',
        
    function ($resource, $http, $q, $log, APP_CONST) {

        return {
            getAll: getAll
        }

        function getAll() {

            var self = this;
            var deferred = $q.defer();

            $http.get(APP_CONST.url.tutorials)
                 .success(function (data) {
                     self.tutorials = data;
                     deferred.resolve(data);
                 }).error(function (data) {
                     deferred.reject(data);
                     $log.error('error retrieving tutorials');
                 });

            return deferred.promise;

        }

    }]);

});