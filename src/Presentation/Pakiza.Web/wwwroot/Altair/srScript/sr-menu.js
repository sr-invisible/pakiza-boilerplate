"use strict";
$(function () {
    var currentUrl = window.location.pathname;
    $('.sr-menu').each(function () {
        var href = $(this).attr('href');
        if (currentUrl == href) {
            $(this).parent().addClass('act_item');
            $(this).parents('.submenu_trigger').addClass('current_section');
            $(this).parents('.submenu_trigger').find('ul').css('display', 'block');
        }
    });
});