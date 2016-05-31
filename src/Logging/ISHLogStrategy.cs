#region Usings
using System;
#endregion

namespace Skahal.Logging
{
	/// <summary>
	/// Defines a interface for log strategies.
	/// </summary>
	public interface ISHLogStrategy
	{
		#region Methods
		/// <summary>
		/// Writes the debug log level message.
		/// </summary>
		/// <param name='message'>
		/// Message.
		/// </param>
		/// <param name='args'>
		/// Arguments.
		/// </param>
		void WriteDebug(string message, params object[] args);
		
		/// <summary>
		/// Writes the warning log level message.
		/// </summary>
		/// <param name='message'>
		/// Message.
		/// </param>
		/// <param name='args'>
		/// Arguments.
		/// </param>
		void WriteWarning(string message, params object[] args);
		
		/// <summary>
		/// Writes the error log level message.
		/// </summary>
		/// <param name='message'>
		/// Message.
		/// </param>
		/// <param name='args'>
		/// Arguments.
		/// </param>
		void WriteError(string message, params object[] args);
		
		/// <summary>
		/// Writes the error log level message.
		/// </summary>
		/// <param name='ex'>
		/// Exception.
		/// </param>
		void WriteError(Exception ex);
		#endregion
	}
}