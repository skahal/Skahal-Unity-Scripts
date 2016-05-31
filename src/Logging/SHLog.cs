#region Usings
using UnityEngine;
using System.Collections;
using System;
using Skahal.Debugging;
#endregion

namespace Skahal.Logging
{
	/// <summary>
	/// A central point to organize logs.
	/// </summary>
	public static class SHLog
	{
		#region Fields
		/// <summary>
		/// The log strategy.
		/// </summary>
		private static ISHLogStrategy s_logStrategy;
		#endregion
		
		#region Constructors
		/// <summary>
		/// Initializes the <see cref="Skahal.Logging.SHLog"/> class.
		/// </summary>
		static SHLog ()
		{
			if (SHDebug.IsDebugBuild) {
				s_logStrategy = new SHDebugLogStrategy ();
			} else {
				s_logStrategy = new SHReleaseLogStrategy ();
			}
				
			Debug("SHLog: log strategy: {0}", s_logStrategy.GetType());
		}
		#endregion
		
		#region Methods
		/// <summary>
		/// Write a debug log level.
		/// </summary>
		/// <param name='message'>
		/// Message.
		/// </param>
		/// <param name='args'>
		/// Arguments.
		/// </param>
		public static void Debug (string message, params object[] args)
		{
			s_logStrategy.WriteDebug(message, args);
		}
		
		/// <summary>
		/// Write a warning log level.
		/// </summary>
		/// <param name='message'>
		/// Message.
		/// </param>
		/// <param name='args'>
		/// Arguments.
		/// </param>
		public static void Warning (string message, params object[] args)
		{
			s_logStrategy.WriteWarning(message, args);
		}
		
		/// <summary>
		/// Write an error log level.
		/// </summary>
		/// <param name='message'>
		/// Message.
		/// </param>
		/// <param name='args'>
		/// Arguments.
		/// </param>
		public static void Error (string message, params object[] args)
		{
			s_logStrategy.WriteError(message, args);
		}
		
		/// <summary>
		/// Write an error log level.
		/// </summary>
		/// <param name='ex'>
		/// The exception about the error log level.
		/// </param>
		public static void Error (Exception ex)
		{
			s_logStrategy.WriteError(ex);
		}
		#endregion
	}
}