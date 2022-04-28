using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusicGroupOnTrigger : MonoBehaviour
{
    public string cardTag;
    public string triggerTag;

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        Debug.Log(string.Format("{0}: enter ({1}, {2})", this, other.name, other.tag));

        if (other.tag == cardTag)
        {
            Debug.Log("Add object: " + this + ", " + other + "; " + this.GetComponent<Collider>() + ", " + other.GetComponent<Collider>());
            GetComponent<MusicPanel>().AddObject(other.GetComponent<MusicConsumer>());
        }

        if (other.tag == triggerTag)
        {
            Debug.Log("Start: " + this + ", " + other + "; " + this.GetComponent<Collider>() + ", " + other.GetComponent<Collider>());
            GetComponent<MusicPanel>().StartExhibition();
        }
    }

    /// <summary>
    /// OnTriggerExit is called when the Collider other has stopped touching the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerExit(Collider other)
    {
        Debug.Log(string.Format("{0}: leave ({1}, {2})", this, other.name, other.tag));

        if (other.tag == cardTag)
        {
            Debug.Log("Remove object: " + other);
            GetComponent<MusicPanel>().RemoveObject(other.GetComponent<MusicConsumer>());
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(triggerTag != "");
        Debug.Assert(cardTag != "");
        Debug.Assert(GetComponent<MusicPanel>() != null);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
