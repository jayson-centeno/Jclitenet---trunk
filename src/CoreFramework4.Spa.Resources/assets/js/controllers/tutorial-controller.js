define(['angular', 'moduleController', "underscore"], function (ng, moduleController, _) {

    'use strict';

    return moduleController.controller("TutorialController", ['$scope', '$stateParams', '$location', 'TutorialService', '$q',

    function ($scope, $stateParams, $location, TutorialService, $q) {
        
        function LoadData() {
            TutorialService.getAll().then(function (data) {
                $scope.model = _.find(data, function (obj) {
                    return obj.Id === parseInt($stateParams.id)
                });
            })
        }

        if (typeof $stateParams.id !== 'undefined') {

            $scope.refresh = function () {
                LoadData();
            }

            $scope.update = function () {

                TutorialService
                    .update({ id: $stateParams.id }, $scope.model)
                    .$promise
                    .then(function () {
                        //success
                    })
            }

            LoadData();

        } else {

            $scope.save = function () {

                var newTutorial = new TutorialService();
                newTutorial.Id = $scope.model.Id;
                newTutorial.Description = $scope.model.Description;

                TutorialService
                    .save(newTutorial)
                    .$promise
                    .then(function (tutorial) {

                        $location.path('/tutorial/' + tutorial.Id);

                    })

            }

        }

    }]);

})