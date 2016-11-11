if (!global.pause) {
    if (argument0 == "arrows") {
        var up = vk_up;
        var down = vk_down;
        var left = vk_left;
        var right = vk_right;
    } else if (argument0 == "WASD") {
        var up = ord('W');
        var down = ord('S');
        var left = ord('A');
        var right = ord('D');
    }
    
    var haxis = gamepad_axis_value(port, gp_axislh);
    if (keyboard_check(right)) {
        haxis = 1;
    } else if (keyboard_check(left)) {
        haxis = -1;
    }
    
    var vaxis = gamepad_axis_value(port, gp_axislv);
    if (keyboard_check(down)) {
        vaxis = 1;
    } else if (keyboard_check(up)) {
        vaxis = -1;
    }
    
    if (obj_timer.isBeat) {
        direction = point_direction(0, 0, haxis, vaxis);
        speed = point_distance(0 ,0, haxis, vaxis) * 150;
    }
}
