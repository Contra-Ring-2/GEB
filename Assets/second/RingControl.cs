using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

using Note = MusicGroup.Note;

public class RingControl : MonoBehaviour
{
    private GameObject arc_prefab;
    public float _circle_time = 6; //material's cycle time
    public GameObject[] arcs;

    private RingGroup ringGroup;

    // Note -> MusicGroup.Note
    private Note[] local_notes;

    // Start is called before the first frame update
    private GameObject newArc;
    void Start()
    {
        ringGroup = transform.parent.GetComponent<RingGroup>();
        Debug.Assert(ringGroup != null, "RingControl needs a parent RingGroup object");

        Debug.Assert(GetComponent<MusicConsumer>() != null, "RingControl needs a MusicConsumer");

        ringGroup.AddControl(this);
        arc_prefab = ringGroup.arc_prefab;
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

            // Debug:
            {
                newarc.transform.Translate(new Vector3(0, 0, 3));
            }
        }

    }

    // TODO: beats per measure (time signature)
    public void Play(float tempo)
    {
        Note[] notes = GetComponent<MusicConsumer>().GetNotes();
        CreateRing(notes, (60 / tempo) * 8, 50);
    }

    public void UpdateNotes(float time, float spc)
    {
        //Debug.Log("Updating");

        Color _color1 = arcs[0].GetComponent<Renderer>().material.color; //.GetFloat("")
        Color _color2 = _color1 * 1.2f;
        for (int i = 0; i < arcs.Length; i++)
        {
            if(local_notes[i].start_time < time && local_notes[i].end_time > time)
            {
                arcs[i].GetComponent<Renderer>().material.color = _color2;
            }
            else
            {
                arcs[i].GetComponent<Renderer>().material.color = _color1;
            }
            if(local_notes[i].start_time <time-spc/2 && local_notes[i].end_time > time - spc / 2)
            {
                // Debug:
                Debug.Log("Fading in: " + local_notes[i]);

                float _p = ((time - spc / 2) - local_notes[i].start_time) / (local_notes[i].end_time - local_notes[i].start_time);
                arcs[i].GetComponent<Renderer>().material.SetFloat("Alpha_", _p);
            }
            if (local_notes[i].start_time < time + spc / 2 && local_notes[i].end_time > time + spc / 2)
            {
                // Debug:
                Debug.Log("Fading out: " + local_notes[i]);

                float _p = (local_notes[i].end_time - (time + spc / 2)) / (local_notes[i].end_time - local_notes[i].start_time);
                arcs[i].GetComponent<Renderer>().material.SetFloat("Alpha_", _p);
            }
        }
    }

    public void Pause()
    {

    }

    public void Stop()
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
