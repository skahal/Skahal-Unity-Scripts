using UnityEngine;
 

public static class GUITextureHelper
{
	public static void SetColorAlpha(GUITexture guiTexture, float alpha)
	{
		guiTexture.color = ColorHelper.New(guiTexture.color, alpha);
	}
}

