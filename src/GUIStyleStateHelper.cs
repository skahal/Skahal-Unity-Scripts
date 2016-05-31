using UnityEngine;
 

public static class GUIStyleStateHelper
{
	public static void SetTextColorAlpha(GUIStyleState guiStyleState, float alpha)
	{
		var c = guiStyleState.textColor;
		c.a = alpha;
		guiStyleState.textColor = c;
	}
}

