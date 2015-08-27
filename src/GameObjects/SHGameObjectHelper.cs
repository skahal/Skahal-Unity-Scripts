#region Usings
using UnityEngine;
using System.Collections.Generic;
#endregion

namespace Skahal.GameObjects
{
	public static class SHGameObjectHelper
	{
		#region Methods

		public static GameObject GetNearestFrom (GameObject from, IEnumerable<GameObject> others)
		{
			Debug.Log ("GetNearestFrom: from = " + from.name);
			GameObject nearest = null;
			float lowestDistance = float.MaxValue;
		
			foreach (GameObject go in others) {
				Debug.Log ("GetNearestFrom: go = " + go.name);
			
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

		public static float GetLowestDistanceFrom (Vector3 from, IEnumerable<Vector3> others)
		{
			float lowestDistance = float.MaxValue;

			foreach (Vector3 other in others) {
				float d = Vector3.Distance (other, from);

				if (d < lowestDistance) {
					lowestDistance = d;
				}

			}

			return lowestDistance;
		}

		public static GameObject FindOrCreate (string name)
		{
			return FindOrCreate (name, Vector3.zero);
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
		#endregion
	}
}