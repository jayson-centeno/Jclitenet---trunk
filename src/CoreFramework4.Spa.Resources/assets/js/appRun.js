define([
    'app',
    'baseService'
], function (app) {

    return app.run(['$rootScope', 'BaseService', function ($rootScope, BaseService) {

        $rootScope.page = {
            setTitle: function (title) {
                this.title = title;
            },

            setClass: function (cssClass) {
                this.cssClass = cssClass;
            }
        }

        $rootScope.$on('$routeChangeSuccess', function (event, current, previous) {

            $rootScope.page.setTitle(current.$$route.title || 'Default Title');
            $rootScope.page.setClass(current.$$route.cssClass || 'page');

            $rootScope.Profile = new BaseService.Profile();

        });

    }]);
});