using UnityEngine;
using System.Collections;

public static class SHInput
{
	#region Properties
	public static bool Paused;
	#endregion
	
	public static bool GetMouseButton(int button)
	{
		if (Paused)
		{
			return false;
		}
		
		return Input.GetMouseButton(button);
	}
}

