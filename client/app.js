requirejs.config({
    baseUrl: 'lib',

    shim: {
        'facebook' : {
            exports: 'FB'
        }
    },

    paths: {
        app: '../app',
        playcanvas: 'playcanvas-latest',
        facebook: '//connect.facebook.net/en_US/sdk'
    }
});

require(['app/main']);