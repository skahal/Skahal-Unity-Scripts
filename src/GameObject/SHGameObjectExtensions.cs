#region Usings
using System;
using UnityEngine;
#endregion

namespace Skahal.GameObjects
{
	/// <summary>
	/// Extensions for GameObject.
	/// </summary>
	public static class SHGameObjectExtensions
	{	
		/// <summary>
		/// Destroies the component.
		/// </summary>
		/// <param name='go'>
		/// Go.
		/// </param>
		/// <param name='componentType'>
		/// Component type.
		/// </param>
		public static void DestroyComponent (this GameObject go, Type componentType)
		{
			var component = go.GetComponent (componentType.Name);
			
			if (component != null)
			{
				Component.Destroy (component);
			}
		}
		
		/// <summary>
		/// Destroies the component.
		/// </summary>
		/// <param name='go'>
		/// Go.
		/// </param>
		/// <param name='component'>
		/// Component.
		/// </param>
		public static void DestroyComponent (this GameObject go, object component)
		{
			if (component != null)
			{
				DestroyComponent(go, component.GetType());
			}
		}
	}
}