# Virtual-Reality-paint-ball-game

Video: https://vimeo.com/204629565

A simple mobile augmented reality app in Unity, with cross-platform networking and using data from mobile sensors.

### Part 1 – Rotating a Cube 
Use **Photon** to connect the phone and PC, and use the **gyroscope** to control a cube on the PC screen.

### Part 2 – AR Paint Ball Game
Use Unity, the gyroscope input, Photon and **Vuforia**

####Steps: 

* Display an AR marker on the computer display screen (i.e. which will become the “canvas” for the paint splats) so the phone can locate its position and orientation with respect to the display screen;

* Once the AR marker has been detected, the user can then lower their phone to aim at the canvas and swipe the phone screen to launch a paint ball;

* The paint balls should be subject to gravity and physics such that when the player raises their phone vertically again they should be able to see the paint balls they launched previously flying through the air;

* When the paint balls hit the PC screen “canvas”, colored paint splatters should be created on the display screen at the point of impact (i.e. collision).
