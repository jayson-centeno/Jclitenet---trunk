'use strict';

define(['angular' , 'app'], function (ng, app) {

    app.constant('APP_CONST', {
        url: {
            tutorials: '/app/getalltutorials',
            authentication: '/authenticate',
            resumeDetails: '/app/GetResumeDetails',
            updateResumeIntroduction: '/app/UpdateResumeIntroduction'
        },
        state: {
            login : "login",
            home : "index"
        },
        auth : {
            name : 'default'
        },
        profileEditor:
        { 
            introduction: "introduction"
        }
    });

})