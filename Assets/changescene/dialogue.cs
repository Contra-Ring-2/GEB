using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class dialogue : MonoBehaviour
{
<<<<<<< HEAD
    public TextMeshProUGUI textcomponent;
    public GameObject but;
    public string[] lines;
    public float textspeed;

    private int index;
=======
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
>>>>>>> 8caa226 (dialogue)

    // Start is called before the first frame update
    void Start()
    {
<<<<<<< HEAD
        textcomponent.text = string.Empty; //initialize as empty string
        string path = Application.dataPath+ "/changescene/plot.txt"; // asset's path
        //Debug.Log(path);
        lines = System.IO.File.ReadAllLines(path);
=======
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
>>>>>>> 8caa226 (dialogue)
        StartDialogue();
    }
    public void ClickNextDia()
    {
<<<<<<< HEAD
        Debug.Log("press space");
        if (textcomponent.text == lines[index])
        {
            Debug.Log("pr1");
            NextLine();
        }
        else
        {
            Debug.Log("pr2");
            StopAllCoroutines();
            textcomponent.text = lines[index];
        }
    }
    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }
    IEnumerator TypeLine()
    {
        foreach(char c in lines[index].ToCharArray())
        {
            textcomponent.text += c;
            yield return new WaitForSeconds(textspeed);
        }
    }
    void NextLine()
    {
        if (index < lines.Length - 1) // small than last element
        {
            index++;
            textcomponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
            but.SetActive(false);
=======
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
    //    lines[0] = "你梳理了下自己的情緒，來到了下個展間";
    //    lines[1] = "不可避免的，你想到了你爸爸";
    //    lines[2] = "想到了午後教堂裡他彈琴的指尖，想到了書房裡那沓陳舊的稿紙";
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
            
            
>>>>>>> 8caa226 (dialogue)
        }
    }
}
