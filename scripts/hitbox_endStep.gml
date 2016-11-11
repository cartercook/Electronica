if (obj_timer.isBeat) {
    var haxis = gamepad_axis_value(player.port, gp_axisrh);
    var vaxis = gamepad_axis_value(player.port, gp_axisrv);
    x = player.x + (haxis * jumpspeed);
    y = player.y + (vaxis * jumpspeed);
}
