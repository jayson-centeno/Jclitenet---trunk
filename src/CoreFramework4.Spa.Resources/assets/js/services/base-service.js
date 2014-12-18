define(['angular', 'moduleService'], function (ng, moduleService) {

    'use strict';

    return moduleService.factory('BaseService', [function () {

        return {

            Profile : function() {
            
                this.FirstName= 'tset',
                this.LastName = 'test'
            
            }

        }

    }])

});