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
        public void Debug(string message, params object[] args)
        {
            UnityEngine.Debug.Log(BuildMessage("DEBUG", message, args));
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
        public void Warning(string message, params object[] args)
        {
            UnityEngine.Debug.LogWarning(BuildMessage("WARNING", message, args));
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
        public void Error(string message, params object[] args)
        {
            UnityEngine.Debug.LogError(BuildMessage("ERROR", message, args));
        }

        /// <summary>
        /// Writes the error log level message.
        /// </summary>
        /// <param name='ex'>
        /// Exception.
        /// </param>
        public void Error(Exception ex)
        {
            Error(ex.Message);
        }

        private string BuildMessage(string level, string message, params object[] args)
        {
            return String.Format("[{0}] {1:dd/MM/yy HH:mm} {2}", level, DateTime.Now, String.Format(message, args));
        }
        #endregion
    }
}