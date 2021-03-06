#region Usings
using System;

#endregion

namespace Skahal.Logging
{
	/// <summary>
	/// The ISHLogStrategy use when the games goes live in a release build.
	/// <remarks>Logs only erros.</remarks>
	/// </summary>
	public sealed class SHReleaseLogStrategy : ISHLogStrategy
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
		public void Debug (string message, params object[] args)
		{
			// Should not log warning messages in a release build.
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
		public void Warning (string message, params object[] args)
		{
			// Should not log warning messages in a release build.
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
		public void Error (string message, params object[] args)
		{
			UnityEngine.Debug.LogError ("[ERROR]" + String.Format (message, args));
		}
		
		/// <summary>
		/// Writes the error log level message.
		/// </summary>
		/// <param name='ex'>
		/// Exception.
		/// </param>
		public void Error (Exception ex)
		{
			Error ("[ERROR] {0}", ex.Message);
		}
		#endregion
	}
}