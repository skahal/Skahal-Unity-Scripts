#region Usings
using System;
using System.Collections;
using UnityEngine;

#endregion

namespace Skahal
{
	/// <summary>
	/// A helper class to generic stuffs on Unity.
	/// </summary>
	public class SH : MonoBehaviour
	{
		#region Events
		/// <summary>
		/// Occurs when application paused.
		/// </summary>
		public static event EventHandler ApplicationPaused;
	
		/// <summary>
		/// Occurs when application resumed.
		/// </summary>
		public static event EventHandler ApplicationResumed;
		#endregion

		#region Fields
		private static MonoBehaviour s_script;
		#endregion

		#region Methods
		void Awake ()
		{
			s_script = this;
			DontDestroyOnLoad (this);
		}
	
		public static new Coroutine StartCoroutine (IEnumerator routine)
		{
			ValidateState ();
			return s_script.StartCoroutine (routine);
		}

		public static new void StopCoroutine (Coroutine coroutine)
		{
			ValidateState ();
			s_script.StopCoroutine (coroutine);
		}

		public static void Sleep (float seconds)
		{
			ValidateState ();
			s_script.StartCoroutine (ExecuteSleep (seconds));
		}

		static IEnumerator ExecuteSleep (float seconds)
		{
			yield return new WaitForSeconds (seconds);
		}

		void OnApplicationPause (bool pause)
		{
			ValidateState ();
		
			if (pause) {
				if (ApplicationPaused != null) {
					ApplicationPaused (s_script, EventArgs.Empty);
				}
			} else if (ApplicationResumed != null) {
				ApplicationResumed (s_script, EventArgs.Empty);
			}
		}
			
		private static void ValidateState ()
		{
			if (s_script == null) {
				s_script = new GameObject ("SH").AddComponent<SH> ();
			}
		}
		#endregion
	}

}