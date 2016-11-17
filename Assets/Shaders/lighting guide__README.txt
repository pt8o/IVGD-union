-----------------------------------
---LIGHTING RESOURCES/GUIDELINES---
-----------------------------------


In order to shade an object, make a copy of the Spritelamp.shader and (for organizational purposes) rename it as the object you're going to shade. 
That shader requires:
   - A diffuse map (ie: the original sprite/spritesheet) 
   - A normal map (I made mine using SpriteLamp, with 4 different light maps (top, bottom, left, right) for the example)
   - A depth map (also made with SpriteLamp)

 Then make a material for the shaded sprite, with the custom shader. Make sure all the maps are sprites, not materials!


Further reading: 

- SpriteLamp's (incomplete) guide to integrating with Unity 
  http://www.snakehillgames.com/using-sprite-lamp-with-engines/

- Creating your own Spritelamp shader
  http://indreams-studios.com/post/writing-a-spritelamp-shader-in-unity/  
    - The corrected/updated shader code, because as useful as it is, the above guide has errors (but is a handy starting point)
      https://docs.google.com/document/d/1ZbQd8k0VlAmCWSxLMjulzK6Xyxtor7aQszTWoHzANpM/edit


Alternate/ DIY method:
 - http://robotloveskitty.tumblr.com/post/33164532086/legend-of-dungeon-dynamic-lighting-on-sprites