define(['angular', 'moduleService', 'underscore', 'app-constants'], function (ng, moduleService, _) {

    'use strict'

    return moduleService.factory('TutorialService', ['$resource', '$http', '$q', '$log', 'APP_CONST', function ($resource, $http, $q, $log, APP_CONST) {
        
        var tutorialService = function () {
            this.tutorials = [];

            //this.loadingTracker = PromiseTracker({ activationDelay: 500, minDuration: 750 });

            this.initialize.apply(this, arguments);
        };

        _.extend(tutorialService.prototype, {

            initialize: function () { },
            getAll: function () {
                
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

        })

        return new tutorialService();

    }]);

});