#region Usings
using UnityEngine;
#endregion

namespace UnityEngine
{
	/// <summary>
	/// Collider extensions.
	/// </summary>
	public static class ColliderExtensions
	{
		/// <summary>
		/// Determines whether this instance is visible from the specified camera.
		/// </summary>
		/// <returns>
		/// <c>true</c> if this instance is visible from the specified camera; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsVisibleFrom (this Collider collider, UnityEngine.Camera camera)
		{
			Plane[] planes = GeometryUtility.CalculateFrustumPlanes (camera);

			return GeometryUtility.TestPlanesAABB (planes, collider.bounds);
		}
	}
}