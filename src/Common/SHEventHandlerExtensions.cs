#region Usings
using System;
#endregion

namespace Skahal.Common
{
	/// <summary>
	/// Extensions methods for EventHandler.
	/// </summary>
	public static class SHEventHandlerExtensions
	{
		#region Methods
		/// <summary>
		/// Raise event.
		/// </summary>
		/// <param name='handler'>
		/// Handler.
		/// </param>
		/// <param name='sender'>
		/// Sender.
		/// </param>
		public static void Raise (this EventHandler handler, object sender)
		{
			if (handler != null)
			{
				handler (sender, EventArgs.Empty);
			}
		}
		
		/// <summary>
		/// Raise the event.
		/// </summary>
		/// <param name='handler'>
		/// Handler.
		/// </param>
		/// <param name='sender'>
		/// Sender.
		/// </param>
		/// <param name='e'>
		/// E.
		/// </param>
		/// <typeparam name='TEventArgs'>
		/// The 1st type parameter.
		/// </typeparam>
		public static void Raise<TEventArgs> (this EventHandler<TEventArgs> handler, object sender, TEventArgs e) 
			where TEventArgs : EventArgs
		{
			if (handler != null)
			{
				handler (sender, e);
			}
		}
		#endregion
	}
}