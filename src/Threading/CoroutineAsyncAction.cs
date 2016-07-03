using System;
using UnityEngine;

namespace Skahal.Threading
{
	/// <summary>
	/// Coroutine async action.
	/// </summary>
	public class CoroutineAsyncAction : IAsyncAction
	{
		#region Fields
		private Coroutine m_coroutine;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Skahal.Threading.CoroutineAsyncAction"/> class.
		/// </summary>
		/// <param name="coroutine">Coroutine.</param>
		public CoroutineAsyncAction (Coroutine coroutine)
		{
			m_coroutine = coroutine;
		}
		#endregion

		#region Methods
		/// <summary>
		/// Cancels the action.
		/// </summary>
		public void Cancel ()
		{
			SHCoroutine.Stop (m_coroutine);
		}
		#endregion
	}
}

