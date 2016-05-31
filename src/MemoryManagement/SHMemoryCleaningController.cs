#region Usings
using System;
using Skahal.Logging;
using UnityEngine;
using System.Collections;
using Skahal.MemoryManagement;
#endregion;

/// <summary>
/// Controls the memory cleaning schedule
/// </summary>
[AddComponentMenu("Skahal/Memory Management/SHMemoryCleaningController")]
public class SHMemoryCleaningController : MonoBehaviour
{
	#region Editor properties
	/// <summary>
	/// Gets or sets the cleaning interval.
	/// </summary>
	/// <value>
	/// The cleaning interval.
	/// </value>
	public float CleaningInterval = 300; // 5 minutes.
	#endregion	
	
	#region Methods
	private void Awake ()
	{
		DontDestroyOnLoad(this);
		StartCoroutine (CheckCleaningTime ());
	}
	
	/// <summary>
	/// Checks the cleaning time.
	/// </summary>
	/// <returns>
	/// The cleaning time.
	/// </returns>
	private IEnumerator CheckCleaningTime ()
	{
		while (true)
		{
			yield return new WaitForSeconds(CleaningInterval);
			SHMemoryCleaner.Clear ();		
		}
	}
	#endregion 
}