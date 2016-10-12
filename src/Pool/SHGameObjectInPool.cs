using System;
using System.Collections;
using UnityEngine;

public class SHGameObjectInPool : MonoBehaviour
{
	int _usageNumber;

	/// <summary>
	/// Auto the disable the game object when delay time is reached.
	/// </summary>
	/// <param name='go'>
	/// Go.
	/// </param>
	/// <param name='delay'>
	/// Delay.
	/// </param>
	private IEnumerator AutoDisable(float delay, Action<GameObject> releaseGameObject)
	{
		yield return new WaitForSeconds(delay);
		releaseGameObject(gameObject);
	}

	public void StartAutoDisable(float delay, Action<GameObject> releaseGameObject)
	{
		StartCoroutine(AutoDisable(delay, releaseGameObject));
	}

	public void CancelAutoDisable()
	{
		StopCoroutine("AutoDisable");
	}
}
