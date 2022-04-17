using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable

public class rotate_box_manager : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 point;
    private float time = 100;
    private int round = 0;
    private Vector3 vec;
    bool stay = false;
    Vector3 startPosition;
    rotate_box boxcontrol;
    void Start()
    {
        boxcontrol = GameObject.Find("box1").GetComponent<rotate_box>();
        startPosition = boxcontrol.transform.position;
        point = boxcontrol.TurnForward();
        vec = Vector3.right;
    }

    // Update is called once per frame
    void Update()
    {
        float currentAngle = Vector3.Angle(boxcontrol.transform.position - point,
                                           startPosition - point);
        if (Input.GetKey(KeyCode.F2))
        {
            Debug.Log(boxcontrol.transform.position);
            Debug.Log(point);
            Debug.Log(startPosition);
            Debug.Log(currentAngle);
            if (currentAngle > 90)
            {
                Debug.Log("helo");
                //startPosition = transform.position;
                //Debug.Log(startPosition);
                //round++;
                //if (round == 1)
                //{
                //    point = boxcontrol.TurnForward();
                //    vec = Vector3.right;
                //    Debug.Log(point);
                //    time = 80;
                //}
                //if (round == 2)
                //{
                //    Debug.Log("hi");
                //    //point = boxcontrol.TurnRight();
                //    //vec = Vector3.back;
                //    //Debug.Log(point);
                //    //time = 80;
                //}
            }
            else
            {
                transform.RotateAround(point, vec, 10 * Time.deltaTime);

            }
        }
        
    }
}
