#region Usings
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
using Skahal.Logging;
#endregion

/// <summary>
/// A base class for game object pools
/// </summary>
public abstract class SHPoolBase : MonoBehaviour
{
	#region Fields
	private List<GameObject> m_gameObjects;
	private Transform m_container;
	#endregion

   #region Constructors
   void Start()
   {
   }
   #endregion

	#region Editor properties
   /// <summary>
   /// The name.
   /// </summary>
	public string Name;

   /// <summary>
   /// The size.
   /// </summary>
	public int Size = 10;

   /// <summary>
   /// The size is fixed?
   /// </summary>
   public bool IsSizeFixed = false;

   /// <summary>
   /// The auto disable time.
   /// <remarks>
   /// If value is diff than zero,
   /// then the object will be put back to the pool in seconds defined in the value.
   /// </remarks>
   /// </summary>
	public float AutoDisableTime = 0;

   /// <summary>
   /// If should raise OnGameObjectEnabledInPool message.
   /// </summary>
	public bool RaiseOnGameObjectEnabledInPoolMessage = false;

   /// <summary>
   /// If should raise OnGameObjectDisabledInPool message.
   /// </summary>
	public bool RaiseOnGameObjectDisabledInPoolMessage = false;
   #endregion

   #region Properties
   /// <summary>
   /// Gets the game objects count.
   /// </summary>
   /// <value>
   /// The game objects count.
   /// </value>
   public int GameObjectsCount
   {
      get
      {
         return m_gameObjects.Count;
      }
   }
	#endregion
	
	#region Internals
   /// <summary>
   /// Creates the initial objects on the pool.
   /// </summary>
   /// <returns>
   /// The objects.
   /// </returns>
	public IEnumerator CreateObjects()
	{
		m_gameObjects = new List<GameObject>(Size);
		
		for (int i = 0; i < Size; i++)
		{
			AddGameObject();
		}
		
		yield return new WaitForSeconds(0);
	}

   /// <summary>
   /// Adds a game object to the pool.
   /// </summary>
   /// <returns>
   /// The game object added.
   /// </returns>
	private GameObject AddGameObject ()
   {
      GameObject go = CreateObject ();
      go.transform.parent = m_container;
      ReleaseGameObject (go);
      m_gameObjects.Add (go);

      m_container.gameObject.name = String.Format ("{0} ({1})", Name, m_gameObjects.Count);
		
      return go;
   }

   /// <summary>
   /// Gets the game object from the pool.
   /// </summary>
   /// <returns>
   /// The game object.
   /// </returns>
   /// <param name='position'>
   /// Position.
   /// </param>
	public GameObject GetGameObject (Vector3 position)
   {
      int length = m_gameObjects.Count;
      GameObject go = null;
		
      for (int i = 0; i < length; i++)
      {
         go = m_gameObjects [i];

         if (go == null)
         {
            SHLog.Error ("{0} - GameObject on index {1} is null. You should not call Destroy() in objects that are in a pool.", GetType ().Name, i);
         }

         if (IsObjectEnabled (go))
         {
            go = null;
         } else
         {
            break;
         }
      }
		
      if (go == null)
      {
         if (IsSizeFixed)
         {
            go = m_gameObjects [0];
            m_gameObjects.RemoveAt (0);
            m_gameObjects.Add (go);
         } else
         {
            go = AddGameObject ();
         }
      }
		
      EnableGameObject (go, position);
		
      return go;
   }

   /// <summary>
   /// Enables the game object.
   /// </summary>
   /// <param name='go'>
   /// Go.
   /// </param>
   /// <param name='position'>
   /// Position.
   /// </param>
	private void EnableGameObject (GameObject go, Vector3 position)
	{
		go.transform.position = position;
		EnableObject (go);
		go.name = Name + " (IN USE)";
		
		if (RaiseOnGameObjectEnabledInPoolMessage)
		{
			Messenger.Send ("OnGameObjectEnabledInPool", go);
		}
		
		if (AutoDisableTime > 0)
		{
			StartCoroutine(AutoDisable(go, AutoDisableTime));
		}
	}

   /// <summary>
   /// Auto the disable the game object when delay time is reached.
   /// </summary>
   /// <param name='go'>
   /// Go.
   /// </param>
   /// <param name='delay'>
   /// Delay.
   /// </param>
	private IEnumerator AutoDisable(GameObject go, float delay)
	{
		yield return new WaitForSeconds(delay);
		ReleaseGameObject(go);
	}

   /// <summary>
   /// Releases the game object to the pool with a delay.
   /// </summary>
   /// <param name='go'>
   /// Go.
   /// </param>
   /// <param name='delay'>
   /// Delay.
   /// </param>
	public void ReleaseGameObject(GameObject go, float delay)
	{
		StartCoroutine(AutoDisable(go, delay));
	}

   /// <summary>
   /// Releases the game object to the pool.
   /// </summary>
   /// <param name='go'>
   /// Go.
   /// </param>
	public void ReleaseGameObject (GameObject go)
	{
		lock (this)
		{
			DisableObject (go);
			go.name = Name + " (FREE)";
			
			if (RaiseOnGameObjectDisabledInPoolMessage)
			{
				Messenger.Send ("OnGameObjectDisabledInPool", go);
			}
		}
	}

   /// <summary>
   /// Releases all game objects that fit in release filter specified.
   /// </summary>
   /// <param name='releaseFilter'>
   /// Release filter.
   /// </param>
   public void ReleaseAll (Func<GameObject, bool> releaseFilter)
   {
      foreach (var go in m_gameObjects)
      {
         if (releaseFilter (go))
         {
            ReleaseGameObject(go);
         }
      }
   }

   /// <summary>
   /// Sets the container.
   /// </summary>
   /// <param name='container'>
   /// Container.
   /// </param>
	public void SetContainer(Transform container)
	{
		m_container = container;
	}
	#endregion
	
	#region Implements in every pool
   /// <summary>
   /// Creates the object.
   /// </summary>
   /// <returns>
   /// The object.
   /// </returns>
	protected abstract GameObject CreateObject();

   /// <summary>
   /// Disables the object.
   /// </summary>
   protected abstract void DisableObject(GameObject goInPool);

   /// <summary>
   /// Enables the object.
   /// </summary>
   protected abstract void EnableObject(GameObject goInPool);

   /// <summary>
   /// Determines whether specified object is enabled.
   /// </summary>
   protected abstract bool IsObjectEnabled(GameObject goInPool);
	#endregion
}

