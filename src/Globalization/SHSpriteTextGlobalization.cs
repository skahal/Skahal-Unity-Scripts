using UnityEngine;
using System.Collections;
using System.Globalization;
using System;
using Skahal.Logging;
using UnityEngine.UI;

public static class SHSpriteTextGlobalization
{
	public static Func<CultureInfo, CultureInfo, string, string> GlobalizeText;
	
	public static void GlobalizeAllSceneSpriteTexts (CultureInfo fromCulture, CultureInfo toCulture)
	{
		var spriteTexts = (Text[])GameObject.FindSceneObjectsOfType (typeof(Text));
		SHLog.Debug ("SHSpriteTextGlobalization: " + spriteTexts.Length);
		
		foreach (Text t in spriteTexts)
		{
			t.text = GlobalizeText(fromCulture, toCulture, t.text);	
		}
	}
}

