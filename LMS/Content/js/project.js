/**
 * Project-specific JavaScripts
 *
 * @project POMS_MPA
 * @since  1.0.0
 */

jQuery(document).ready(function ($) {

    /**
    * Left Navigation Control
    * ---------------------------
    */
    var submenu = $('ul.submenu');

    // Hide all the submenus first
    submenu.hide();

    // Parent menu items
    $('ul.menu > li > a').click(function (e) {
        e.preventDefault();
        var this_parent = $(this),
            nearby_submenu = this_parent.next('ul.submenu');

        $('ul.menu > li').removeClass('active');
        this_parent.parent('li').addClass('active');
        submenu.slideUp();
        if (nearby_submenu.css('display') == 'none')
            nearby_submenu.slideDown();
        else
            nearby_submenu.slideUp();
    });

    $('ul.submenu > li > a').click(function () {
        var this_parent = $(this).parent('li');

        $('ul.submenu > li').removeClass('active');
        this_parent.addClass('active');
    });


    /**
    * jQuery ScrollBar
    * Plugin: jQuery SlimScroll
    * ---------------------------
    */
//    $('#main-menu').slimScroll({
//        height: 'auto',
//        position: 'left'
//    });


    $('#user-menu').live('click', function () {
        var user_dropdown = $('.user-dropdown-menu');
        if (user_dropdown.is(':visible')) {
            user_dropdown.hide();
        } else {
            user_dropdown.show();
        }
    });

});


function toggle_fullscreen() {
    var fullscreenEnabled = document.fullscreenEnabled || document.mozFullScreenEnabled || document.webkitFullscreenEnabled;
    if (fullscreenEnabled) {
        if (!document.fullscreenElement && !document.mozFullScreenElement && !document.webkitFullscreenElement && !document.msFullscreenElement) {
            launchIntoFullscreen(document.documentElement);
        } else {
            exitFullscreen();
        }
    }
}

// Thanks to http://davidwalsh.name/fullscreen
function launchIntoFullscreen(element) {
    if (element.requestFullscreen) {
        element.requestFullscreen();
    } else if (element.mozRequestFullScreen) {
        element.mozRequestFullScreen();
    } else if (element.webkitRequestFullscreen) {
        element.webkitRequestFullscreen();
    } else if (element.msRequestFullscreen) {
        element.msRequestFullscreen();
    }
}

function exitFullscreen() {
    if (document.exitFullscreen) {
        document.exitFullscreen();
    } else if (document.mozCancelFullScreen) {
        document.mozCancelFullScreen();
    } else if (document.webkitExitFullscreen) {
        document.webkitExitFullscreen();
    }
} 
