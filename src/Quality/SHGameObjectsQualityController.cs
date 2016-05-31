#region Usings
using UnityEngine;
using System.Collections;
using System.Linq;
using Skahal.Logging;
#endregion

#region Enums
/// <summary>
/// The qualities of game objects.
/// </summary>
public enum SHGameObjectsQuality
{
	Normal,
	Good,
	Best
}
#endregion

/// <summary>
/// Controls the quality of environment elements based on current device capabilities.
/// </summary>
[AddComponentMenu("Skahal/Quality/SHGameObjectsQualityController")]
public class SHGameObjectsQualityController : MonoBehaviour
{
	#region Constructors
	/// <summary>
	/// Initializes a new instance of the <see cref="SHGameObjectsQualityController"/> class.
	/// </summary>
	public SHGameObjectsQualityController ()
	{
		Instance = this;
	}
	#endregion
	
	#region Editor properties
	/// <summary>
	/// The best quality devices.
	/// </summary>
	public SHDeviceGeneration[] BestQualityDevices;
	
	/// <summary>
	/// The good quality devices.
	/// </summary>
	public SHDeviceGeneration[] GoodQualityDevices;
	
	/// <summary>
	/// The best quality game objects.
	/// </summary>
	public GameObject[] BestQualityGameObjects;
	
	/// <summary>
	/// The good quality game objects.
	/// </summary>
	public GameObject[] GoodQualityGameObjects;
	
	/// <summary>
	/// The normal quality game objects.
	/// </summary>
	public GameObject[] NormalQualityGameObjects;
	#endregion
	
	#region Properties
	/// <summary>
	/// Gets the singleton instance.
	/// </summary>
	/// <value>
	/// The instance.
	/// </value>
	public static SHGameObjectsQualityController Instance { get; private set; }
	#endregion
	
	#region Life cycle
	private void Awake ()
	{	
		SHLog.Debug ("SHGameObjectsQualityController: initializing quality objects selection...");
		
		Instance = this;
		
		if (IsCurrentDeviceBestQuality)
		{
			SHLog.Debug ("SHGameObjectsQualityController: current device is a best quality device. Removing good and normal qualities game objects.");
		
			SHGameObjectHelper.Destroy (GoodQualityGameObjects);
			SHGameObjectHelper.Destroy (NormalQualityGameObjects);
		}
		else
		if (IsCurrentDeviceGoodQuality)
		{
			SHLog.Debug ("SHGameObjectsQualityController: current device is a good quality device. Removing best and normal qualities game objects.");
			
			SHGameObjectHelper.Destroy (BestQualityGameObjects);
			SHGameObjectHelper.Destroy (NormalQualityGameObjects);
		}
		else
		{
			SHLog.Debug ("SHGameObjectsQualityController: current device is a normal quality device. Removing best and good qualities game objects.");
		
			SHGameObjectHelper.Destroy (BestQualityGameObjects);
			SHGameObjectHelper.Destroy (GoodQualityGameObjects);
		}
	}
	#endregion
	
	#region Helpers
	public bool IsCurrentDeviceBestQuality {
		get {
			return IsCurrentDevicenOnList (BestQualityDevices);
		}
	}
	
	public bool IsCurrentDeviceGoodQuality {
		get {
			return IsCurrentDevicenOnList (GoodQualityDevices);
		}
	}
	
	private bool IsCurrentDevicenOnList (SHDeviceGeneration[] list)
	{
		if (list != null)
		{
			var currentGeneration = SHDevice.Generation;
			
			foreach (var i in list)
			{
				if (currentGeneration == i)
				{
					return true;
				}
			}
		}
		return false;
	}
	#endregion
}