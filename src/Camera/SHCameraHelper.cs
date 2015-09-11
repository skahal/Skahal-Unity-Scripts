#region Usings
using UnityEngine;
using System.Collections;
using Skahal.Tweening;


#endregion

namespace Skahal.Camera
{
	/// <summary>
	///  Camera helpers methods.
	/// </summary>
	public static class SHCameraHelper
	{
		/// <summary>
		/// Shake the camera with time and amount specified.
		/// </summary>
		/// <param name='camera'>
		/// Camera.
		/// </param>
		/// <param name='time'>
		/// Time.
		/// </param>
		/// <param name='amount'>
		/// Amount.
		/// </param>
		public static void Shake (UnityEngine.Camera camera, float time, Vector3 amount)
		{
			iTweenHelper.ShakeRotation (camera.gameObject, 
				iT.ShakeRotation.amount, amount,
				iT.ShakeRotation.time, time);
		}

		/// <summary>
		/// Shake the main camera with time and amount specified.
		/// </summary>
		/// <param name='time'>
		/// Time.
		/// </param>
		/// <param name='amount'>
		/// Amount.
		/// </param>
		public static void Shake (float time, Vector3 amount)
		{
			Shake (UnityEngine.Camera.main, time, amount);
		}
		
		/// <summary>
		/// Shake the main camera.
		/// </summary>
		public static void Shake ()
		{
			Shake(UnityEngine.Camera.main, 2f, new Vector3 (1f, 1f, 1f));
		}

		/// <summary>
		/// Gets the camera left border in world position
		/// </summary>
		/// <returns>The left border.</returns>
		/// <param name="camera">Camera.</param>
		public static float GetLeftBorder(UnityEngine.Camera camera = null) 
		{
			camera = camera ?? UnityEngine.Camera.main;

			var dist = (Vector3.zero - camera.transform.position).z;
			return camera.ViewportToWorldPoint(new Vector3(0,0,dist)).x;
		}

		/// <summary>
		/// Gets the camera right border in world position
		/// </summary>
		/// <returns>The left border.</returns>
		/// <param name="camera">Camera.</param>
		public static float GetRightBorder(UnityEngine.Camera camera = null) 
		{
			camera = camera ?? UnityEngine.Camera.main;

			var dist = (Vector3.zero - camera.transform.position).z;
			return camera.ViewportToWorldPoint(new Vector3(1,0,dist)).x;
		}
	}
}