using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetNextPath : MonoBehaviour
{
    private string parapath;
    private string buttonpath;
    private char ch;
    private char par;
    private GameObject dialogue;
    private GameObject storyball;
    public GameObject NPC;
    void Start()
    {
        string name_ = gameObject.name;
        ch = name_[gameObject.name.Length - 1];
        par = name_[gameObject.name.Length - 3];
        parapath = Application.dataPath + "/Dialogue/Dialogue" + par + "_" + ch + ".txt";
        buttonpath = Application.dataPath + "/Dialogue/Button" + par + "_" + ch + ".txt";
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other == NPC.GetComponent<Collider>())
        {
            //Debug.Log(other);
            storyball.SetActive(true);
            dialogue = GameObject.FindWithTag("dialogue");
            storyball = GameObject.FindWithTag("storyUI");
            dialogue dia = dialogue.GetComponent<dialogue>();
            dia.SetParaPath(parapath);
            dia.SetButtonPath(buttonpath);
            dia.SetAndStart();
        }
        
    }
}
