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
        rotate_axis = new Vector3(transform.position.x + 0.25f, transform.position.y - 0.25f, transform.position.z);
        Debug.Log(rotate_axis);
    }

    // Update is called once per frame
    void Update()
    {
        if (time > 80)
        {
            transform.RotateAround(rotate_axis, Vector3.back, 20 * Time.deltaTime);
        }
    }
}
