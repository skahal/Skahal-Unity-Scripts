#region Usings
using UnityEngine;
using System.Collections;
#endregion

/// <summary>
/// Extensions methods for Layers
/// </summary>
public static class SHLayerExtensions
{
	/// <summary>
	/// Sets the layer.
	/// </summary>
	/// <param name='go'>
	/// Go.
	/// </param>
	/// <param name='layerName'>
	/// Layer name.
	/// </param>
	public static void SetLayer (this GameObject go, string layerName)
	{
		go.layer = LayerMask.NameToLayer (layerName);
	}
	
	/// <summary>
	/// Sets the layer recursively.
	/// </summary>
	/// <param name='go'>
	/// Go.
	/// </param>
	/// <param name='layerName'>
	/// Layer name.
	/// </param>
	public static void SetLayerRecursively (this GameObject go, string layerName)
	{
		go.SetLayer(layerName);
		var transform = go.transform;
		var childCount = transform.childCount;
		
		for (int i = 0; i < childCount; i++)
		{
			transform.GetChild (i).gameObject.SetLayerRecursively (layerName);
		}
	}
}