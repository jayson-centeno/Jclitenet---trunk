define(['angular', 'moduleDirective', 'resumeService'], function (ng, moduleDirective) {

    return moduleDirective.directive("editorValue", function (APP_CONST, resumeService) {

        return {
        
            restrict : 'A',
            link : function (scope, element, attributes) {
                
                resumeService
                   .getDetailsOnly()
                   .then(function (data) {

                       switch(attributes.editorValue)
                       {
                           case APP_CONST.profileEditor.introduction:
                               $(element).text(data.Introduction);
                               break;
                       }

                   })

            }

        }

    })

})