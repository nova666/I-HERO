using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SwitchCameraBossBattle : MonoBehaviour {


    public GameObject holderOfTheCamera;   // the camera must always have an object parent it to
    public List<GameObject> cameraPosition = new List<GameObject>();
    private int IndexCamera = 0;  // ESSENTIAL FOR THE RIGHT INDEX O



                                  // Use this for initialization
    void Start () {
        OrganizeCameras();
        holderOfTheCamera.transform.position = cameraPosition[4].transform.position;
        holderOfTheCamera.transform.parent = cameraPosition[4].transform;

    }

    private void OrganizeCameras()
    {
        foreach (GameObject cameraPos in GameObject.FindGameObjectsWithTag("CameraPosition"))
        {
            cameraPosition.Add(cameraPos);
        }
        var newList = cameraPosition.OrderBy(x => x.name).ToList();
        cameraPosition = (List<GameObject>)newList;
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void ChangeCamera(string name)
    {
        if(name == "TriggerCamera1")       // Camera 1
        {
            holderOfTheCamera.transform.position = cameraPosition[0].transform.position;
            holderOfTheCamera.transform.parent = cameraPosition[0].transform;
        }
        else if (name == "TriggerCamera2")   // Camera 2
        {
            holderOfTheCamera.transform.position = cameraPosition[1].transform.position;
            holderOfTheCamera.transform.parent = cameraPosition[1].transform;
        }
        else if (name == "TriggerCamera3")   // Camera 3
        {
            holderOfTheCamera.transform.position = cameraPosition[2].transform.position;
            holderOfTheCamera.transform.parent = cameraPosition[2].transform;
        }
        else if (name == "TriggerCamera4")   // Camera 4
        {
            holderOfTheCamera.transform.position = cameraPosition[3].transform.position;
            holderOfTheCamera.transform.parent = cameraPosition[3].transform;
        }
        else                                // Camera 5
        {
            holderOfTheCamera.transform.position = cameraPosition[4].transform.position;
            holderOfTheCamera.transform.parent = cameraPosition[4].transform;
        }
    }
}
