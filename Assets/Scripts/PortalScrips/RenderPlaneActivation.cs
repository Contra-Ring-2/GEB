using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderPlaneActivation: MonoBehaviour
{
	void OnTriggerEnter(Collider other)
	{

		if (other.tag == "RenderPlane")
		{
			other.GetComponent<MeshRenderer>().enabled = true;

			if(other.GetComponent<MeshRenderer>() == null){
				Debug.Log("Nothing");
			}else{
				Debug.Log("RenderPlane : activate");
			}
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.tag == "RenderPlane")
		{
			other.GetComponent<MeshRenderer>().enabled = false;

			if (other.GetComponent<MeshRenderer>() == null)
			{
				Debug.Log("Nothing");
			}
			else
			{
				Debug.Log("RenderPlane : deactivate");
			}

		}
	}

}
