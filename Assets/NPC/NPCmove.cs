using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//stop at step point

public class NPCmove : MonoBehaviour
{
    // put the points from unity interface
    public Transform[] wayPointList;

    public int currentWayPoint = 0;
    Transform targetWayPoint;

    public float speed = 4f;
    //public GameObject dialogue;
    //public GameObject storyball;
    public bool canwalk = false;

    // Use this for initialization
    void Start()
    {
        canwalk = false;
    }

    // Update is called once per frame
    void Update()
    {
        // check if we have somewere to walk
        if (currentWayPoint < this.wayPointList.Length && canwalk)
        {
            if (targetWayPoint == null)
                targetWayPoint = wayPointList[currentWayPoint];
            walk();
        }
        else
        {
            stay();
        }
    }

    void stay() 
    {
        //this.GetComponent<AnimatorControl_JelloMan>().ChangeStae("idle");
    }

    void walk()
    {
        // rotate towards the target
        transform.forward = Vector3.RotateTowards(transform.forward, targetWayPoint.position - transform.position, speed * Time.deltaTime, 0.0f);

        // move towards the target
        transform.position = Vector3.MoveTowards(transform.position, targetWayPoint.position, speed * Time.deltaTime);

        //this.GetComponent<AnimatorControl_JelloMan>().ChangeStae("walk");

        if (transform.position == targetWayPoint.position)
        {
            currentWayPoint++;
            if (currentWayPoint >= this.wayPointList.Length)
            {
                //out of range
                return;
            }
            targetWayPoint = wayPointList[currentWayPoint];
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "step")
        {
            //stop
            Debug.Log("step");
            canwalk = false;
        }
        //Debug.Log("collide : "+ dialogue.GetComponent<dialogue>());
        //storyball.SetActive(true);
        //dialogue.GetComponent<dialogue>().StartDialogue();
        //Debug.Log("collide");
        if(other.gameObject.tag == "PlayerCapsule"){
            Debug.Log("playercapsule");
            canwalk = true;
        }
    }

}
