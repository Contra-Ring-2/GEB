using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempocontrol : MonoBehaviour
{
    // Start is called before the first frame update
    private int[] gamelist = { 0,1,2,3,0,1,2,3,0,1,2,3};
    private GameObject[] cubelist = new GameObject[4];
    public Material basicmat;
    public Material[] matlist = new Material[4];
    public int temple = 10;
    public float time = 0;
    private bool onlight = false;
    private GameObject nowcube;
    private int nowidx = 0; //for gamelist
    void Start()
    {
        for(int i = 0; i < 4; i++)
        {
            int t = i + 1;
            string temp = "Cube" + t;
            //string temp2 = "t" + t;
            GameObject cube = GameObject.Find(temp);
            //Material mat = GameObject.Find(temp2).GetComponent;
            cubelist[i] = cube;
            //matlist[i] = mat;
        }
        //cubelist[gamelist[0]].GetComponent<Renderer>().material = 

    }

    // Update is called once per frame
    void Update()
    {
        //if (time < temple && onlight == false)
        //{
        //    onlight = true;
        //    nowcube = cubelist[gamelist[nowidx]];
        //    nowcube.GetComponent<Renderer>().material = matlist[gamelist[nowidx]];
        //}
        //else if(time >= temple)
        //{
        //    time =0;
        //    nowidx++;
        //    nowcube.GetComponent<Renderer>().material = basicmat;
        //    onlight = false;
        //}
        //time += Time.deltaTime*3;
    }
}
