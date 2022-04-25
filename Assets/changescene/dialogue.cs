using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class dialogue : MonoBehaviour
{
    public TextMeshProUGUI textcomponent;
    public GameObject but;
    public string[] lines;
    public float textspeed;

    private int index;

    // Start is called before the first frame update
    void Start()
    {
        textcomponent.text = string.Empty; //initialize as empty string
        string path = Application.dataPath+ "/changescene/plot.txt"; // asset's path
        //Debug.Log(path);
        lines = System.IO.File.ReadAllLines(path);
        StartDialogue();
    }
    public void ClickNextDia()
    {
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
        }
    }
}
