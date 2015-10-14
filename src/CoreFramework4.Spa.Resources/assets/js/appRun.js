define(['app'
    , 'authenticationService'
    , 'app-constants'
], function (app) {

    return app.run(['$rootScope', '$state', '$templateCache', 'AuthenticationService', 'APP_CONST',

    function ($rootScope, $state, $templateCache, authenticationService, APP_CONST) {

            console.log('run');

            $rootScope.page = {
                setTitle: function (title) {
                    this.title = title;
                },
            }

            $rootScope.$on('$locationChangeSuccess', function () {

                console.log('location change success');

                if (!authenticationService.isAuthenticated())
                                $state.go(APP_CONST.state.login);

            });

        }]);

});