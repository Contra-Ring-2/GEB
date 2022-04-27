using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetNextPath : MonoBehaviour
{
    private string parapath;
    private string buttonpath;
    public GameObject dialogue;
    public GameObject storyball;
    public GameObject NPC;
    void Start()
    {
        parapath = Application.dataPath + "/changescene/plot.txt"; // change
        buttonpath = Application.dataPath + "/changescene/buttonPlot.txt";
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other == NPC.GetComponent<Collider>())
        {
            Debug.Log(other);
            storyball.SetActive(true);
            dialogue dia = dialogue.GetComponent<dialogue>();
            dia.SetParaPath(parapath);
            dia.SetButtonPath(buttonpath);
            dia.SetAndStart();
        }
        
    }
}
