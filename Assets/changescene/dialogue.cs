using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    public int paraidx = 0; //now paragraph line idx
    private int buttonidx = 0;

    // Start is called before the first frame update
    void Start()
    {
        //textcomponent.text = string.Empty; //initialize as empty string
        string parapath = Application.dataPath + "/changescene/plot.txt"; // asset's path
        Debug.Log(parapath);
        paragraph = System.IO.File.ReadAllLines(parapath);
        for (int i = 0; i < 5; i++)
        {
            textcomponent[i].text = string.Empty; 
        }
        paralinelen = paragraph.Length;
        paraidx = 0;
        lines_idx = 0;
        txt_idx = 0;

        string buttonpath = Application.dataPath + "/changescene/buttonPlot.txt";
        buttonLine = System.IO.File.ReadAllLines(buttonpath);
        buttontext.text = string.Empty;
        buttonidx = 0;

        CreateLine();
        StartDialogue();
    }
    public void ClickNextDia()
    {
        StopAllCoroutines();
        txt_idx = 0; // change
        lines_idx = 0; //change
        buttontext.text = string.Empty;
        NextPara();
    }
    void StartDialogue()
    {
        // while(lines[index]!=" ")
        //{
        //Debug.Log("startdialogue");
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
            if (txt_idx < paralinelen)
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
    //void CreateLine()
    //{
    //    lines[0] = "�A�޲z�F�U�ۤv�������A�Ө�F�U�Ӯi��";
    //    lines[1] = "���i�קK���A�A�Q��F�A����";
    //    lines[2] = "�Q��F�ȫ�а�̥L�u�^�����y�A�Q��F�ѩи̨���ª��Z��";
    //}
    void CreateLine()
    {
        int index = 0;
        for(int i = paraidx; i < paragraph.Length; i++)
        {

            if (paragraph[i] == "-")
            {
                paralinelen = i - paraidx;
                paraidx = i+1;
                
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
