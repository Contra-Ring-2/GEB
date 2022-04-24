using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controller of a ring group (for music benches). <br/>
/// Provider for RingControl child objects.
/// </summary>
public class RingGroup : MonoBehaviour
{
	private readonly List<RingControl> rings = new List<RingControl>();
	public GameObject arc_prefab;

	public void PlayAllRings(float tempo)
	{
		for (int i = 0; i < rings.Count; i++)
		{
			RingControl ring = rings[i];
			ring.Play(tempo, i);
		}
	}

	public void PauseAllRings()
	{
		foreach (RingControl ring in rings)
		{
			ring.Pause();
		}
	}

	public void StopAllRings()
	{
		foreach (RingControl ring in rings)
		{
			ring.Stop();
		}
	}

	public void UpdateAllRingNotes(float time, float spc)
    {
		foreach (RingControl ring in rings)
		{
			ring.UpdateNotes(time, spc);
		}
	}

	public void AddControl(RingControl ring)
	{
		rings.Add(ring);
	}
}
