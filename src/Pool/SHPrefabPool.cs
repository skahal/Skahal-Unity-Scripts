#region Usings
using UnityEngine;
using System.Collections;
#endregion

/// <summary>
/// Implements a classic pool for game objects derived froma prefab.
/// </summary>
public class SHPrefabPool : SHPoolBase
{
   #region Fields
   private Object m_prefab;
   #endregion

   #region Constructors
   /// <summary>
   /// Initializes a new instance of the <see cref="SHPrefabPool"/> class.
   /// </summary>
   /// <param name='prefabName'>
   /// Prefab name (the prefab should be in a folder named ˝Resources˝.
   /// </param>
   public SHPrefabPool (string prefabName)
   {
      PrefabName = prefabName;
   }

   /// <summary>
   /// Initializes a new instance of the <see cref="SHPrefabPool"/> class.
   /// </summary>
   public SHPrefabPool ()
   {

   }
   #endregion

   #region Editor properties
   public string PrefabName;
   #endregion

   #region Methods
   /// <summary>
   /// Awake this instance.
   /// </summary>
   private void Awake ()
   {
      m_prefab = Resources.Load (PrefabName);
   }
   #endregion

   #region implemented abstract members of SHPoolBase
   /// <summary>
   /// Creates the object.
   /// </summary>
   /// <returns>
   /// The object.
   /// </returns>
   protected override GameObject CreateObject ()
   {
      var go = (GameObject)Object.Instantiate (m_prefab, Vector3.zero, Quaternion.identity);
      return go;
   }

   /// <summary>
   /// Disables the GameObject.
   /// </summary>
   /// <param name='goInPool'>
   /// GameObject in pool.
   /// </param>
   protected override void DisableObject (GameObject goInPool)
   {
      goInPool.SetActiveRecursively(false);
   }

   /// <summary>
   /// Enables the GameObject.
   /// </summary>
   /// <param name='goInPool'>
   /// GameObject in pool.
   /// </param>
   protected override void EnableObject (GameObject goInPool)
   {
      goInPool.SetActiveRecursively(true);
   }

   /// <summary>
   /// Determines whether the specified GameObject is enabled..
   /// </summary>
   /// <returns>
   /// <c>true</c> if the GameObject is enabled; otherwise, <c>false</c>.
   /// </returns>
   /// <param name='goInPool'>
   /// The GameObject in pool.
   /// </param>
   protected override bool IsObjectEnabled (GameObject goInPool)
   {
      return goInPool.active;
   }
   #endregion
}

