// Part of the CWVRGUI system -- github.com/chilton/CWVRGUI
//Attach this script to your World Space based Canvas GameObject.
//Also make sure you have an EventSystem in your hierarchy.

// Then drag this object onto your pointer's list of CWVRCanvases. 
// if you want to test whether or not this works, point this object at a buttton that canvas while playing. 
// Make sure the DebugX bool is true on the pointter
// Make sure the Game view is active
// Tap the letter X on your keyboard. 
// The button should be activated.
// 
// License and Restrictions: This is covered by the MIT license. 


// use some stuff
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;


// attach this stuff if you forgot
[RequireComponent(typeof(Canvas))]
[RequireComponent(typeof(GraphicRaycaster))]


public class CWVRCanvas : MonoBehaviour
{
    // 
    GraphicRaycaster raycaster;
    PointerEventData pointerEventData;
    EventSystem eventSystem;
    Canvas canvas;
    public Camera VRCamera; // You CAN set this ahead of time, if it helps you debug something. Otherwise, it will be set by the pointer. 
    // the ability to set this by the pointer becomes important later if you want multiple pointers to be able to swap duties and interact
    // with this GUI. Note that two pointers can't interact with a canvas at one time. That's on Unity's end. 

    GameObject lastObject; // We will want to know what we were just looking at, so we can know whether or not to tell it bye when we leave.

    // Good ole start. Here we're going to grab our GraphicsRaycaster and our Event System and tuck those away for later.
    void Start()
    {
        raycaster = GetComponent<GraphicRaycaster>();
        eventSystem = GetComponent<EventSystem>();
    }

    // And in our Update loop, we'll check to see if the camera is there, and if so, we'll track its location. 
    void Update()
    {
        if (VRCamera==null) {
            return;
        }

        TrackPointer();

    }

    // This is called by the pointer to set this canvas' camera to itself. We'll also set the canvas' camera to this.
    // The camera does NOT need to be enabled, but it does need to exist. 
    // I haven't tried it with an inactive game object, but why would you do that? 
    public void SetCamera(Camera newCamera)
    {
        VRCamera = newCamera;
        canvas = GetComponent<Canvas>();
        canvas.worldCamera = newCamera;
    }

    // This is decidedly indirect. We get our pointer event data from the event system, then 
    // set our position to the center point of the camera, 
    // then create a list to store hits in
    // and then we cast our ray and do our hit test. 

    public void TrackPointer()
    {
        pointerEventData = new PointerEventData(eventSystem);
        pointerEventData.position = VRCamera.ViewportToScreenPoint(new Vector2(0.5f, 0.5f));

        List<RaycastResult> results = new List<RaycastResult>();

        raycaster.Raycast(pointerEventData, results);

        if (results.Count == 0)
        {
            if (lastObject != null)
            {
                EventSystem.current.SetSelectedGameObject(null);
                lastObject = null;
            }
        }
        else
        {
            // in my tests, results[0] was always the closest item. In the case of buttons, it would hit the text sometimes.
            // so we send that Select message up the flag pole, just in case that's what happened.
            if (lastObject != results[0].gameObject)
            {
                EventSystem.current.SetSelectedGameObject(null);


                results[0].gameObject.SendMessageUpwards("Select", SendMessageOptions.DontRequireReceiver);
                lastObject = results[0].gameObject;
            }
        }
    }

    // This is actually sent from the CWVRPointer, but you can send it from wherever. 
    // when it fires, it checks the position of the pointer against stuff on this canvas,
    // then sends an activation event to the button as if it was clicked with a mouse button. 
    public void Click()
    {
        if (lastObject!=null) {
            Button theButton = lastObject.GetComponent<Button>();
            if (theButton == null)
            {
                if (lastObject.transform.parent!=null) {
                    theButton = lastObject.transform.parent.GetComponent<Button>();
                }
            }

            if (theButton != null)
            {
                pointerEventData.button = PointerEventData.InputButton.Left;
                theButton.OnSubmit(pointerEventData);
            }


        }
        }

}