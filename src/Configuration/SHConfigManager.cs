#region Usings
using Skahal.Common;
using UnityEngine;
using System.Collections.Generic;
using Skahal.Logging;
#endregion

/// <summary>
/// Manages the global configs for the game.
/// </summary>
[AddComponentMenu("Skahal/Configuration/SHConfigManager")]
public class SHConfigManager : MonoBehaviour
{
	#region Fields
	private static Dictionary<string, string> s_settings;
	#endregion
		
	#region Editor properties
	/// <summary>
	/// Gets or sets the game settings.
	/// </summary>
	public SHKeyValue[] Settings;
	#endregion
	
	#region Public Methods
	/// <summary>
	/// Gets a setting from SHConfigManager.Properties.
	/// </summary>
	/// <returns>
	/// The setting.
	/// </returns>
	/// <param name='key'>
	/// Key.
	/// </param>
	public static string GetSetting (string key)
	{
		ValidateSetup ();
		
		if (!s_settings.ContainsKey (key))
		{
			SHError.ThrowError ("SHConfigManager: The key {0} was not found on Settings.", key);
		}
		
		return s_settings [key];
	}
	
	/// <summary>
	/// Determines whether the Setting with specified keys is equal the expected value.
	/// </summary>
	/// <returns>
	/// <c>true</c> if the key's value is equal than expectedValue; otherwise, <c>false</c>.;
	/// </returns>
	/// <param name='key'>
	/// If set to <c>true</c> key.
	/// </param>
	/// <param name='expectedValue'>
	/// If set to <c>true</c> expected value.
	/// </param>
	public static bool Is (string key, string expectedValue)
	{
		return GetSetting(key).Equals(expectedValue);
	}
	#endregion
	
	#region Private Methods
	private void Awake ()
	{
		DontDestroyOnLoad (this);	
		s_settings = Settings.ToDictionary ();
		
		SHLog.Debug ("SHConfigManager: {0} settings available.", s_settings.Count);
		
		foreach (var s in s_settings)
		{
			SHLog.Debug ("\t{0} = {1}", s.Key, s.Value);
		}
	}
	
	/// <summary>
	/// Validates the SHConfigManagers's basic setup to work well.
	/// </summary>
	private static void ValidateSetup ()
	{
		if (s_settings == null)
		{
			Debug.LogError("SHConfigManager: put SHConfigManager as a component in a GameObject befor call any method from SHConfigManager.");
		}
	}
	#endregion
}