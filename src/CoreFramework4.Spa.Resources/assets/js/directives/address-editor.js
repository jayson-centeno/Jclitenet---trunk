define(["angular", "moduleDirective"], function (ng, moduleDirective) {

    'use strict'

    return moduleDirective.directive('addressEditor', function () {

        return {

            restrict: 'A',
            link: function (scope, element, attributes)
            {
                console.log('addressEditor link loaded');
            },
            templateUrl: '/assets/views/partial/address-editor.html',
        }

    });

})