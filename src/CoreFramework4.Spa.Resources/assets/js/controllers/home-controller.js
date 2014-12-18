define(['angular', 'moduleController', 'tutorialService'], function (ng, moduleController) {

    'use strict';

    return moduleController.controller('HomeController', ['$scope', 'TutorialService', function ($scope, TutorialService) {

        function LoadTutorials() {
            TutorialService.getAll().then(function (data) {
                $scope.model = data;
            })
        }

        LoadTutorials();

        $scope.delete = function (tutorial) {

            TutorialService
                .delete({ id: tutorial.Id })
                .$promise
                .then(function() {
                    alert('removed');
                });
        }

    }]);

});