'use strict';

define(['playcanvas'], function () {
    require(['app/fb-manager']);
    require(['app/rotate-on-drag']);

    var app = createApp();
    initGround();
    initCameraNode();
    initLight();
    initTestTowers();

    app.on('update', function (deltaTime) {
        //cube.rotate(10 * deltaTime, 20 * deltaTime, 30 * deltaTime);
    });

    // ----- Methods -----

    function createApp() {
        var canvas = document.getElementById('application-canvas');
        var app = new pc.Application(canvas);
        app.start();
        app.setCanvasFillMode(pc.FILLMODE_KEEP_ASPECT);
        app.setCanvasResolution(pc.RESOLUTION_AUTO);
        return app;
    }

    function initLight() {
        var light = new pc.Entity();
        light.addComponent('light', {
            castShadows: true,
            shadowResolution: 1024
        });
        light.setEulerAngles(45, -45, 0);
        app.root.addChild(light);
    }

    function initCameraNode() {
        var cameraRoot = new pc.Entity();
        cameraRoot.addComponent('script', {
            scripts: [{
                url: 'app/rotate-on-drag.js'
            }]
        });

        var camera = new pc.Entity();
        camera.addComponent('camera', {
            clearColor: new pc.Color(0.1, 0.1, 0.1)
        });
        cameraRoot.addChild(camera);
        camera.setPosition(0, 25, 15);
        camera.setEulerAngles(-60, 0, 0);

        app.root.addChild(cameraRoot);
    }

    function initGround() {
        var ground = new pc.Entity();
        ground.addComponent('model', {
            type: 'plane'
        });
        ground.setLocalScale(20, 1, 20);
        app.root.addChild(ground);
    }

    function initTestTowers() {
        var cube = new pc.Entity();
        cube.addComponent('model', {
            type: 'box',
            castShadows: true,
            receiveShadows: false
        });
        cube.setLocalScale(1,2,1);
        cube.translateLocal(0,1,0);
        app.root.addChild(cube);
    }
});