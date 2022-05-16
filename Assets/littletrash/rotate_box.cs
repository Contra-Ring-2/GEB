using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate_box : MonoBehaviour
{
    // Start is called before the first frame update
    private float speed = 0.5f;
    private float time = 100;
    private Vector3 rotate_axis;
    void Start()
    {
        //rotate_axis = new Vector3(transform.position.x + 0.25f, transform.position.y - 0.25f, transform.position.z);
        //Debug.Log(rotate_axis);
    }

    // Update is called once per frame
    void Update()
    {
        //if (time > 80)
        //{
          //  transform.RotateAround(rotate_axis, Vector3.back, 20 * Time.deltaTime);
        //}
    }
    public Vector3 TurnRight()
    {
        rotate_axis = new Vector3(transform.position.x - 0.25f, transform.position.y - 0.25f, transform.position.z);
        return rotate_axis;
    }
    public Vector3 TurnLeft()
    {
        rotate_axis = new Vector3(transform.position.x + 0.25f, transform.position.y - 0.25f, transform.position.z);
        return rotate_axis;
    }
    public Vector3 TurnForward() //backward
    {
        //Debug.Log(transform.position);
        rotate_axis = new Vector3(transform.position.x, transform.position.y - 0.3f, transform.position.z + 0.3f);
        //Debug.Log(rotate_axis);
        return rotate_axis;
    }
    public Vector3 TurnBack() // forward
    {
        rotate_axis = new Vector3(transform.position.x, transform.position.y - 0.25f, transform.position.z - 0.25f);
        return rotate_axis;
    }
}
