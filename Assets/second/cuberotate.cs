using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cuberotate : MonoBehaviour
{
    private Vector3 rotate_axis;
    private void Start()
    {
        
    }
    private void Update()
    {
        transform.RotateAround(new Vector3(0,0,0), Vector3.forward, 40 * Time.deltaTime);
    }
}
