using System;

namespace Skahal.Threading
{
	/// <summary>
	/// Coroutine async action provider.
	/// </summary>
	public class CoroutineAsyncActionProvider : IAsyncActionProvider
	{
		#region Methods
		/// <summary>
		/// Start the action with the specified delay (in seconds).
		/// </summary>
		/// <param name="delay">Delay in seconds.</param>
		/// <param name="action">Action.</param>
		public IAsyncAction Start (float delay, Action action)
		{
			var coroutine = SHCoroutine.Start (delay, action);

			return new CoroutineAsyncAction (coroutine);
		}
		#endregion
	}
}
