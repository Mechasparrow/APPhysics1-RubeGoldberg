using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideBlockTrigger : MonoBehaviour, TriggeredObject {

    public void onTrigger()
    {
        gameObject.SetActive(false);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
