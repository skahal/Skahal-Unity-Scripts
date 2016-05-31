#region Usings
using System;
#endregion

namespace Skahal.Logging
{
	/// <summary>
	/// The ISHLogStrategy use during developement that use UnityEngine.Debug class to write logs.
	/// <remarks>Logs everythings.</remarks>
	/// </summary>
	public sealed class SHDebugLogStrategy : ISHLogStrategy
	{
		#region ISHLogStrategy implementation
		/// <summary>
		/// Writes the debug log level message.
		/// </summary>
		/// <param name='message'>
		/// Message.
		/// </param>
		/// <param name='args'>
		/// Arguments.
		/// </param>
		public void WriteDebug (string message, params object[] args)
		{
			UnityEngine.Debug.Log ("[DEBUG]" + String.Format (message, args));
		}
		
		/// <summary>
		/// Writes the warning log level message.
		/// </summary>
		/// <param name='message'>
		/// Message.
		/// </param>
		/// <param name='args'>
		/// Arguments.
		/// </param>
		public void WriteWarning (string message, params object[] args)
		{
			UnityEngine.Debug.LogWarning ("[WARNING]" + String.Format (message, args));
		}
		
		/// <summary>
		/// Writes the error log level message.
		/// </summary>
		/// <param name='message'>
		/// Message.
		/// </param>
		/// <param name='args'>
		/// Arguments.
		/// </param>
		public void WriteError (string message, params object[] args)
		{
			UnityEngine.Debug.LogError ("[ERROR]" + String.Format (message, args));
		}
		
		/// <summary>
		/// Writes the error log level message.
		/// </summary>
		/// <param name='ex'>
		/// Exception.
		/// </param>
		public void WriteError(Exception ex)
		{
			WriteError ("[ERROR] {0}", ex.Message);
		}
		#endregion
	}
}