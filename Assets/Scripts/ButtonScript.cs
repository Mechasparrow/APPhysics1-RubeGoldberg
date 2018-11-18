using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour {

    //button materials
    public Material buttonPressedMat;
    public Material buttonNotPressedMat;

    //pressy part of button
    public GameObject pressyPart;

    //game object that will be affected
    public GameObject affectedObject;

    //is the button pressed boolean
    private bool button_pressed;

	// Use this for initialization
	void Start () {
        button_pressed = false;
        updateButtonMat();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //checks if button has been hit
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Coll has occurred");
        button_pressed = true;
        updateButtonMat();
        affectedObject.GetComponent<TriggeredObject>().onTrigger();
    }

    //updates the button material
    void updateButtonMat()
    {

        Renderer r = pressyPart.GetComponent<Renderer>();

        if (button_pressed)
        {
            r.material = buttonPressedMat;
        }else
        {
            r.material = buttonNotPressedMat;
        }

    }

}
