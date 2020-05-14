using UnityEngine;
 

public static class GUITextureHelper
{
	public static void SetColorAlpha(UnityEngine.UI.Image guiTexture, float alpha)
	{
		guiTexture.color = ColorHelper.New(guiTexture.color, alpha);
	}
}

