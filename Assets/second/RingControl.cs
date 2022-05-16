using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

using Random = System.Random;
using Note = MusicGroup.Note;

public class RingControl : MonoBehaviour
{
    public Color ringColor;
    public Transform ringLocationOverride;
    //public float scaleMax = 2.2f;
    //public float scaleMin = 0.3f;

    private GameObject arc_prefab;
    //public const float _circle_time = 1; //6; //material's cycle time
    public GameObject[] arcs = null;

    private RingGroup ringGroup;

    // Note -> MusicGroup.Note
    private Note[] local_notes;

    // Start is called before the first frame update
    private GameObject newArc;
    void Start()
    {
        // ringGroup = transform.parent.GetComponent<RingGroup>();
        // Debug.Assert(ringGroup != null, "RingControl needs a parent RingGroup object");
        ringGroup = null;
        for (Transform anc = transform.parent; anc != null; anc = anc.parent)
        {
            ringGroup = anc.GetComponent<RingGroup>();
            
            if (ringGroup != null)
            {
                break;
            }
        }

        Debug.Assert(ringGroup != null, "RingControl needs an ancestor RingGroup");
        Debug.Assert(GetComponent<MusicConsumer>() != null, "RingControl needs a MusicConsumer");

        ringGroup.AddControl(this);
        arc_prefab = ringGroup.arc_prefab;
    }

    public void CreateRing(Note[] notes,float spc)
    {
        local_notes = notes;

        // analyze height of notes
        /*
        float heightMin = 1e20f, heightMax = -1e20f;
        foreach (Note note in notes)
        {
            heightMin = Math.Min(heightMin, note.hieght);
            heightMax = Math.Max(heightMax, note.hieght);
        }
        */

        //float heightRange = heightMax - heightMin;
        // float heightRange = 48;
        float heightRange = ringGroup.heightMax - ringGroup.heightMin;

        {
            float heightMin = 1e20f, heightMax = -1e20f;
            foreach (Note note in notes)
            {
                heightMin = Math.Min(heightMin, note.hieght);
                heightMax = Math.Max(heightMax, note.hieght);
            }

            Debug.Log(string.Format("Ring: [{0}, {1}]", heightMin, heightMax));
        }

        arcs = new GameObject[notes.Length];
        for (int i = 0; i < notes.Length; i++)
        {
            Transform parent = ringLocationOverride ? ringLocationOverride : ringGroup.ringLocation;
            GameObject newarc = Instantiate(arc_prefab, parent);
            // newarc.transform.parent = ringLocationOverride ? ringLocationOverride : ringGroup.ringLocation;

            Material mat_newarc = new Material(newarc.GetComponent<Renderer>().material); // Instantiate(newarc.GetComponent<Renderer>().material);

            // // Debug:
            // newarc.transform.Translate(new Vector3(0, 0, 3));

            float interval = (notes[i].end_time - notes[i].start_time) / spc;
            //float _time = (interval / 2f) + (notes[i].start_time); // * (_circle_time / spc);

            //int shaderAlphaID = Shader.PropertyToID("Alpha_");

            mat_newarc.SetFloat("Alpha_", 0);

            //mat_newarc.SetFloat("Scale_", 1 + (notes[i].hieght / hieght_range));

            {
                //const float ratio = (float)(2 / (2 - 0.2));
                //float rateMax = 2.2f, rateMin = 0.3f;
                float scaleMax = ringGroup.scaleMax, scaleMin = ringGroup.scaleMin;
                float ratio = (float) Math.Pow(scaleMax / scaleMin, 1 / heightRange);

                //float scaleFac = 2 * (notes[i].hieght); // 1 + (notes[i].hieght / hieght_range);
                float scaleFac = (float) (scaleMin * Math.Pow(ratio, notes[i].hieght - ringGroup.heightMin));
                newarc.transform.localScale = new Vector3(scaleFac, scaleFac, scaleFac);
            }

            //mat_newarc.SetFloat("Angle_", interval * _circle_time);
            mat_newarc.SetFloat("Angle_", interval);

            if (ringColor == null)
            {
                Color color = arcs[0].GetComponent<Renderer>().material.GetColor("Color_");
                
                Debug.LogWarning(string.Format("{0}: ringColor is null, default to prefab color = {1}.", this, color));
                ringColor = color;
            }

            newarc.GetComponent<Renderer>().material = mat_newarc;
            arcs[i] = newarc;
        }

    }

    // TODO: beats per measure (time signature)
    public void Play(float tempo, int partIdx)
    {
        if (!GetComponent<MusicConsumer>().isEnabled)
        {
            return;
        }

        Note[] notes = GetComponent<MusicConsumer>().GetNotes(partIdx);
        CreateRing(notes, (60 / tempo) * 8); //, 50);
    }

    public void UpdateNotes(float time, float spc)
    {
        if (arcs == null || new List<GameObject>(arcs).Contains(null))
        {
            return;
        }

        Color _color1 = ringColor; // arcs[0].GetComponent<Renderer>().material.GetColor("Color_"); // arcs[0].GetComponent<Renderer>().material.color; //.GetFloat("")
        Color _color2 = _color1 * 1.5f; // Color.white; // _color1 * 1.2f;
        for (int i = 0; i < arcs.Length; i++)
        {
            if (arcs[i] == null)
            {
                continue;
            }

            if (local_notes[i].start_time < time && local_notes[i].end_time > time)
            {
                //arcs[i].GetComponent<Renderer>().material.color = _color2;
                arcs[i].GetComponent<Renderer>().material.SetColor("Color_", _color2);
            }
            else
            {
                //arcs[i].GetComponent<Renderer>().material.color = _color1;
                arcs[i].GetComponent<Renderer>().material.SetColor("Color_", _color1);
            }
            
            if(local_notes[i].start_time - spc/2 < time && local_notes[i].end_time - spc/2 > time)
            {
                float _p = (time - (local_notes[i].start_time - spc / 2)) / (local_notes[i].end_time - local_notes[i].start_time);
                arcs[i].GetComponent<Renderer>().material.SetFloat("Alpha_", _p);

                //Debug.Log(string.Format("Fading in: note={0}, _p={1}", local_notes[i], _p));
            }
            
            if (local_notes[i].start_time + spc/2 < time && local_notes[i].end_time + spc/2 > time)
            {
                float _p = ((local_notes[i].end_time + spc / 2) - time) / (local_notes[i].end_time - local_notes[i].start_time);
                arcs[i].GetComponent<Renderer>().material.SetFloat("Alpha_", _p);

                //Debug.Log(string.Format("Fading out: note={0}, _p={1}", local_notes[i], _p));
            }

            if (local_notes[i].end_time + spc / 2 < time)
            {
                arcs[i].SetActive(false);
            }

            //Debug.Log(string.Format("Range ({0}, {1}): {2}, {3}, {4}", local_notes[i].start_time, local_notes[i].end_time, time - spc / 2, time, time + spc / 2));

            arcs[i].GetComponent<Renderer>().material.SetFloat(
                "Theta_",
                (float)(6.28 * ((time - local_notes[i].start_time) % spc / spc))
            );
        }
    }

    public void Pause()
    {
        
    }

    public void Stop()
    {
        if (arcs != null)
        {
            var old_arcs = arcs;
            arcs = null;

            // TODO: remove all cloned arcs
            foreach (GameObject arc in old_arcs)
            {
                Debug.Assert(arc != null);

                arc.SetActive(false);
                // Destroy(arc);
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
