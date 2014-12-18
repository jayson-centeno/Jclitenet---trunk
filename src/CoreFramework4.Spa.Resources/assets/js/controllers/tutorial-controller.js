define(['angular', 'moduleController', 'tutorialService'], function (ng, moduleController) {

    'use strict';

    return moduleController.controller("TutorialController", ['$scope', '$routeParams', '$location', 'TutorialService', '$q', function ($scope, $routeParams, $location, TutorialService, $q) {
        
        function LoadData() {
            TutorialService.getAll().then(function (data) {
                $scope.model = data;
            })
        }

        if (typeof $routeParams.id !== 'undefined') {

            $scope.refresh = function () {
                LoadData();
            }

            $scope.update = function () {

                TutorialService
                    .update({ id: $routeParams.id }, $scope.model)
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