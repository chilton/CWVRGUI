# CWVRGUI
Simple toolkit for interacting with the Unity GUI system via a hand controller in VR. 

I needed a way to make Unity's GUI system work from a hand controller in VR, for Glycon and a few other things. I figured it out, but it was not easy! So I've simplified and documented my code, and open sourced it. Hopefully this will help other Unity developers, too. This should work across all VR systems. 

Basically, this means you can build your GUI in uGUI, and test it with a mouse. Then when you're ready to go VR, just drop these two files in and presto-blammo. 

Well, it's a little more complicated. There are notes in the code that explain how it works. 

Add the CWVRCanvas on a Canvas. 
Add the CWVRPointer on another object, that you'll use as your pointer. 
Drag the Canvas object onto the Pointer object in the Canvases section in the inspector. 

... that's about it! 

After that, your Unity GUI should 'just work' with either a mouse or a VR controller. You have to bring your own line renderer though, if you want a laser pointer.

chiltonwebb at gmail dot com if you need help making it work!
