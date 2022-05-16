using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPath : MonoBehaviour
{
    private int pathidx = 5;
    public string[] parapaths;
    public string[] buttonpaths;
    // Start is called before the first frame update
    void Start()
    {
        parapaths = new string[pathidx];
        buttonpaths = new string[pathidx];
        for (int i = 0; i < pathidx; i++)
        {
            parapaths[i] = Application.dataPath + "/Dialogue/Dialogue1_" + (i + 1) + ".txt";
            buttonpaths[i] = Application.dataPath + "/Dialogue/Button1_" + (i + 1) + ".txt";
            Debug.Log(parapaths[i]);
        }

    }
}
