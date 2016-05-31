#region Usings
using UnityEngine;
#endregion

namespace Skahal.Rendering 
{
	/// <summary>
	/// Renderer extensions.
	/// </summary>
	public static class RendererExtensions
	{
		/// <summary>
		/// Determines whether this instance is visible from the specified camera.
		/// </summary>
		/// <returns>
		/// <c>true</c> if this instance is visible from the specified camera; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsVisibleFrom (this Renderer renderer, UnityEngine.Camera camera)
		{
			Plane[] planes = GeometryUtility.CalculateFrustumPlanes (camera);
			
			return GeometryUtility.TestPlanesAABB (planes, renderer.bounds);
		}
	}
}