define(['angular', 'moduleDirective'], function (ng, moduleDirective) {

    'use strict';

    return moduleDirective.directive("headerFixed", function () {
        
        return {
            
            restrict: 'A',
            link: function (scope, element, attributes) {

                console.log('headerFixed link');

                var wrap = $(window);
                wrap.scroll(function (e) {

                    if ($(this).scrollTop() >= 120) {

                        if (!$(element).hasClass('fix')) {
                            $(element).addClass("fix");
                            $(element).hide().fadeIn(1000);
                        }

                    } else {

                        if ($(element).hasClass('fix')) {
                            $(element).removeClass("fix");
                            $(element).hide().fadeIn();
                        }
                    }

                });


            },
            controller: function ($scope) {

                console.log('headerFixed controller');

            }

        }

    });

})