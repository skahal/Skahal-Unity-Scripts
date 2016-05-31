#region Usings
using UnityEngine;
#endregion

namespace Skahal.Sounds
{
	/// <summary>
	/// Helpers for AudioSource component.
	/// </summary>
	public static class SHAudioSource
	{
		/// <summary>
		/// Changes the volume from all AudioSource components in the current scene.
		/// </summary>
		/// <param name='newVolume'>
		/// New volume.
		/// </param>
		public static void ChangeVolumeFromAll (float newVolume)
		{
			AudioSource[] all = FindAll();
			
			foreach (AudioSource audio in all)
			{
				audio.volume = newVolume;
			}
		}
		
		/// <summary>
		/// Changes the volume percent from all AudioSource components in the current scene.
		/// </summary>
		/// <param name='percent'>
		/// Percent.
		/// </param>
		public static void ChangeVolumePercentFromAll (float percent)
		{
			AudioSource[] all = FindAll();
		
			foreach (AudioSource audio in all)
			{
				audio.volume *= percent;
			}
		}
		
		/// <summary>
		/// Finds all AudioSource components in the current scene.
		/// </summary>
		/// <returns>
		/// The all AudioSource components.
		/// </returns>
		public static AudioSource[] FindAll()
		{
			return (AudioSource[])Resources.FindObjectsOfTypeAll (typeof(AudioSource));
		}
	}
}