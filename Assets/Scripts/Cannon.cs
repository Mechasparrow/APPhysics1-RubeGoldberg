using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour, TriggeredObject {

    public GameObject forcePoint;
    public GameObject cannonBall;

    public float power; 

    private bool launched;
    private bool launching;

    private float launchTimer;
    private float launchDuration;

    private Vector3 launchForce;


    // Use this for initialization
    void Start () {
        launched = false;
        launching = false;

        launchTimer = 0.0f;
        launchDuration = 0.25f;

	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if (launching && launchTimer < launchDuration)
        {
            launchTimer += Time.deltaTime;
            cannonBall.GetComponent<Rigidbody>().AddForce(launchForce);
        }

        if (launchTimer > launchDuration)
        {
            launching = false;
            launched = true;
        }

	}

    public void onTrigger()
    {
        launchCannon();
    }

    private void launchCannon()
    {

        if (launched != true && launching == false)
        {
            //get transforms
            Transform cannonBallTransform = cannonBall.GetComponent<Transform>();
            Transform forcePointTranform = forcePoint.GetComponent<Transform>();

            Vector3 forceVector = forcePointTranform.position - cannonBallTransform.position;

            launchForce = forceVector * power;
            launching = true;
        }else
        {
            // Do nothing
        }



    }


}
