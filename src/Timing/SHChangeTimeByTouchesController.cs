#region Usings
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Skahal.Timing;
#endregion

/// <summary>
/// Performs change in the game time based on gamer inputs.
/// </summary>
[AddComponentMenu("Skahal/Timing/SHChangeTimeByTouchesController")]
public class SHChangeTimeByTouchesController : MonoBehaviour
{
	#region Fields
	private Dictionary<int, float> m_touchesTimeScale;
	#endregion
	
	#region Properties
	public SHTouchesTimeScaleEntry[] TouchesTimeScale;
	public float IgnoreWhenTimeScaleLowerThan = 1;
	#endregion
	
	#region Life cycle
	private void Awake ()
	{
		if (TouchesTimeScale.Length == 0)
		{
			Destroy (this);
		}
		else
		{
			// Prepares the internal dictionary to be used in a fast way
			// during runtime.
			m_touchesTimeScale = new Dictionary<int, float> ();
			
			foreach (var e in TouchesTimeScale)
			{
				m_touchesTimeScale.Add (e.Touches, e.TimeScale);
			}
		}
	}
	
	private void Update ()
	{
		if (Time.timeScale >= IgnoreWhenTimeScaleLowerThan)
		{
			var touchesCount = Input.touchCount;
		
			if (m_touchesTimeScale.ContainsKey (touchesCount))
			{
				Time.timeScale = m_touchesTimeScale [touchesCount];
			}
		}
	}
	#endregion
}