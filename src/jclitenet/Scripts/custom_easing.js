/// <reference path="jquery-ui-1.8.11.min.js" />
/// <reference path="jquery-1.5.1-vsdoc.js" />

var prev = '';
$(function () {
    var current = 0;
    var iterate = function () {
        var i = parseInt(current + 1);
        var lis = $('#radio_menu').children('li').size();
        if (i > lis) i = 1;
        display($('#radio_menu li:nth-child(' + i + ')'));
    }

    prev = 1;
    display($('#radio_menu li:first'));

    var slidetime = setInterval(iterate, 3000);
    $('#radio_menu li').click(function (e) {
        clearTimeout(slidetime);

        var linktext = $(this).find('span:first').text();
        if (prev != linktext) {
            display($(this));

            $(this).parent().find('li:nth-child(' + prev + ')').attr('class', '');
            $(this).attr('class', 'selected');
            prev = linktext;
        }
        e.preventDefault();
    });

    function display(elem) {
        var $this = elem;
        var repeat = false;
        if (current == parseInt($this.index() + 1))
            repeat = true;

        if (!repeat)
            $this.parent().find('li:nth-child(' + current + ')').stop(true, true).animate('', 0, function () {
                $(this).animate({ 'opacity': '0.7' }, 700);
            });

        current = parseInt($this.index() + 1);

        var elemsel = $this;
        elemsel.stop(true, true).animate({'opacity': '1.0' }, 300);

        var elem = $('span', $this);
        var info_elem = elem.next();
        $('#rotator1 .heading').animate({ 'left': '-220px' }, 500, 'easeOutCirc', function () {
            $('h1', $(this)).html(info_elem.find('.info_heading').html());
            $(this).animate({ 'left': '0px' }, 500, 'easeInOutQuad');
        });
        $('#rotator1 .topinfo').animate({ 'top': '-270px' }, 500, 'easeOutCirc', function () {
            $('p', $(this)).html(info_elem.find('.info_description').html());
            $(this).animate({ 'top': '0px' }, 400, 'easeInOutQuad');
        })
        $('#rotator1').prepend(
$('<img/>', {
    style: 'opacity:0',
    className: 'bg'
}).load(
function () {
    $(this).animate({ 'opacity': '1' }, 500)
    $(this).next().animate({ 'opacity': '0' }, 500, function () {
        $(this).remove();
    });
}
).attr('src', info_elem.find('.info_image').html()).width('100%').height('200px')
);
    }
});