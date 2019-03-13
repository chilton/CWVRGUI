// Part of the CWVRGUI system -- github.com/chilton/CWVRGUI
// Attach this to some object you want to use as your controller. 
// A camera will be automatically added. Disable the camera component, but leave it on the object.
// Attach the CWVRCanvas object to your World Space based canvas. 
// Then drag that object onto this one's list of canvases.
// if you want to test whether or not this works, point this object at a buttton that canvas while playing. 
// Make sure the DebugX bool is true
// Make sure the Game view is active
// Tap the letter X on your keyboard. 
// The button should be activated.
// 
// License and Restrictions: This is covered by the MIT license. 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]

public class CWVRPointer : MonoBehaviour
{
    public Camera myCamera;
    public CWVRCanvas[] canvases;
    public bool autoPowerUp = true;
    public bool DebugX = false;
    // Start is called before the first frame update
    void Start()
    {
        //myCamera = GetComponent<Camera>(); // This works if you have your camera component enabled. 
        myCamera.enabled = false;
        if (autoPowerUp==true) {
            PowerUp();
        }
    }

    // Update is called once per frame
    void PowerUp()
    {
        // find all the CWVRCanvases and send them our pointer. 
        for (int i = 0; i < canvases.Length; i++)
        {
            canvases[i].SetCamera(myCamera);
        }
    }

    public void ClickCanvas()
    {
        for (int i = 0; i < canvases.Length; i++)
        {
            canvases[i].Click();
        }
    }

    private void Update()
    {
        if (DebugX == true)
        {
             if (Input.GetKeyUp(KeyCode.X))
            {
                ClickCanvas();
            }
        }

    }
}
