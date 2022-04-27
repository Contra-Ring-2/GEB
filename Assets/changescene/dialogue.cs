using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class dialogue : MonoBehaviour
{
    public TextMeshProUGUI[] textcomponent;
    public TextMeshProUGUI buttontext;
    public GameObject but;
    public string[] lines;
    public string[] paragraph;
    public string[] buttonLine;
    public float textspeed;

    public int lines_idx = 0;
    public int txt_idx = 0;
    public int paralinelen = 0; //now paragraph length
    public int paraidx = -1; //store "-" position
    private int buttonidx = 0;

    private string parapath;
    private string buttonpath;
    public GameObject storyUI;
    public GameObject NPC;

    // Start is called before the first frame update
    void Start()
    {
        //textcomponent.text = string.Empty; //initialize as empty string
        parapath = Application.dataPath + "/changescene/plot.txt"; // asset's path
        //Debug.Log(parapath);
        //paragraph = System.IO.File.ReadAllLines(parapath);
        //for (int i = 0; i < 5; i++)
        //{
        //    textcomponent[i].text = string.Empty; 
        //}
        //paralinelen = paragraph.Length;
        //paraidx = 0;
        //lines_idx = 0;
        //txt_idx = 0;
        SetParagString();

        buttonpath = Application.dataPath + "/changescene/buttonPlot.txt";
        //buttonLine = System.IO.File.ReadAllLines(buttonpath);
        //buttontext.text = string.Empty;
        //buttonidx = 0;
        SetButtonString();

        //CreateLine();
        //StartDialogue();
    }
    void SetParagString()
    {
        Array.Clear(paragraph, 0, paragraph.Length);
        paragraph = System.IO.File.ReadAllLines(parapath);
        for (int i = 0; i < 5; i++)
        {
            textcomponent[i].text = string.Empty;
        }
        paralinelen = paragraph.Length;
        paraidx = -1;
        lines_idx = 0;
        txt_idx = 0;
    }
    public void SetButtonString()
    {
        Array.Clear(buttonLine, 0, buttonLine.Length);
        //Debug.Log(buttonpath);
        buttonLine = System.IO.File.ReadAllLines(buttonpath);
        buttontext.text = string.Empty;
        buttonidx = 0;
    }
    public void SetParaPath(string path)
    {
        parapath = path;
    }
    public void SetButtonPath(string path)
    {
        buttonpath = path;
    }

    public void ClickNextDia()
    {
        StopAllCoroutines();
        txt_idx = 0; // change
        lines_idx = 0; //change
        buttontext.text = string.Empty;
        if (paraidx+1 >= paragraph.Length)
        {
            //end this storyUI
            Debug.Log("end");
            storyUI.SetActive(false);
            for (int i = 0; i < 5; i++)
            {
                textcomponent[i].text = string.Empty;
            }
            NPC.GetComponent<NPCmove>().canwalk = true;
            return;

        }
        else
        {
            Debug.Log("nextpara");
            NextPara();
        }
        
    }
    public void SetAndStart()
    {
        Debug.Log("setandstart");
        SetButtonString();
        SetParagString();
        
        CreateLine();
        StartDialogue();
        
    }
    public void StartDialogue()
    {
        // while(lines[index]!=" ")
        //{
        Debug.Log("startdialogue");
        StartCoroutine(TypeLine());
        //}
        
    }
    IEnumerator TypeLine()
    {
        foreach(char c in lines[lines_idx].ToCharArray())
        {
            textcomponent[txt_idx].text += c;
            yield return new WaitForSeconds(textspeed);
        }
        //Debug.Log("hello");
        if (textcomponent[txt_idx].text == lines[lines_idx])//check
        {
            txt_idx += 1;
            lines_idx += 1;
            if (txt_idx < paralinelen && lines_idx<5)
            {
                //Debug.Log(txt_idx);
                StartDialogue();
            }
            else
            {
                //Debug.Log("test");
                Invoke("SetbuttonText", 2f); //delay
            }
        }

    }
    void SetbuttonText()
    {
        Debug.Log("setbuttontext");
        if(buttonidx >= buttonLine.Length)
        {
            return;
        }
        buttontext.text = buttonLine[buttonidx];
        buttonidx += 1;
    }
    void NextPara()
    {
        //lines_idx++;
        for(int i = 0; i < 5; i++)
        {
            textcomponent[i].text = string.Empty;
            lines[i] = string.Empty;
        }
        //paraidx = paralinelen + 1;
        CreateLine();
        //Debug.Log("nextPAra");
        StartDialogue();
    }
    void CreateLine()
    {
        int index = 0;
        for(int i = paraidx+1; i < paragraph.Length; i++)
        {

            if (paragraph[i] == "-")
            {
                paralinelen = i - paraidx;
                paraidx = i;
                
                //Debug.Log(paraidx);
                break;
            }
            else
            {
                //Debug.Log(paragraph[i]);
                lines[index] = paragraph[i];
                index += 1;
            }
            
            
        }
    }
}
