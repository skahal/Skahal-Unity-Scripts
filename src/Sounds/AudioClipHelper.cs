using UnityEngine;
 
public static class AudioClipHelper
{
	public static void PlayOneShotRandom(AudioSource audioSource, AudioClip[] audioClips)
	{
		PlayOneShotRandom(audioSource, audioClips, 1f);
	}
	
	public static void PlayOneShotRandom(AudioSource audioSource, AudioClip[] audioClips, float volumeScale)
	{
		audioSource.PlayOneShot(audioClips[Random.Range(0, audioClips.Length)], volumeScale);
	}
}

