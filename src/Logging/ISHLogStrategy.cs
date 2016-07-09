using System;

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
		void Debug(string message, params object[] args);
		
		/// <summary>
		/// Writes the warning log level message.
		/// </summary>
		/// <param name='message'>
		/// Message.
		/// </param>
		/// <param name='args'>
		/// Arguments.
		/// </param>
		void Warning(string message, params object[] args);
		
		/// <summary>
		/// Writes the error log level message.
		/// </summary>
		/// <param name='message'>
		/// Message.
		/// </param>
		/// <param name='args'>
		/// Arguments.
		/// </param>
		void Error(string message, params object[] args);
		
		/// <summary>
		/// Writes the error log level message.
		/// </summary>
		/// <param name='ex'>
		/// Exception.
		/// </param>
		void Error(Exception ex);
		#endregion
	}
}