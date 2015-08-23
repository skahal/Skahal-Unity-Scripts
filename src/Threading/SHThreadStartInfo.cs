#region Usings
using System;

#endregion

namespace Skahal.Threading
{
	/// <summary>
	/// Start info for SHThread
	/// </summary>
	public sealed class SHThreadStartInfo
	{
		#region Properties

		/// <summary>
		/// Gets or sets the action that will be execute on thread.
		/// </summary>
		/// <value>
		/// The action.
		/// </value>
		public Action Action { get; set; }

		/// <summary>
		/// Gets or sets the time, in seconds, to wait until execute the action in the thread.
		/// </summary>
		/// <value>
		/// The delay.
		/// </value>
		public float Delay { get; set; }

		#endregion
	}
}