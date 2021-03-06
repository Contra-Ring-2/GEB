using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalPairTextureSetup : MonoBehaviour {

	public Camera cameraA;
	public Camera cameraB;

	public Material cameraMatA;
	public Material cameraMatB;

	public GameObject renderPlaneA;
	public GameObject renderPlaneB;

	void Start () {

		if (cameraA.targetTexture != null)
		{
			cameraA.targetTexture.Release();
		}
		cameraA.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
		cameraMatA.mainTexture = cameraA.targetTexture;

		if (cameraB.targetTexture != null)
		{
			cameraB.targetTexture.Release();
		}
		cameraB.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
		cameraMatB.mainTexture = cameraB.targetTexture;


		renderPlaneA.GetComponent<Renderer>().material = cameraMatB;
		renderPlaneB.GetComponent<Renderer>().material = cameraMatA	;
		}

}
