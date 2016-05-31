using UnityEngine;
 
using System;

public static class SHRaycastHit
{
	public static RaycastHit FindByTag(RaycastHit[] hits, string tag)
	{
		var length = hits.Length;
		
		for (var i = 0; i < length; i++)
		{
			var hit = hits[i];
			
			if (tag.Equals(hit.transform.tag, StringComparison.OrdinalIgnoreCase))
			{
				return hit;
			}
		}
		
		return new RaycastHit();
	}
	
	public static bool ContainsTag(RaycastHit[] hits, string tag)
	{
		var length = hits.Length;
		
		for (var i = 0; i < length; i++)
		{
			var hit = hits[i];
			
			if (tag.Equals(hit.transform.tag, StringComparison.OrdinalIgnoreCase))
			{
				return true;
			}
		}
		
		return false;
	}
}

