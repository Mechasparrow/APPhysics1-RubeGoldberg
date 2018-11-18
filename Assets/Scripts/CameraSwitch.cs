using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour, TriggeredObject {

    public Camera primaryCamera;
    public Camera secondaryCamera;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void switchCameras()
    {
        primaryCamera.enabled = false;
        secondaryCamera.enabled = true;
    }

    public void onTrigger()
    {
        switchCameras();
    }

}
