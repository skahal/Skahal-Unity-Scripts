#region Usings
using UnityEngine;
using Skahal.Logging;
#endregion

#region Enums
public enum DayNightControllerBehavior
{
	Auto,
	OnlyDay,
	OnlyNight
}
#endregion

/// <summary>
/// A day and night enviroment controller script.
/// </summary>
[AddComponentMenu("Skahal/Environment/Day Night Controller Script")]
public class DayNightControllerScript : MonoBehaviour
{
	#region Constructors
	public DayNightControllerScript()
	{
		Instance = this;
	}
	#endregion
	
	#region Properties
	public int DayStartHour = 7;
	public int DayEndHour = 9;
	public Material DaySkybox;
	public Material NightSkybox;
	public Color DayAmbientLight = new Color (180, 180, 180, 255);
	public Color NightAmbientLight = new Color (0, 0, 0, 0);
	public GameObject[] DayGameObjects;
	public GameObject[] NightGameObjects;
	public DayNightControllerBehavior Behavior = DayNightControllerBehavior.Auto;
	[HideInInspector]
	public static DayNightControllerScript Instance;
	
	public bool IsDay { get; private set; }
		
	#endregion
	
	#region Public Methods
	public void ChangeTo (DayNightControllerBehavior behavior)
	{
		Behavior = behavior;
		
		int hour = System.DateTime.Now.Hour;
		
		switch (Behavior)
		{
			case DayNightControllerBehavior.Auto:
				if (hour >= DayStartHour && hour < DayEndHour)
				{
					StartDay();
				}
				else
				{
					StartNight();
				}
				
				break;
			
			case DayNightControllerBehavior.OnlyDay:
				StartDay();
				break;
			
			case DayNightControllerBehavior.OnlyNight:
				StartNight();
				break;
		}
	}
	#endregion
	
	#region Private Methods
	void Start()
	{
		ChangeTo(Behavior);
	}
	
	void StartDay ()
	{
		SHLog.Debug ("DayNightControllerScript.StartDay");
		IsDay = true;
		RenderSettings.skybox = DaySkybox;
		RenderSettings.ambientLight = DayAmbientLight;
		SHGameObjectHelper.Deactive (NightGameObjects, true);
		SHGameObjectHelper.Active (DayGameObjects, true);
		Messenger.Send ("OnDayStarted");
	}
	
	void StartNight ()
	{
		SHLog.Debug ("DayNightControllerScript.StartNight");
		IsDay = false;
		RenderSettings.skybox = NightSkybox;
		RenderSettings.ambientLight = NightAmbientLight;
		SHGameObjectHelper.Deactive (DayGameObjects, true);
		SHGameObjectHelper.Active (NightGameObjects, true);
		Messenger.Send ("OnNightStarted");
	}
	#endregion
}