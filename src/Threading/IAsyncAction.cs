using System;

namespace Skahal.Threading
{
	/// <summary>
	/// Define an interface to async action.
	/// </summary>
	public interface IAsyncAction
	{
		/// <summary>
		/// Cancels the action.
		/// </summary>
		void Cancel();
	}
}
