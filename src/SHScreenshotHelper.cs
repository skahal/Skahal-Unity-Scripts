#region Usings
using UnityEngine;
using System.Collections;
using System.IO;
using Skahal.Logging;
using System;
using Skahal.IO;
using Skahal.Threading;


#endregion

/// <summary>
/// Screenshot helper.
/// </summary>
public class SHScreenshotHelper : MonoBehaviour
{
	/// <summary>
	/// Takes the screenshot from screen.
	/// </summary>
	public static void TakeScreenshot (Action<string>screenshotTakenCallback)
	{
		var fileName = Path.Combine (Application.persistentDataPath, "SHScreenshotHelper_TakeScreenshot.png");
		SHFileHelper.DeleteIfExists (fileName);
		
		Application.CaptureScreenshot (fileName);
		
		SHLog.Debug ("SHScreenshotHelper.TakeScreenshot: {0}", fileName);
		
		SHThread.WaitFor (
		() => 
		{
			return File.Exists (fileName);	
		},
		() =>
		{
			screenshotTakenCallback (fileName);
		}
		);
	}
	
	/// <summary>
	/// Takes the screenshot from screen.
	/// </summary>
	public static void TakeScreenshot (Action<Texture2D>screenshotTakenCallback)
	{
		TakeScreenshot ((string fileName) =>
		{
			fileName = string.Format ("file://{0}", fileName);
		
			var www = new WWW (fileName);
			var screenshot = new Texture2D (System.Convert.ToInt32 (SHScreen.Width), System.Convert.ToInt32 (SHScreen.Height));
			
			SHThread.WaitFor (
			() => 
			{
				return www.isDone;	
			},
			() =>
			{
				www.LoadImageIntoTexture (screenshot);			
				screenshotTakenCallback (screenshot);
			}
			);
		});
	}
}