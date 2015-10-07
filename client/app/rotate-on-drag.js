pc.script.create('rotate-on-drag', function (app) {
    var RotateOnDrag = function (entity) {
        this.entity = entity;

        var mouse = new pc.Mouse(document.body);

        var x = 0;
        var speed = 0.2;

        mouse.on('mousemove', function (event) {
            if (event.buttons[pc.MOUSEBUTTON_LEFT]) {
                // rotate in the opposite direction of the drag
                x -= event.dx;

                var rotationDelta = -x * speed;

                entity.setLocalEulerAngles(0, rotationDelta, 0);
            }
        });
    };

    return RotateOnDrag;
});