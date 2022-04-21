using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPanel : MonoBehaviour
{

	private List<RingControl> rings = new List<RingControl>();
	

	private void OnTriggerEnter(Collider other)
	{
		MusicConsumer mc = other.GetComponent<MusicConsumer>();
		if (mc != null){
			mc.isEnabled = true;
		}

		if (other.tag == "Player")
        {
			StartExhibition();
        }
	}

	private void OnTriggerExit(Collider other)
	{
		MusicConsumer mc = other.GetComponent<MusicConsumer>();
		if (mc != null)
		{
			mc.isEnabled = false;
		}
	}

	private void StartExhibition() // ½Ð¶}©lªíºt (一句廢話，好像是開始表演拉)
	{
		MusicGroup musicGroup = GetComponent<MusicGroup>();
		RingGroup ringGroup = GetComponent<RingGroup>();

		ringGroup.PlayAllRings(musicGroup.tempo);

		GetComponent<MusicGroup>().PlayAllMusic();

		float startTime = Time.time;
		float spc = 8 * (60 / GetComponent<MusicGroup>().tempo); // is this 8?

		void UpdateExhibition()
        {
			GetComponent<RingGroup>().UpdateAllRingNotes(startTime, spc);
			MasterModel.TheModel.CallbackWaitingFor(new WaitForFixedUpdate(), UpdateExhibition);
		}

		MasterModel.TheModel.CallbackInSecond(
			0,
			UpdateExhibition
		);
	}

    private void Start()
    {
		
    }

}

