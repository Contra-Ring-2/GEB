using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingControl : MonoBehaviour
{
    public GameObject arc;
    public float _circle_time = 6; //material's cycle time
    public GameObject[] arcs;
    public int time = 1;


    public class Note
    {
        public float start_time;
        public float hieght; //radius
        public float end_time;
        public float spc; //second per cycle
    }
    // Start is called before the first frame update
    private GameObject newArc;
    void Start()
    {
    }

    public void CreateRing(Note[] notes)
    {
        arcs = new GameObject[notes.Length];
        for (int i = 0; i < notes.Length; i++)
        {
            GameObject newarc = Instantiate(arc);
            Material mat_newarc = Instantiate(newarc.GetComponent<Renderer>().material);

            float interval = (notes[i].end_time - notes[i].start_time) / notes[i].spc;
            float _time = (interval / 2f) + (notes[i].start_time % notes[i].spc) * (_circle_time / notes[i].spc);
            mat_newarc.SetFloat("StartTime_ ", _time);
            mat_newarc.SetFloat("Alpha_ ", 0);
            // mat_newarc.SetFloat("Radiusinner_", notes[i].hieght);
            // mat_newarc.SetFloat("Radiusouter_ ", notes[i].hieght +2);
            //scale_change
            mat_newarc.SetFloat("Angle_ ", interval * _circle_time);
            newarc.GetComponent<Renderer>().material = mat_newarc;
            arcs[i] = newarc;
        }

    }

    /// <summary>
    /// input: array
    ///     ring-arc
    ///     arc-time
    ///     starting-point
    ///     arc-r
    ///     width
    /// output:
    ///     play()
    /// </summary>
    /// 
    void FirstRing()
    {


    }
    // Update is called once per frame
    void Update()
    {
        //test

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    GameObject newarc = Instantiate(arc);
        //    Material mat_newarc = Instantiate(newarc.GetComponent<Renderer>().material);
            
        //    mat_newarc.SetFloat("StartTime_", time);
        //    newarc.GetComponent<Renderer>().material = mat_newarc;
        //    newarc.SetActive(true);
        //    time++;
        //}
    }
}
