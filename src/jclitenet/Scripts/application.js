function fixLoginPosition() {
    $(".logindisplay-wrapper").height("0");
    $("#logindisplay")
        .unbind('click')
        .click(function () {

            if ($(".logindisplay-wrapper").height() > 0) {
                $(".logindisplay-wrapper").animate(
                    { height: 0 },
                    500,
                    function () { $(this).hide(); }
                );
            } else {
                $(".logindisplay-wrapper").show();
                $(".logindisplay-wrapper").animate(
                    { height: 100 },
                    500
                );
            }

        });
}

function autoSelectMenu() {
    $('#menu li').removeClass("current")
    $('#menu li a').each(function (index) {
        if (this.href == window.location.href)
            $(this.parentElement).addClass("current");
    });
}

function setupSearchButton() {
    $("#seachIconButton").click(function () {
        $("#txtSearch").focus();
    });
}

function initInspiration() {
    var logo = $('#inspiration');
    if (logo.lenght > 0) {
        var originalPos = logo.offset().top;
        $(window).scroll(function () {
            var elpos = logo.offset().top;
            var windowpos = $(window).scrollTop();
            var finaldestination = windowpos + originalPos;
            logo.stop().animate({ 'top': finaldestination }, 1000);
        });
    }
}

function registerSimpleMenu() {
    $("#submenu").css({ display: "none" });
    $("#submenu").css({ visibility: "hidden" }).hover(function () {
        $(this).css({ background: "#eee" });
    }, function () {
        $(this).hide(300);
        $(this).css({ background: "#ccc" });
    });
    $(".submenu").hover(function () {
        initDevelopmentSubMenu();
        $("#submenu").css({ visibility: "visible", display: "none" }).show(350);
    }, function () {
    })
}

function initTutorialPaging()
{
    $('#tutorialNextResultButton h2').hide();
    $('#tutorialNextResultButton .icon-spinner').fadeIn();

    showAjaxContent("/Partial/PartialTutorials/", function (result) {

        $('.tutorial-list-container').append(result);

        var nextPage = $(".tutorial-list-container").attr('data-next-page', 2);

        $('#tutorialNextResultButton .icon-spinner').hide();
        $('#tutorialNextResultButton h2').fadeIn();
    });

    $('#tutorialNextResultButton').unbind('click').click(function () {
        
        $('#tutorialNextResultButton h2').hide();
        $('#tutorialNextResultButton .icon-spinner').fadeIn();

        var nextPage = $(".tutorial-list-container").attr('data-next-page');

        if (typeof nextPage == 'undefined') { nextPage = 1; }

        showAjaxContent("/Partial/PartialTutorials?page=" + nextPage, function (result) {

            $('#tutorialNextResultButton .icon-spinner').hide();
            $('#tutorialNextResultButton h2').fadeIn();

            if (typeof result.length == 'undefined') return;

            $('.tutorial-list-container').append(result)
                                         .attr('data-next-page', parseInt(nextPage) + 1);

            $(result).fadeIn('slow');

        });

    });
}

function showAjaxContent(url, callback) {

    $.ajax({
        url: url,
        type: 'GET',
        cache: 'true',
        success: function (result) {
            if (typeof callback != 'undefined') { callback(result); }
        },
        error: function (xhr, errorType, exception) { //Triggered if an error communicating with server  
            var errorMessage = exception || xhr.statusText; //If exception null, then default to xhr.statusText  
        }
    });
}

$(document).ready(function () {
    autoSelectMenu();
    setupSearchButton();
    fixLoginPosition();
    initInspiration();
    registerSimpleMenu();
    initTutorialPaging();
});

var coreBasePlugin = function() {
    
    popMessage = function(message) {
        alert(message);
    },

    getElemBySelector = function(selector){

        if (selector == '') return selector;
        if (selector.indexOf(".") == -1) {
            selector = "#" + selector;
        }
        
        return $(selector);
    },

    downloadCss = function (cssPath, callbackkSucess) {
        if ($('head').find("link[href*='" + cssPath + "']").length == 0) {
            $('head').append("<link href='" + cssPath + this.getTimeStamp() + "' rel='stylesheet' type='text/css' />");
        }
        if (typeof callbackkSucess != 'undefined') callbackkSucess();
    },

    getRootDomainUrl = function () {
        return location.protocol + "//" + location.host;
    },

    this.removeCssReference = function (cssPath, callbackkSucess) {
        $('head').find("link[href*='" + cssPath + "']").remove();
        if (typeof callbackkSucess != 'undefined') callbackkSucess();
    },

    getTimeStamp = function () {
        return "?timestamp=" + new Date().getTime();
    }

}

var GetRootDomain = function () {
    var http = location.protocol;
    var slashes = http.concat("//");
    return slashes.concat(window.location.hostname);
}
