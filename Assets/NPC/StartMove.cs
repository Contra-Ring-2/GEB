using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMove : MonoBehaviour
{
    public GameObject NPC;
    private void OnTriggerEnter(Collider other)
    {
        NPC.GetComponent<NPCmove>().canwalk = true;
        Debug.Log("StartMove");
    }
}
