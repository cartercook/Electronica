if (global.pause) {
    pauseCounter++;
    if (pauseCounter >= 120) { //120 frames == 2 seconds
        obj_p1.x = 64;
        obj_p1.y = 320;        
        obj_p2.x = 896;
        obj_p2.y = 320;
        
        pauseCounter = 0;
        global.pause = false;
    }
} else {
    frameCount++;
    if (frameCount > framesPerBeat) {
        audio_play_sound(snd_beat, 0, 0);
        isBeat = true;
        frameCount = 0;
    }
}
