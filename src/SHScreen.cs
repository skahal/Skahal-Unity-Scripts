using UnityEngine;
 
[AddComponentMenu("Skahal/SHScreen")]
public class SHScreen : MonoBehaviour
{
	#region Properties
	public static Rect FullScreenRect = new Rect(0, 0, Screen.width, Screen.height);
	public static Vector2 CenterScreenPoint = new Vector2(Screen.width / 2, Screen.height / 2);
	
	public static float Width = Screen.width;
	public static float Height = Screen.height;
	
	public static float HalfWidth = Screen.width / 2;
	public static float HalfHeight = Screen.height / 2;
	
	public static float QuarterWidth = Screen.width / 4;
	public static float QuarterHeight = Screen.height / 4;
	#endregion
	
	#region Life cycle
	void OnLevelWasLoaded()
	{
		FullScreenRect = new Rect(0, 0, Screen.width, Screen.height);
		CenterScreenPoint = new Vector2(Screen.width / 2, Screen.height / 2);
		
		Width = Screen.width;
		Height = Screen.height;
		
		HalfWidth = Screen.width / 2;
		HalfHeight = Screen.height / 2;
		
		QuarterWidth = Screen.width / 4;
		QuarterHeight = Screen.height / 4;
	}
	#endregion
}

