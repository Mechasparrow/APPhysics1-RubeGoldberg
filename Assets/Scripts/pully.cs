using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pully : MonoBehaviour {

    private Rigidbody rb;

    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePosition;

    }

    // Update is called once per frame
    void Update () {
       }
}
