#region Usings
using UnityEngine;
using System.Collections;
#endregion

/// <summary>
/// Helper stuffs for unit and functional tests.
/// </summary>
public class SHTestHelper : MonoBehaviour
{
	#region Fields
	private static SHTestHelper s_instance;
	#endregion
	
	#region Public Methods
	public static void Wait (float seconds)
	{
		PrepareSingleton ();
		s_instance.StartCoroutine(s_instance.WaitFor(seconds));
	}
	#endregion
	
	#region Private Methods
	private static void PrepareSingleton ()
	{
		if (s_instance == null)
		{
			s_instance = new GameObject ("SHTestHelper").AddComponent<SHTestHelper> ();
		}
	}
	
	private IEnumerator WaitFor(float seconds)
	{
		yield return new WaitForSeconds(seconds);
	}
	#endregion
}