#region Usings
using UnityEngine;
using System.Collections.Generic;
using Skahal.Logging;
#endregion

public static class SHGameObjectHelper 
{	
	#region GetNearestFrom
	public static GameObject GetNearestFrom (GameObject from, IEnumerable<GameObject> others)
	{
		SHLog.Debug ("GetNearestFrom: from = " + from.name);
		GameObject nearest = null;
		float lowestDistance = float.MaxValue;
		
		foreach (GameObject go in others) {
			SHLog.Debug ("GetNearestFrom: go = " + go.name);
			
			if (go.GetInstanceID () != from.GetInstanceID ()) {
				float d = Vector3.Distance (go.transform.position, from.transform.position);
				
				if (d < lowestDistance) {
					lowestDistance = d;
					nearest = go;
				}
			}
		}
		
		return nearest;
	}
	#endregion
	
	public static GameObject FindOrCreate (string name)
	{
		return FindOrCreate(name, Vector3.zero);
	}
	
	public static GameObject FindOrCreate (string name, Vector3 position)
	{
		var go = GameObject.Find (name);
		
		if (go == null) {
			go = new GameObject (name);
			go.transform.position = position;
		}
		
		return go;
	}
	
	public static void Active (GameObject[] gos, bool includeChildren = false)
	{
		foreach (GameObject g in gos)
		{
			if (g != null)
			{
				if (includeChildren)
				{
					g.SetActiveRecursively (true);
				}
				else
				{	
					g.active = true;
				}
			}	
		}
	}
	
	public static void Deactive (GameObject[] gos, bool includeChildren = false)
	{
		foreach (GameObject g in gos)
		{
			if (g != null)
			{
				if (includeChildren)
				{
					g.SetActiveRecursively (false);
				}
				else
				{	
					g.active = false;
				}
			}
		}
	}
	
	public static void Destroy (GameObject[] gos)
	{
		if (gos != null)
		{
			foreach (GameObject g in gos)
			{
				GameObject.Destroy (g);
			}
		}
	}
}
