using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusicGroupOnTrigger : MonoBehaviour
{
    public string cardTag;
    public string triggerName;

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        Debug.Log(string.Format("{0}: enter {1}", this, other));

        if (other.tag == cardTag)
        {
            GetComponent<MusicPanel>().AddObject(other.GetComponent<MusicConsumer>());
        }

        if (other.name == triggerName)
        {
            GetComponent<MusicPanel>().StartExhibition();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(triggerName != "");
        Debug.Assert(cardTag != "");
        Debug.Assert(GetComponent<MusicPanel>() != null);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
