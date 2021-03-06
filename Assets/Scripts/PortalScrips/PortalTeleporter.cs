using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTeleporter : MonoBehaviour {

	public GameObject player;
	public Transform receiver;

	private Transform player_transform;
	private bool setActiveNextTick = false;
	private bool playerIsOverlapping = false;

	private void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		player_transform = player.transform;
	}

	void Update()
	{
		if (setActiveNextTick)
		{
			//player.GetComponent<CharacterController>().enabled = true;
			setActiveNextTick = false;
		}


		if (playerIsOverlapping)
		{

			Vector3 portalToPlayer = player_transform.position - transform.position;
			float dotProduct = Vector3.Dot(transform.up, portalToPlayer);

			if (dotProduct < 0f)
			{

				float rotationDiff = -Quaternion.Angle(transform.rotation, receiver.rotation);
				rotationDiff += 180f;
				player_transform.Rotate(Vector3.up, rotationDiff);
				Vector3 positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalToPlayer;

				//player.GetComponent<CharacterController>().enabled = false;
				setActiveNextTick = true;
				player_transform.position = receiver.position + positionOffset;


				playerIsOverlapping = false;
			}

		}
	}

	void OnTriggerEnter(Collider other)
	{
		Debug.Log("Enter:" + other.name + ":" + other.tag);
		if (other.tag == "PlayerCapsule")
		{
			playerIsOverlapping = true;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.tag == "PlayerCapsule")
		{
			playerIsOverlapping = false;
		}
	}

}
