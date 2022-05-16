using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCUltimate : MonoBehaviour
{
    public GameObject[] stepLists;
    public int curActiveIndex;

    private string parapath;
    private string buttonpath;
    private char ch;
    private char par;
    private GameObject dialogue;
    private GameObject storyball;

    public int chapter;


    // Start is called before the first frame update
    void Start()
    {
        curActiveIndex = 1;
        storyball = GameObject.FindWithTag("storyball");
        dialogue = GameObject.FindWithTag("dialogue");
        
        parapath = Application.dataPath + "/Dialogue/Dialogue" + chapter.ToString() + "_" + curActiveIndex.ToString() + ".txt";
        buttonpath = Application.dataPath + "/Dialogue/Button" + chapter.ToString() + "_" + curActiveIndex.ToString() + ".txt";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            ShowNextNPC();
        }
    }

    void ShowNextNPC()
    {
        if (curActiveIndex <= stepLists.Length)
        {
            /*
            curActiveIndex++;
            stepLists[curActiveIndex].SetActive(true);
            stepLists[curActiveIndex].GetComponent<BoxCollider>().enabled = true;
            */
            storyball.GetComponent<MeshRenderer>().enabled = true;

            dialogue dia = dialogue.GetComponent<dialogue>();
            //dia.NPC = NPC;
            dia.SetParaPath(parapath);
            dia.SetButtonPath(buttonpath);
            dia.SetAndStart();

            curActiveIndex++;
        }
    }
}