using UnityEngine;
 

public static class MaterialHelper
{
	public static void SetColorAlpha(Material material, float alpha)
	{
		var c = material.color;
		c.a = alpha;
		material.color = c;
	}
}

