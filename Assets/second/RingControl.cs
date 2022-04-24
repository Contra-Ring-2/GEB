using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

using Random = System.Random;
using Note = MusicGroup.Note;

public class RingControl : MonoBehaviour
{
    public Color ringColor;

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
            Material mat_newarc = new Material(newarc.GetComponent<Renderer>().material); // Instantiate(newarc.GetComponent<Renderer>().material);


            float interval = (notes[i].end_time - notes[i].start_time) / spc;
            float _time = (interval / 2f) + (notes[i].start_time) * (_circle_time / spc);

            //int shaderAlphaID = Shader.PropertyToID("Alpha_");

            mat_newarc.SetFloat("Alpha_", 0);
            //mat_newarc.SetFloat("StartTime_ ", _time);
            //mat_newarc.SetFloat(shaderAlphaID, 0);
            // mat_newarc.SetFloat("Radiusinner_", notes[i].hieght);
            // mat_newarc.SetFloat("Radiusouter_ ", notes[i].hieght +2);
            //scale_change
            //mat_newarc.SetFloat("Scale_", 1 + (notes[i].hieght / hieght_range));

            float scaleFac = 1 + (notes[i].hieght / hieght_range);
            newarc.transform.localScale = new Vector3(scaleFac, scaleFac, scaleFac);

            mat_newarc.SetFloat("Angle_", interval * _circle_time);

            if (ringColor == null)
            {
                Color color = arcs[0].GetComponent<Renderer>().material.GetColor("Color_");
                
                Debug.LogWarning(string.Format("{0}: ringColor is null, default to prefab color = {1}.", this, color));
                ringColor = color;
            }

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
        // Debug:
        //{
        //    //Debug.Log("Updating");

        //    Random random = new Random();
        //    foreach (GameObject arc in arcs)
        //    {
        //        Material material = arc.GetComponent<Renderer>().material; // new Material(arc.GetComponent<Renderer>().material);

        //        material.SetFloat(
        //            "Alpha_",
        //            1.0f
        //        //(float)random.NextDouble()
        //        );

        //        material.SetColor(
        //            "Color_",
        //            new Color(
        //                (float)random.NextDouble(),
        //                (float)random.NextDouble(),
        //                (float)random.NextDouble()
        //            )
        //        );

        //        float scaleFac = (float)(0.5 + random.NextDouble());
        //        //arc.transform.localScale.Scale(new Vector3(scaleFac, scaleFac, scaleFac));
        //        arc.transform.localScale = new Vector3(scaleFac, scaleFac, scaleFac);

        //        material.SetFloat(
        //            "Angle_",
        //            (float)(random.NextDouble())
        //        );

        //        material.SetFloat(
        //            "Theta_",
        //            (float)(_circle_time * random.NextDouble())
        //        //(float)random.NextDouble()
        //        );

        //        //arc.GetComponent<Renderer>().material = null; // material;
        //        //arc.GetComponent<Renderer>().materials = new Material[] { material, };
        //        //arc.GetComponent<Renderer>().material = material;

        //        arc.transform.Translate(new Vector3(0, 0, 0.003f * (float)random.NextDouble()));
        //    }

        //    return;
        //}

        Color _color1 = ringColor; // arcs[0].GetComponent<Renderer>().material.GetColor("Color_"); // arcs[0].GetComponent<Renderer>().material.color; //.GetFloat("")
        Color _color2 = _color1 * 1.2f;
        for (int i = 0; i < arcs.Length; i++)
        {
            // Debug:
            //if (i != 0)
            //{
            //    //arcs[i].SetActive(false);
            //    continue;
            //}

            // Debug:
            {
                Vector3 pos = arcs[i].transform.localPosition;
                arcs[i].transform.localPosition = new Vector3(pos.x, pos.y, i / 8.0f);
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
                // Debug:
                //Debug.Log("Fading in: " + local_notes[i]);

                float _p = (time - (local_notes[i].start_time - spc / 2)) / (local_notes[i].end_time - local_notes[i].start_time);
                arcs[i].GetComponent<Renderer>().material.SetFloat("Alpha_", _p);

                //Debug.Log(string.Format("Fading in: note={0}, _p={1}", local_notes[i], _p));
            }
            
            if (local_notes[i].start_time + spc/2 < time && local_notes[i].end_time + spc/2 > time)
            {
                // Debug:
                //Debug.Log("Fading out: " + local_notes[i]);

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
                (float)(6.28 * ((time + local_notes[i].start_time) % spc / spc))
            );
        }
    }

    public void Pause()
    {

    }

    public void Stop()
    {
        // TODO: remove all cloned arcs
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
