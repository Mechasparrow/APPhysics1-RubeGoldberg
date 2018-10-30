using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullyChecker : MonoBehaviour {

    public GameObject rightRope;
    public GameObject leftRope;
    public float rope_offset;
    public float margin_of_error; 

    //pulled from the left rope and right rope supplied
    private RopeSystem rightRopeSystem;
    private RopeSystem leftRopeSystem;

    private static int compareYPos(GameObject go1, GameObject go2)
    {
        Vector3 pos1 = go1.transform.position;
        Vector3 pos2 = go2.transform.position;

        if (pos1.y > pos2.y)
        {
            return -1;
        }else if (pos2.y > pos1.y)
        {
            return 1;
        }else
        {
            return 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject collided_object = other.gameObject;

        if (collided_object.CompareTag("rope"))
        {
            GameObject parent = collided_object.GetComponent<Transform>().parent.gameObject;

            if (parent.CompareTag("left_rope"))
            {
                GameObject topMostRope = leftRopeSystem.topMostRope;

                Vector3 topMostRopeCurrentPos = topMostRope.transform.position;

                GameObject otherTopMostRope = rightRopeSystem.topMostRope;

                List<GameObject> ropez = new List<GameObject>();
                
                GameObject[] ropes = GameObject.FindGameObjectsWithTag("rope");

                foreach (GameObject rope in ropes)
                {
                    if (rope.transform.parent == leftRopeSystem.transform)
                    {
                        ropez.Add(rope);
                    }
                }

                ropez.Sort(compareYPos);

                if (ropez.Count > 0)
                {

                    GameObject newRope = ropez[0];

                    leftRopeSystem.topMostRope = newRope;

                    rightRopeSystem.topMostRope = topMostRope;
                    topMostRope.transform.parent = rightRope.transform;

                    Vector3 newPos = topMostRope.transform.position;

                    newPos.x = otherTopMostRope.transform.position.x;
                    newPos.z = otherTopMostRope.transform.position.z;
                    newPos.y = otherTopMostRope.transform.position.y + 2f;

                    topMostRope.transform.position = newPos;

                }else
                {
                    leftRopeSystem.GetComponent<ConstantVel>().velocity = Vector3.zero;
                    rightRopeSystem.GetComponent<ConstantVel>().velocity = Vector3.zero;
                }



            }
            else if (parent.CompareTag("right_rope"))
            {
                GameObject topMostRope = rightRopeSystem.topMostRope;
                GameObject otherTopMostRope = leftRopeSystem.topMostRope;

                topMostRope.transform.parent = leftRope.transform;

                Vector3 newPos = topMostRope.transform.position;

                newPos.x = otherTopMostRope.transform.position.x;
                newPos.z = otherTopMostRope.transform.position.z;
                newPos.y = otherTopMostRope.transform.position.y + 1.5f;

                topMostRope.transform.position = newPos;

                leftRopeSystem.topMostRope = topMostRope;


            }

        }

    }

    // Use this for initialization
    void Start () {
        
        //retrieve the rope systems
        rightRopeSystem = rightRope.GetComponent<RopeSystem>();
        leftRopeSystem = leftRope.GetComponent<RopeSystem>();

    }
	
	// Update is called once per frame
	void Update () {
	
        

	}
}
