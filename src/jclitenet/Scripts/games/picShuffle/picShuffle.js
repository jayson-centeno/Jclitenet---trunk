/// <reference path="../application.js" />

(function ($) {

    var config = {};

    var pluginConstants = {
        EMPTY_VALUE: '',
        DOT_VALUE: '.',
        ZERO_VALUE: 0
    }

    var self = this;

    var myClass = function (value) {
        this.Value = value;
    }

    var defaults = {
        skinId: 1,
        skinNaming: 'skin_',
        skinDefaulClass: 'index.css',
        rootPath: '../../../../Scripts/games/picShuffle/',
        defaultImagesExt: '.png',
        shaffleButtonId: 'btnShaffle',
        defaultShaffleDimension: 9,
        defaultDesign:
            "<div id='picShuffleMenuContainer'>" +
                "<div class='picShuffleMenuContainer'>" +
                    "<input id='btnShaffle' type='button' value='Shuffle' />" +
                "</div>" +
                "<div class='picShuffleContainer'>" +
                    "<div>" +
                        "<img alt='' src='' />" +
                    "</div>" +
                    "<div>" +
                        "<img alt='' src='' />" +
                    "</div>" +
                    "<div>" +
                        "<img alt='' src='' />" +
                    "</div>" +  
                    "<div>" +
                        "<img alt='' src='' />" +
                    "</div>" +
                    "<div>" +
                        "<img alt='' src='' />" +
                    "</div>" +
                    "<div>" +
                        "<img alt='' src='' />" +
                    "</div>" +
                    "<div>" +
                        "<img alt='' src='' />" +
                    "</div>" +
                    "<div>" +
                        "<img alt='' src='' />" +
                    "</div>" +
                    "<div>" +
                        "<img alt='' src='' />" +
                    "</div>" +
                "</div>" +
                "<div class='clear'>" +
                "</div>" +
            "</div>"
    };

    var global = {
        selected: '',
        selector: ''
    };

    var targetElement;

    var init = $.prototype.init;
    $.prototype.init = function (selector, context) {
        var r = init.apply(this, arguments);
        if (selector && selector.selector) {
            r.context = selector.context, r.selector = selector.selector;
        }
        if (typeof selector == 'string') {
            r.context = context || document, r.selector = selector;
            global.selector = r.selector;
        }
        global.selected = r;
        return r;
    }

    $.prototype.init.prototype = $.prototype;

    $.fn.picShuffle = {

        setup: function (optionalConfig) {

            self = this;

            if (optionalConfig) setConfig($.extend(defaults, optionalConfig));

            this.loadSkin();

            this.initEvents();

        },

        loadSkin: function() {

            targetElement = global.selected;
            debugger

            var skinStyle = config.rootPath + config.skinNaming + config.skinId + "/" + config.skinDefaulClass;
            this.downloadCss(skinStyle, function () {
                
                targetElement.append(config.defaultDesign);
                targetElement.show();
                self.loadImages(targetElement, self.generateArray());

            });
        },

        loadImages: function (selectedElement, randomArr) {
            
            var picContainers = selectedElement.find(".picShuffleContainer div");
            if (picContainers.length == 0)
            {
                popMessage("Unable to load images, Invalid layout.");
                return;
            }
           
            $.each(picContainers, function (index, divElement) {
                
                $(divElement).addClass(index.toString());

                if (randomArr[index] == 0) {
                    $(divElement).addClass('blank');
                    $(divElement).find("img").removeAttr('src');
                }
                else
                {
                    $(divElement).removeClass('blank');
                    var img = config.rootPath + config.skinNaming + config.skinId + "/images/" + randomArr[index].toString() + config.defaultImagesExt;
                    $(divElement).find("img").attr('src', img);
                }

                $(divElement).unbind("click").click(function () {
                    
                    if ($(this).hasClass('blank')) {
                        return;
                    }

                    var targetImg = $(this).find('img').attr('src');
                    var blankDiv = picContainers.filter("div.blank");

                    blankDiv.removeClass('blank');
                    blankDiv.find('img').attr('src', targetImg);

                    $(this).addClass('blank').find('img').removeAttr('src');

                    self.validateGame();

                });

            })

        },

        validateGame: function() {
            
            var picContainers = targetElement.find(".picShuffleContainer div");
                        
            var isAllImageInPosition = true;
            $.each(picContainers, function (index, divElement) {
                
                if ($(divElement).hasClass('blank')) return;
                if ($(divElement).find("img").attr('src').lastIndexOf(index.toString()) == -1) {
                    isAllImageInPosition = false;
                }

            });

            if (isAllImageInPosition) alert('Yes! you win!!!');
        },

        initEvents: function () {
            var buttonId = this.getElemBySelector(config.shaffleButtonId);
            if (buttonId) {
                buttonId.unbind('click').click(function () {
                    self.startShuffle();
                });
            }
        },

        startShuffle: function() {
            
            var randomnums = self.randomizeArray(self.generateArray());
            self.loadImages(targetElement, randomnums);

        },

        generateArray:  function() {
        
            var ar;
            for (var i = 0, ar = []; i < config.defaultShaffleDimension; i++) {
                ar[i] = i;
            }

            return ar;

        },

        randomizeArray: function(arr) {
            
            arr.sort(function () {
                return Math.random() - 0.5;
            });

            return arr;

        },

        config: function (args) {
            setConfig($.extend(defaults, args));
            return (getConfig());
        },

        popMessage: function(message) {
            alert(message);
        },

        getElemBySelector: function(selector){

            if (selector == '') return selector;
            if (selector.indexOf(".") == -1) {
                selector = "#" + selector;
            }
        
            return $(selector);
        },

        downloadCss: function (cssPath, callbackkSucess) {
            if ($('head').find("link[href*='" + cssPath + "']").length == 0) {
                $('head').append("<link href='" + cssPath + this.getTimeStamp() + "' rel='stylesheet' type='text/css' />");
            }
            if (typeof callbackkSucess != 'undefined') callbackkSucess();
        },

        getRootDomainUrl: function () {
            return location.protocol + "//" + location.host;
        },

        removeCss: function (cssPath, callbackkSucess) {
            $('head').find("link[href*='" + cssPath + "']").remove();
            if (typeof callbackkSucess != 'undefined') callbackkSucess();
        },

        getTimeStamp: function () {
            return "?timestamp=" + new Date().getTime();
        }

    };

    function setConfig(value) {
        config = value;
    }

    function getConfig() {
        return config;
    }

})(jQuery);