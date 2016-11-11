points = 0;
port = argument0;
if gamepad_is_connected(port){ 
    gamepad_set_axis_deadzone(port, 0.3);
}
