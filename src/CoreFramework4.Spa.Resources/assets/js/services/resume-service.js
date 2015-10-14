define(["angular", "moduleService"], function (ng, moduleService) {

    'user strict';

    return moduleService.service('resumeService', ['$http', '$q', '$log', 'APP_CONST', 'AuthenticationService',

    function ($http, $q, $log, APP_CONST, AuthenticationService) {

        console.log("resumeService loaded");

        if (!AuthenticationService.isAuthenticated()) return;

        return {

            getDetailsOnly: getDetailsOnly,
            saveIntroduction: saveIntroduction

        }

        function getDetailsOnly() {

            var deferred = $q.defer();

            $http.get(APP_CONST.url.resumeDetails)
                 .success(function (data) {
                     if (data) { deferred.resolve(data); }
                }).error(function (data) {
                    deferred.reject(data);
                    $log.error('error retrieving resume details');
                });

            return deferred.promise;

        }

        function saveIntroduction(introduction) {
                    
            var deferred = $q.defer();

            //$http({
            //    method: 'POST',
            //    url: APP_CONST.url.updateResumeIntroduction,
            //    data: { value: introduction },
            //    headers: { 'contentType': 'application/json' },
            //    dataType: 'json',
            //}).success(function (data, status, headers, config) {
            //    deferred.resolve();
            //}).error(function (data, status, headers, config) {
            //    $log.error('error posting data');
            //});

            //return deferred.promise;


            $http.post(APP_CONST.url.updateResumeIntroduction, { "Data": introduction })
                 .success(function () {
                     deferred.resolve();
                 }).error(function () {
                     $log.error('error posting data');
                 });

            return deferred.promise;

        }

    }])


})