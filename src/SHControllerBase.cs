using UnityEngine;
using System.Collections;
using System;
using Skahal.Logging;
//using Zenject;

namespace Skahal
{
	/// <summary>
	/// Controller base class.
	/// </summary>
	public abstract class SHControllerBase : MonoBehaviour
	{
		#region Fields
		private string m_controllerName;
		#endregion
		
		#region Constructors
		/// <summary>
		/// Construct the object.
		/// </summary>
		/// <param name="log">Log.</param>
		//[Inject]
		public void Construct (ISHLogStrategy log)
		{
			Log = log;
			m_controllerName = GetType ().Name;
			Cloneable = false;
		}
		#endregion
		
		#region Properties
		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="Skahal.SHControllerBase"/> is cloneable.
		/// </summary>
		/// <value><c>true</c> if cloneable; otherwise, <c>false</c>.</value>
		public bool Cloneable { get; set; }

		/// <summary>
		/// Gets the log.
		/// </summary>
		/// <value>The log.</value>
		protected ISHLogStrategy Log { get; private set; }
		#endregion
		
		#region Methods
		/// <summary>
		/// Writes a debug message.
		/// </summary>
		/// <param name="methodName">Method name.</param>
		/// <param name="message">Message.</param>
		/// <param name="args">Arguments.</param>
		protected void Debug (string methodName, string message, params object[] args)
		{
			var firstPart = String.Format ("{0}.{1}: ", m_controllerName, methodName);
			Log.Debug (firstPart + message, args);
		}
		#endregion
	}
}