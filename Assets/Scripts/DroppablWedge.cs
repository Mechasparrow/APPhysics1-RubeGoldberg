using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppablWedge : MonoBehaviour, TriggeredObject {

    private bool dropped = false;

    

    // Use this for initialization
    void Start () {
        dropped = false;
        updateDropped();
    }

    public void onTrigger()
    {
        dropped = true;
        updateDropped();
    }

    private void updateDropped()
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        if (dropped)
        {
            rb.isKinematic = false;
        }else
        {
            rb.isKinematic = true;
        }
    }

}
