using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantVel : MonoBehaviour {

    //The Constant Velocity
    public Vector3 velocity;

    //The rigidbody
    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

        rb.velocity = velocity * 10;

	}
}
