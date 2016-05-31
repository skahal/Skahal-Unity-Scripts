#region Usings
using System;
using UnityEngine;
#endregion

namespace Skahal.Effects
{
	/// <summary>
	/// Wireframe effect service.
	/// </summary>
	public static class WireframeEffectService
	{
		/// <summary>
		///  Applies to all scene game objects with MeshFilters the WireframeEffectController
		/// </summary>
		/// <param name='initializeControllerAction'>
		/// Initialize controller action.
		/// </param>
		public static MeshFilter[] ApplyToAllSceneMeshFilters (Action<WireframeEffectController> initializeControllerAction)
		{
			var gos = (MeshFilter[])GameObject.FindObjectsOfType (typeof(MeshFilter));
	
			foreach (var g in gos) {
				var controller = g.gameObject.AddComponent<WireframeEffectController> ();
				initializeControllerAction (controller);
				
				if (controller.TargetCamera == null) {
					controller.TargetCamera = UnityEngine.Camera.main;
				}
			}
			
			return gos;
		}
		
		public static void Unapply (MeshFilter[] meshFilters)
		{
			foreach (var g in meshFilters) {
				Component.DestroyImmediate(g.gameObject.GetComponent<WireframeEffectController> ());
			}
		}
	}
}