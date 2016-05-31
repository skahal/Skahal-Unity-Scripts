#region Usings
using UnityEngine;
#endregion

public static class ColorHelper
{
	public static Color New(Color c, float alpha)
	{
		return new Color(c.r, c.g, c.b, alpha);
	}
}

