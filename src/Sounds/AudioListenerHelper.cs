using UnityEngine;
 

public static class AudioListenerHelper
{
	public static void TurnOn()
	{
		AudioListener.pause = false;
	}
	
	public static void TurnOff()
	{
		AudioListener.pause = true;
	}
}

