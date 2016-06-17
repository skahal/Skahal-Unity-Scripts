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
		#region Constructors
		/// <summary>
		/// Initializes the <see cref="Skahal.Logging.SHLog"/> class.
		/// </summary>
		static SHLog ()
		{
			if (SHDebug.IsDebugBuild) {
				LogStrategy = new SHDebugLogStrategy ();
			} else {
				LogStrategy = new SHReleaseLogStrategy ();
			}
				
			Debug("SHLog: log strategy: {0}", LogStrategy.GetType());
		}
        #endregion

        #region Properties        
        /// <summary>
        /// Gets the log strategy.
        /// </summary>
        public static ISHLogStrategy LogStrategy { get; private set; }
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
			LogStrategy.Debug(message, args);
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
			LogStrategy.Warning(message, args);
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
			LogStrategy.Error(message, args);
		}
		
		/// <summary>
		/// Write an error log level.
		/// </summary>
		/// <param name='ex'>
		/// The exception about the error log level.
		/// </param>
		public static void Error (Exception ex)
		{
			LogStrategy.Error(ex);
		}
		#endregion
	}
}