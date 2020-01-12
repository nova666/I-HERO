using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRotationController : MonoBehaviour {


    Camera camera;
    
    // Use this for initialization
    void Start () {

        camera = Camera.main;

    }
	
	// Update is called once per frame
	void Update () {

        Vector3 v = camera.transform.position - transform.position;
        v.x = v.z = 0.0f;
        transform.LookAt(camera.transform.position - v);
        transform.Rotate(0, 180, 0);

    }
}
