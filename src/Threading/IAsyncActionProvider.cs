using System;

namespace Skahal.Threading
{
	/// <summary>
	/// Define an interface to async action provider.
	/// </summary>
	public interface IAsyncActionProvider
	{
		/// <summary>
		/// Start the action with the specified delay (in seconds).
		/// </summary>
		/// <param name="delay">Delay in seconds.</param>
		/// <param name="action">Action.</param>
		IAsyncAction Start(float delay, Action action);
	}
}

