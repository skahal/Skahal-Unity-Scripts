#region Usings
using System;
using System.IO;
using UnityEngine;
#endregion

[AddComponentMenu("Skahal/Movies/SHScreenshotsMovieMakerScript")]
public class SHScreenshotsMovieMakerScript : MonoBehaviour
{
	#region Editor properties
	public string RootFolderPath = "/";
	public int FrameRate = 25;
	#endregion
	
	#region Methods
	void Start()
	{
		Time.captureFramerate = FrameRate;
		RootFolderPath = Path.Combine(RootFolderPath, Guid.NewGuid().ToString());
		System.IO.Directory.CreateDirectory(RootFolderPath);
	}

	void Update ()
	{
		string name = string.Format ("{0}/{1:D04} shot.png", RootFolderPath, Time.frameCount);
		Application.CaptureScreenshot (name);
	}
	#endregion
}

