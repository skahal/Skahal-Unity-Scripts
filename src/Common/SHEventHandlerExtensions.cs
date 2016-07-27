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
            RaiseToEnabledSubscribers(handler, sender, EventArgs.Empty);
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
            RaiseToEnabledSubscribers(handler, sender, e);
        }

        private static void RaiseToEnabledSubscribers(MulticastDelegate handler, object sender, EventArgs e)
        {
            if (handler != null)
            {
                var subscribers = handler.GetInvocationList();

                foreach (var s in subscribers)
                {
                    var eventSubscriber = s.Target as IEventSubscriber;

                    if (eventSubscriber != null && !eventSubscriber.enabled)
                    {
                        continue;
                    }

                    s.DynamicInvoke(sender, e);
                }
            }
        }
		#endregion
	}
}