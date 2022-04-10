using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class testTouching : MonoBehaviour
{
	private void OnCollisionEnter(Collision collision)
	{
		Debug.Log("Enter");
	}

	private void OnCollisionExit(Collision collision)
	{
		Debug.Log("Exit");
	}

	private void OnCollisionStay(Collision collision)
	{
		Debug.Log("Stay");
	}

}
