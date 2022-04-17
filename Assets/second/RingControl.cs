using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingControl : MonoBehaviour
{
    public GameObject arc_prefab;
    public float _circle_time = 6; //material's cycle time
    public GameObject[] arcs;

    private Note[] local_notes;

    public class Note
    {
        public float start_time;
        public float end_time;
        public float hieght; // radius
    }
    // Start is called before the first frame update
    private GameObject newArc;
    void Start()
    {
    }

    public void CreateRing(Note[] notes,float spc,float hieght_range)
    {
        local_notes = notes;

        arcs = new GameObject[notes.Length];
        for (int i = 0; i < notes.Length; i++)
        {
            GameObject newarc = Instantiate(arc_prefab);
            Material mat_newarc = Instantiate(newarc.GetComponent<Renderer>().material);

            float interval = (notes[i].end_time - notes[i].start_time) / spc;
            float _time = (interval / 2f) + (notes[i].start_time) * (_circle_time / spc);
            mat_newarc.SetFloat("StartTime_ ", _time);
            mat_newarc.SetFloat("Alpha_ ", 0);
            // mat_newarc.SetFloat("Radiusinner_", notes[i].hieght);
            // mat_newarc.SetFloat("Radiusouter_ ", notes[i].hieght +2);
            //scale_change
            mat_newarc.SetFloat("Scale_", 1 + (notes[i].hieght / hieght_range));
            mat_newarc.SetFloat("Angle_ ", interval * _circle_time);
            newarc.GetComponent<Renderer>().material = mat_newarc;
            arcs[i] = newarc;
        }

    }


    public void Play()
    {

        float timer = Time.time;
        for(int i = 0; i < arcs.Length; i++)
        {
            if(timer <  local_notes[i].end_time)
            {

            }
        }
        

    }

    void UpdateNotes()
    {
        for(int i = 0; i < arcs.Length; i++)
        {
            if(local_notes[i].start_time<1 && local_notes[i].end_time > 1)
            {

            }
        }
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
