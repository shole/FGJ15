#Tentaquarium
This is the primary repository for the Global/Finnish Gamejam 2015 entry
<br/>
http://youtu.be/zjk1dtibbds
<br/>
http://globalgamejam.org/2015/games/tentaquarium
<br/>
<br/>
Binary builds for desktop play can be downloaded from my personal website:
<br/>
http://teatimeresearch.com/

#Gameplay
* After the server is running, players download and launch their mobile controller apps, which spawn an octopus.
* One player controls one tentacle of the octopus, indicated with with player number matching their app.
* The player **moves the tentacle** around with the directional pad. The direction of movement is relative to the angle of the player number.
* The player can **grab** by **holding down** the tentacle button and **pull** by holding down grab and using the directional pad.
* The player can also **punch** by doubletapping the directional pad while not grabbing.
* The objective of the game is to stay on top and have fun punching other players off the top. There is no win or lose state.

#Requirements
* Unity 4.x Pro - Pro is required for the used render-to-texture and mobile export
* Android SDK - if you wish to export the android controller
* A planetarium full-dome projector & access to it's control software to produce distortion maps - the game can be played on a monitor, but where's the fun in that?

#Building the mobile controller
Open mobile_controller_client.unity.
Hit build, pick Android and build it.
It should also work on other platforms but has not been tested.
No promises

#Building the gameplay / projection server
Open server.unity.
Create and apply the target distortion maps to the front & back distortion surface materials ( **Assets\ProjectorUV\DomeFront** & **DomeBack** ).
See instructions for capturing UV maps below.
Change the server view settings, hit build.

##Changing server views

###To view the server in side-by-side projector warped layout
* enable **DomeRendering / Mesh creating warped fisheye**
* enable **DomeRendering / Mesh creating fisheye / Camera: fisheye**
* disable **DomeRendering / Mesh creating fisheye / Camera: fisheye preview**

###To view the server in full bottom-up view (better for development)
* disable **DomeRendering / Mesh creating warped fisheye**
* disable **DomeRendering / Mesh creating fisheye / Camera: fisheye**
* enable **DomeRendering / Mesh creating fisheye / Camera: fisheye preview**

#8bit resolution UV distortion map creation
Procedure for this is same as the Base UV map step in 16bit instructions below.
The texture for this does not need to be marked as **advanced**, but **format** has got to be set to **Truecolor** rather than **compressed**.
Using 16bit resolution is recommended for accuracy.
See comparison images at **/16bit UV map/comparisons/**.

#16bit resolution UV distortion map creation
Disable all color filtering (interpolation) from the software which does the projection warping if possible.
A UVmap is a pixel-to-pixel mapping so it should be as accurate as possible.
Any aliasing artefacts you might see on the resulting UV maps gets smoothed out automagically in runtime texture interpolation.
It makes sense to pick a UVmap resolution slightly bigger, but closely matching the final presentation screen resolution.

###Rendering the Base UV map:
It's safest to use one of the upscaled UVmap versions in the prescaled/ folder. (**UVmap_*.png**)
Take the base UV map and save it's warped version.
The save resolution should be a power of 2 square, such as 2048x2048.
(This bitmap is also a standard 8x8bit UVmap.)

###Rendering the Detail UV map:
For optimal quality, take **UVmap_detail_1of128.png**, REPEAT it 128x128 times across the area.
This would give us the theoretical resolution of 65536x65536 to work with.
Do the warp and save the image as a power of 2 square matching the Base UV resolution.
If the texture repeat cannot be done or other issues are encountered, pick one of the pre-scaled detail-textures. (**UVmap_detail_*.png**)
Pick as high resolution one as possible, and perform the same procedure as with the base UV map and save to a power of 2 resolution.

If using the textures separately, the base UV can be smaller with no quality loss.
If using combined single 32bit texture, the detail texture resolution should match the base UV map's.
Using the combined 32bit texture is probably the preferred way to go since it should save a lot of texture memory.

To combine the UV maps, put them in the "**combine UVs to single 32bit image**" folder.<br>
Open a commandline and run "**combineUVs.bat UVmap.png UVmap_detail.png**"

###When importing to the engine, For best quality set these texture settings;
Texture Type: Advanced
Generate Mip Maps: off
Format: RGBA32 or Truecolor        <- this one is especially important if you forget to touch the others

###Applying correct shader
Apply shader to **DomeFront** and **DomeBack** materials under **Assets\ProjectorUV**
* for 8bit standard UVmap: **dist.shader** or **dist_shadowmask.shader**
* for 16bit separated UVmaps: **distortion_16bit.shader**
* for 16bit combined UVmap: **distortion_16bit_4channel.shader**

#Server commandline parameters
Example batch files are provided at project root.
###-nomusic
Disable music, leaving only underwater ambient and player sounds
###-announcement="message"
Display a message at rim of the aquarium.
This is especially useful for sharing the URL for the mobile client.

#Credits
The full Gamejam team:
* Paavo Happonen
* Juho Hartikainen
* Mikko Immonen
* Jukka Lankinen
* Ilja Levonen
* Janne Räsänen
<br/>
<br/>
<br/>
<br/>
<br/>
For any questions or comments, you can reach me at pahappo@gmail.com
