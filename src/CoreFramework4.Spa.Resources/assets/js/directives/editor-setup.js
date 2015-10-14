define(["angular", "moduleDirective", "authenticationService", "resumeService"], function (ng, moduleDirective) {

    'use strict';

    return moduleDirective.directive("editorSetup", function ($rootScope, $compile, $templateRequest, AuthenticationService, resumeService) {

        return {

            restrict: 'A',
            link: function (scope, element, attr) { //will not run if compile is active

                var editorSetupValue = attr.editorSetup;
                console.log(editorSetupValue);

                console.log('editorSetup link loaded');

                if (!AuthenticationService.isAuthenticated()) {
                    $(element).hide();
                    return;
                }

                $templateRequest('/assets/views/partial/editor-popover.html')
                       .then(function (responsePopover) {

                           var popoverTemplate = $(responsePopover);

                           $templateRequest('/assets/views/partial/editor-popover-content.html')
                                .then(function (responseContent) {

                                    $(element).popover({
                                        trigger: 'manual',
                                        content: responseContent,
                                        template: popoverTemplate,
                                        animation:false,
                                        placement: "right",
                                        html: true
                                    }).on("mouseenter", function () {

                                        var _this = this;
                                        $(this).popover("show");
                                        $(this).siblings(".popover")
                                               .on("mouseleave", function () { //popup
                                                    $(_this).popover('hide');
                                               })

                                    }).on("mouseleave", function () { //element

                                        var _this = this;
                                        setTimeout(function () {
                                            if (!$(".popover:hover").length) {
                                                $(_this).popover("hide")
                                            }
                                        }, 100);

                                    }).on("shown.bs.popover", function () {
                                        
                                        $(this)
                                           .siblings(".popover")
                                           .find('.popover-content i')
                                           .unbind('click')
                                           .click(function () {

                                               console.log('clicked edit');

                                               var modalBody = $('#mainModal').find('.modal-body');

                                               if ($('#mainModal').length > 0) {

                                                   $('#mainModal').find('.modal-title').text(attr.editorTitle);
                                                   var attributes = $.map(modalBody[0].attributes, function (item) {
                                                        return item.name;
                                                   });

                                                   $.each(attributes, function (i, item) {
                                                       if (item == "class") return;
                                                       $(modalBody).removeAttr(item);
                                                   });

                                                   var editorSetupAttrTest = $(modalBody).attr(editorSetupValue);
                                                   if (typeof editorSetupAttrTest == typeof undefined || editorSetupAttrTest === false) {

                                                       $(modalBody).attr(editorSetupValue, '');

                                                       var html = $(modalBody)[0].outerHTML;
                                                       var compiled = $compile(html)(scope);

                                                       var parent = $(modalBody).parent();
                                                       $(modalBody).remove();
                                                       $(parent).find(".modal-header").after(compiled);

                                                   }

                                                   $('#mainModal').modal('show');
                                                   
                                               }    
                                               else {

                                                   $templateRequest('/assets/views/partial/editor-setup.html')
                                                          .then(function (responseEditor) {

                                                              var mainHtml = $(responseEditor);
                                                              $(mainHtml).find('.modal-body')
                                                                         .attr(editorSetupValue, '');

                                                              $(mainHtml).find('.modal-title').text(attr.editorTitle);

                                                              var html = angular.element($(mainHtml)[0].outerHTML);

                                                              var compiled = $compile(html)(scope);
                                                              angular.element('body').append(compiled);
                                                              $('#mainModal').modal('show');

                                                          })

                                               }

                                           })

                                    })

                                });

                       })

            },
            controller: function () {
                console.log('editorSetup controller loaded');
            }

        }

    })

})