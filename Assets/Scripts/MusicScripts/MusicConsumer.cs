using System;
using System.Collections;
using System.Globalization;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// Playable music comsumer. <br/>
/// It needs a parent MusicGroup object. <br/>
/// </summary>
public class MusicConsumer : MonoBehaviour
{
    public MusicGroup.FlipModifier modifier = MusicGroup.FlipModifier.NORMAL;
    public float waitBeats = 0.0f;
    
    public int keyShift = 0;
    public float speedMultiplier = 1.0f;

    public bool isEnabled = false;

    // TODO: speed, pitch?

    private MusicGroup musicGroup;

    /// <summary>
    /// Play music with consumer specified argument
    /// </summary>
    public void PlayMusic()
    {
        if (!isEnabled)
        {
            return;
        }

        AudioSource source = GetComponent<AudioSource>();
        float seconds = (60 / musicGroup.tempo) * waitBeats;

        source.clip = musicGroup.GetMusicSource(modifier);
        source.outputAudioMixerGroup = musicGroup.GetMixerGroup(keyShift);

        source.PlayDelayed(seconds);

        //MasterModel.TheModel.CallbackInSecond(
        //    seconds,
        //    () =>
        //    {
        //        //source.clip = musicGroup.GetMusicSource(modifier);

        //        //Debug.Log("Playing music: " + source.clip);
        //        source.Play();
        //    }
        //);
    }

    public void PauseMusic()
    {
        GetComponent<AudioSource>().Pause();
    }

    public void StopMusic()
    {
        GetComponent<AudioSource>().Stop();
    }

    public MusicGroup.Note[] GetNotes(int partIdx)
    {
        List<MusicGroup.Note> notes = new List<MusicGroup.Note>();

        if (musicGroup.normalMusicXML == null)
        {
            // two tiger
            float[] tigerNotes =
            {
                1, 1,
                2, 1,
                3, 1,
                1, 1,

                1, 1,
                2, 1,
                3, 1,
                1, 1,

                3,   1,
                4,   1,
                5,   2,

                3,   1,
                4,   1,
                5,   2,

                5, .5F,
                6, .5F,
                5, .5F,
                4, .5F,
                3,   1,
                1,   1,

                5, .5F,
                6, .5F,
                5, .5F,
                4, .5F,
                3,   1,
                1,   1,

                 1, 1,
                -2, 1,
                 1, 2,

                 1, 1,
                -2, 1,
                 1, 2,
            };

            //List<MusicGroup.Note> notes = new List<MusicGroup.Note>();
            {
                float sumTime = (60 / musicGroup.tempo) * waitBeats; // 0;
                for (int i = 0; i < tigerNotes.Length; i += 2)
                {
                    float height = tigerNotes[i] + keyShift;
                    float duration = tigerNotes[i + 1];

                    float startTime = sumTime;
                    float endTime = startTime + duration;

                    notes.Add(new MusicGroup.Note { start_time = startTime, end_time = endTime, hieght = height });
                }
            }

            return notes.ToArray();
        }

        // TODO: optimize?
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(musicGroup.normalMusicXML.text);

        XmlNodeList parts = doc.GetElementsByTagName("part");
        Debug.Assert(parts.Count == 1);

        Dictionary<string, float> keyValue = new Dictionary<string, float>();
        keyValue.Add("C", 0.0f);
        keyValue.Add("D", 1.0f);
        keyValue.Add("E", 2.0f);
        keyValue.Add("F", 3.0f);
        keyValue.Add("G", 4.0f);
        keyValue.Add("A", 5.0f);
        keyValue.Add("B", 6.0f);

        //List<MusicGroup.Note> notes = new List<MusicGroup.Note>();

        float secPerBeat = 60 / musicGroup.tempo;
        float waitSec = secPerBeat * waitBeats;

        foreach (XmlNode part in parts)
        {
            float division = 1.0f;

            // TODO: parse/calculate these
            float beatPerMeasure = 4.0f;
            float partBaseHieght = 8.0f * partIdx;

            {
                float measureOffset = 0.0f;
                foreach (XmlNode measure in part.SelectNodes("measure"))
                {
                    Debug.Assert(measure.SelectSingleNode("backup") == null, string.Format("{0} contains 'backup'", measure.InnerXml));

                    //float division = 1.0f;

                    XmlNode attribute = measure.SelectSingleNode("attributes");
                    if (attribute != null)
                    {
                        XmlNode divisions = attribute.SelectSingleNode("divisions");
                        if (divisions != null)
                        {
                            division = float.Parse(divisions.InnerText);  // CultureInfo.InvariantCulture.NumberFormat;
                        }
                    }

                    {
                        float noteOffset = 0.0f, lastNoteLength = 0.0f;
                        foreach (XmlNode note in measure.SelectNodes("note"))
                        {
                            if (note.SelectSingleNode("chord") == null)
                            {
                                noteOffset += lastNoteLength;
                            }

                            float duration = float.Parse(
                                note.SelectSingleNode("duration").InnerText
                            );

                            XmlNode pitch = note.SelectSingleNode("pitch");
                            Debug.Assert(pitch != null, string.Format("{0} does not contain 'pitch'", note.InnerXml));

                            string step = pitch.SelectSingleNode("step").InnerText;
                            float octave = float.Parse(pitch.SelectSingleNode("octave").InnerText);

                            float noteStart = measureOffset + noteOffset;
                            float pitchValue = keyValue[step] + octave*7.0f;
                            float noteLength = duration / division;

                            float noteStartSec = waitSec + secPerBeat * noteStart;
                            float pitchHeight = partBaseHieght + keyShift + (1.0f + pitchValue);
                            float noteLengthSec = secPerBeat * noteLength;

                            MusicGroup.Note note1 = new MusicGroup.Note
                            {
                                start_time = noteStartSec,
                                end_time   = noteStartSec + noteLengthSec,
                                hieght     = pitchHeight
                            };

                            notes.Add(note1);

                            lastNoteLength = noteLength;
                        }
                    }

                    measureOffset += beatPerMeasure;
                }
            }
        }

        return notes.ToArray();
    }

    // Start is called before the first frame update
    void Start()
    {
        musicGroup = transform.parent.GetComponent<MusicGroup>();
        Debug.Assert(musicGroup != null, "MusicCosumer needs a parent MusicGroup object");

        //if (targetTempo == 0.0f)
        //{
        //    targetTempo = musicGroup.tempo;
        //}

        // configure consumer
        gameObject.AddComponent<AudioSource>();
        //GetComponent<AudioSource>().outputAudioMixerGroup = Instantiate(musicGroup.defaultMixerGroup);
        //GetComponent<AudioSource>().outputAudioMixerGroup = musicGroup.defaultMixerGroup;

        musicGroup.AddConsumer(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
