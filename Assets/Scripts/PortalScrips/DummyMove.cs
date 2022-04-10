using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyMove : MonoBehaviour
{
	public float speed = 1;
    void Update()
    {
		if (Input.GetKey(KeyCode.UpArrow)){
			transform.position += new Vector3(0, 0, speed * Time.deltaTime);
		}
		if (Input.GetKey(KeyCode.DownArrow))
		{
			transform.position += new Vector3(0, 0, -1 * speed * Time.deltaTime);
		}

	}
}
