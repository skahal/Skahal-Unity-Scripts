#region Usings
using System;
using UnityEngine;
#endregion

/// <summary>
/// A central point to organize error handling
/// </summary>
public static class SHError
{
	#region Methods
	/// <summary>
	/// Throws a error with specified message.
	/// </summary>
	/// <param name='message'>
	/// Message.
	/// </param>
	/// <param name='args'>
	/// Arguments.
	/// </param>
	public static void ThrowError(string message, params object[] args)
	{
		Debug.LogError(String.Format(message, args));
	}
	#endregion
}

