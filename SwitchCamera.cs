using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour {

    public GameObject holderOfTheCamera;   // the camera must always have an object parent it to
    public Transform[] cameraPosition;
    public Camera mainCamera;
    private int IndexCamera = 0;  // ESSENTIAL FOR THE RIGHT INDEX O
    bool camerSwitched = false;

    
    // Use this for initialization
    void Start () {
        holderOfTheCamera.transform.position = cameraPosition[IndexCamera].position;
        holderOfTheCamera.transform.parent = cameraPosition[IndexCamera];
    }
	
	// Update is called once per frame
	void Update () {                                                 /// Check Each Of The cameras and enables the right one  losing too many calculations here
        
      
        if(IndexCamera < cameraPosition.Length)
        {
            if (camerSwitched)
            {
                holderOfTheCamera.transform.position = cameraPosition[IndexCamera].position;
                holderOfTheCamera.transform.parent = cameraPosition[IndexCamera];
                holderOfTheCamera.transform.rotation = cameraPosition[IndexCamera].rotation;
                GvrCardboardHelpers.Recenter();
                camerSwitched = false;
            }
            else
            {
                return;
            }
        }
        else
        {
            return;
        }
       
    }


    public void ChangeCamera()     // trigger the logic for the switch
    {
        IndexCamera++;
        camerSwitched = true;
    }

}

