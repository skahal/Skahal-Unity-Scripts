#region Usings
using UnityEngine;
using System.Collections;
using Skahal.Logging;
#endregion

/// <summary>
/// Central point about general game's information.
/// </summary>
[AddComponentMenu("Skahal/Common/SHGameInfo")]
public class SHGameInfo : MonoBehaviour
{
	#region Fields
	private static SHGameInfo s_instance;
	#endregion
	
	#region Editor properties
	/// <summary>
	/// The game's version.
	/// </summary>
	public string m_version;
	
	/// <summary>
	/// Is is a beta version.
	/// </summary>
	public bool m_isBetaVersion;
	
	/// <summary>
	/// The multplayer's version.
	/// </summary>
	public string m_multiplayerVersion;
	#endregion
	
	#region Static properties
	/// <summary>
	/// Gets the game's version.
	/// </summary>
	public static string Version {
		get {
			ValidateState ();
			return s_instance.m_version;
		}
	}
	
	/// <summary>
	/// Gets the if is a beta version.
	/// </summary>
	public static bool IsBetaVersion {
		get {
			ValidateState ();
			return s_instance.m_isBetaVersion;
		}
	}
	
	/// <summary>
	/// Gets the multiplayer's version.
	/// </summary>
	public static string MultiplayerVersion {
		get {
			ValidateState ();
			return s_instance.m_multiplayerVersion;
		}
	}

	#endregion
	
	#region Methods
	private void Awake ()
	{
		DontDestroyOnLoad (this);
		s_instance = this;
	}
	
	/// <summary>
	/// Validates the state.
	/// </summary>
	private static void ValidateState ()
	{
		if (s_instance == null)
		{
			SHLog.Error("SHGameInfo can't be used before the script be inserted on a game object in the first loaded scene.");
		}
	}
	#endregion
}