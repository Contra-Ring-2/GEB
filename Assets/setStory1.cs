using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setStory1 : MonoBehaviour
{
    private string parapath;
    private string buttonpath;
    private GameObject dialogue;
    private GameObject storyball;
    void Start()
    {
        parapath = Application.dataPath + "/Dialogue/Dialogue0_1.txt"; // change
        buttonpath = Application.dataPath + "/Dialogue/Button0_1.txt";
        dialogue = GameObject.FindWithTag("dialogue");
        storyball = GameObject.FindWithTag("storyUI");
        dialogue dia = dialogue.GetComponent<dialogue>();
        dia.SetParaPath(parapath);
        dia.SetButtonPath(buttonpath);
        dia.SetAndStart();
    }

}
