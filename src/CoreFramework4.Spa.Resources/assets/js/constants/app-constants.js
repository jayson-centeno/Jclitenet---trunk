'use strict';

define(['angular'], function (ng) {

    ng.module("spaweb")
      .constant('APP_CONST', {
          url: { tutorials: '/app/getalltutorials' }
      });

})