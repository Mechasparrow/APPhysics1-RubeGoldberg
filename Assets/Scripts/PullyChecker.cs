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

    private void listRopes(List<GameObject> ropes)
    {
        Debug.Log("Listing Off Ropes in Order");

        foreach (GameObject rope in ropes)
        {
            Debug.Log(rope.transform.position);
        }
    }

    private List<GameObject> getRopesForSide(string side)
    {
        GameObject[] ropes = GameObject.FindGameObjectsWithTag("rope");

        GameObject rope_system = null;

        if (side == "left")
        {
            rope_system = leftRope;
        } else if (side == "right")
        {
            rope_system = rightRope;
        }

        string rope_tag = rope_system.tag;

        List<GameObject> filtered_ropes = new List<GameObject>();

        foreach (GameObject rope in ropes)
        {
            
            if (rope.transform.parent.gameObject.CompareTag(rope_tag))
            {
                filtered_ropes.Add(rope);
            }

        }

        filtered_ropes.Sort(compareYPos);

        return filtered_ropes;

    }

    private void stopPulleySystem()
    {
        leftRope.GetComponent<ConstantVel>().velocity = Vector3.zero;
        rightRope.GetComponent<ConstantVel>().velocity = Vector3.zero;

        leftRope.GetComponent<Rigidbody>().velocity = Vector3.zero;
        rightRope.GetComponent<Rigidbody>().velocity = Vector3.zero;

        //stop the pulley

        leftRope.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        rightRope.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;


    }

    private bool shouldPulleyStop()
    {
        List<GameObject> left_filtered_ropes = getRopesForSide("left");
        List<GameObject> right_filtered_ropes = getRopesForSide("right");

        return (left_filtered_ropes.Count <= 1 || right_filtered_ropes.Count <= 1);
    }

    private void RopeToOppSide(GameObject rope, string side)
    {
        List<GameObject> filtered_ropes = getRopesForSide(side);

        GameObject rope_system = null;
        GameObject opp_rope_system = null;

        if (side == "left")
        {
            opp_rope_system = rightRope;
            rope_system = leftRope;

        }
        else if (side == "right")
        {
            opp_rope_system = leftRope;
            rope_system = rightRope;

        }

        if (filtered_ropes.Count >= 2)
        {
            //retrieve the new top most rope for the rope sys
            filtered_ropes.RemoveAt(0);
            GameObject updatedTopRope = filtered_ropes[0];
            Debug.Log("updated Top Rope is ");
            Debug.Log(updatedTopRope);

            //saving it as the new top most rope
            rope_system.GetComponent<RopeSystem>().topMostRope = updatedTopRope;

            //add the left_rope to the right rope system via transform
            rope.transform.parent = opp_rope_system.transform;

            //move the left rope to the right rope
            GameObject oldOppTopMostRope = opp_rope_system.GetComponent<RopeSystem>().topMostRope;
            Vector3 new_opp_rope_pos = rope.transform.position;
            new_opp_rope_pos = oldOppTopMostRope.transform.position;
            new_opp_rope_pos.y += 2.2f;
            rope.transform.position = new_opp_rope_pos;

            //set the left (now right) to the new top most rope for that side
            rightRopeSystem.topMostRope = rope;

            if (filtered_ropes.Count <= 1)
            {
                stopPulleySystem();
            }


        }
        else
        {
            stopPulleySystem();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject collided_object = other.gameObject;

        if (collided_object.CompareTag("rope"))
        {
            GameObject rope = collided_object;
            GameObject parent = collided_object.GetComponent<Transform>().parent.gameObject;

            if (parent.CompareTag("left_rope"))
            {
                RopeToOppSide(rope, "left");
            }else if (parent.CompareTag("right_rope"))
            {
                RopeToOppSide(rope, "right");
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
	void FixedUpdate () {

        Vector3 vel_of_left = leftRope.GetComponent<Rigidbody>().velocity;
        Vector3 vel_of_right = rightRope.GetComponent<Rigidbody>().velocity;

        if (vel_of_left.magnitude > vel_of_right.magnitude)
        {
            rightRope.GetComponent<Rigidbody>().velocity = -vel_of_left;
        }else if (vel_of_right.magnitude > vel_of_left.magnitude)
        {
            leftRope.GetComponent<Rigidbody>().velocity = -vel_of_right;
        }

        if (shouldPulleyStop())
        {
            stopPulleySystem();
        }


	}
}
