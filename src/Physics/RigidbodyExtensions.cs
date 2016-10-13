using System;

namespace UnityEngine
{
	/// <summary>
	/// Rigidbody extension methods.
	/// </summary>
	public static class RigidbodyExtensions
	{
		/// <summary>
		/// Resets the rigidbody.
		/// </summary>
		/// <param name="rb">Rigidbody.</param>
		public static void Reset(this Rigidbody rb)
		{
			rb.velocity = Vector3.zero;
			rb.angularVelocity = Vector3.zero;
			rb.rotation = Quaternion.identity;
		}
	}
}
