"use strict";

define(['facebook'], function () {

    window.fbAsyncInit = function () {
        FB.init({
            appId: '1639046766344117',
            status: true, // Faster FB login?
            xfbml: true,
            frictionlessRequests: true,
            version: 'v2.4'
        });

        function onLogin(response) {
            if (response.status == 'connected') {
                FB.api('/me?fields=id,name,picture', function (data) {
                    var welcomeBlock = document.getElementById('fb-welcome');
                    welcomeBlock.innerHTML = 'FacebookID: ' + data.id + '. Hello, ' + data.name + '!'
                    + '<img src="' + data.picture.data.url + '"></img>';
                });
            }
        }

        FB.getLoginStatus(function (response) {
            // Check login status on load, and if the user is
            // already logged in, go directly to the welcome message.
            if (response.status == 'connected') {
                onLogin(response);
            } else {
                // Otherwise, show Login dialog first.
                FB.login(function (response) {
                    onLogin(response);
                }, {scope: 'user_friends, email'});
            }
        });
    };

});