#region Usings
using System;
#endregion

namespace Skahal.Logging
{
	/// <summary>
	/// An ISHLogStrategy that add a prefix text to every log message of an underlying log strategy.
	/// </summary>
	public sealed class PrefixedLogStrategy : ISHLogStrategy
	{
        #region Fields
        private ISHLogStrategy m_underlyingLogStrategy;
        private string m_prefix;
        #endregion

        #region Constructors
        public PrefixedLogStrategy(ISHLogStrategy underlyingLogStrategy, string prefix)
        {
            if (underlyingLogStrategy == null)
            {
                throw new ArgumentNullException("underlyingLogStrategy");
            }

            m_underlyingLogStrategy = underlyingLogStrategy;
            m_prefix = prefix;
        }
        #endregion

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
        public void Debug (string message, params object[] args)
		{
			m_underlyingLogStrategy.Debug (BuildMessage(message), args);
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
			m_underlyingLogStrategy.Warning(BuildMessage(message), args);            
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
			m_underlyingLogStrategy.Error(BuildMessage(message), args);
		}
		
		/// <summary>
		/// Writes the error log level message.
		/// </summary>
		/// <param name='ex'>
		/// Exception.
		/// </param>
		public void Error(Exception ex)
		{
			Error (ex.Message);
		}

        private string BuildMessage(string message)
        {
			return  "{0}{1}".With(m_prefix, message);
        }
		#endregion
	}
}