#region Usings
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;
#endregion

/// <summary>
/// A manager for game objects pools.
/// </summary>
[AddComponentMenu("Skahal/Pool/SHPoolsManager")]
public class SHPoolsManager : MonoBehaviour
{	
	#region Fields
	private static SHPoolsManager s_instance;
	private Dictionary<string, SHPoolBase> m_pools = new Dictionary<string, SHPoolBase>();
	#endregion

   #region Constructors
   public SHPoolsManager ()
   {
      s_instance = this;
   }
   #endregion

   #region Properties
   /// <summary>
   /// Gets the pools.
   /// </summary>
   /// <value>
   /// The pools.
   /// </value>
   public static IList<SHPoolBase> Pools
   {
      get
      {
         return s_instance.m_pools.Values.ToList().AsReadOnly();
      }
   }
   #endregion

   #region Life cycle and pools methods
	private void Start ()
   {
      SHPoolBase[] pools = this.GetComponentsInChildren<SHPoolBase> ();
		
      foreach (SHPoolBase p in pools)
      {
         var goName = p.Name + " pool";

         Transform container = transform.FindChild (goName);

         if (container == null)
         {
            container = new GameObject (goName).transform;
            container.parent = s_instance.transform;
         }

         p.SetContainer (container);
         m_pools.Add (p.Name, p);
         StartCoroutine (p.CreateObjects ());
      }
   }
	
	public static GameObject GetGameObject(string poolName)
	{
		return GetGameObject(poolName, Vector3.zero);
	}
	
	public static GameObject GetGameObject(string poolName, Vector3 position)
	{
		return s_instance.m_pools[poolName].GetGameObject(position);
	}
	
	public static void ReleaseGameObject (string poolName, GameObject go)
   {
      if (s_instance.m_pools.ContainsKey (poolName))
      {
         s_instance.m_pools [poolName].ReleaseGameObject (go);
      }
   }
	
	public static void ReleaseGameObject (string poolName, GameObject go, float delay)
   {
      s_instance.m_pools [poolName].ReleaseGameObject (go, delay);
   }

   /// <summary>
   /// Releases all game objects in all pools that fit in release filter specified.
   /// </summary>
   /// <param name='releaseFilter'>
   /// Release filter.
   /// </param>
   public static void ReleaseAll (Func<GameObject, bool> releaseFilter)
   {
      foreach (var p in s_instance.m_pools)
      {
         p.Value.ReleaseAll(releaseFilter);
      }
   }
   #endregion
}