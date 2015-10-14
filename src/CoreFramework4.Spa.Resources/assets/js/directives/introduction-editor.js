define(["angular", "moduleDirective", "resumeService"], function (ng, moduleDirective, resumeService) {

    'use strict'

    return moduleDirective.directive('introductionEditor', function ($compile, resumeService) {

        return {

            restrict: 'A',
            scope: { data: "=" },
            link: function (scope, element, attributes) {

                console.log('introductionEditor LINK has been loaded');

                resumeService
                   .getDetailsOnly()
                   .then(function (data) {
                       scope.data = data;
                   })

                scope.save = Save;

                function Save()
                {
                    resumeService
                        .saveIntroduction(scope.data.Introduction)
                        .then(function () {
                            
                            $("[editor-setup=" +
                                attributes.$attr.introductionEditor + "]")
                                    .text(scope.data.Introduction);

                            $('#mainModal').modal('hide');

                        })
                }

            },

            templateUrl: '/assets/views/partial/introduction-editor.html',
            controller: function ($scope) {
                console.log('introductionEditor CONTROLLER has been loaded');
            }
        }

    });

})