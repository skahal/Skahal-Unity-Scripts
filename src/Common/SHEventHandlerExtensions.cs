#region Usings
using System;
using System.Reflection;
using Skahal.Logging;
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
        public static void Raise(this EventHandler handler, object sender)
        {
            RaiseToEnabledSubscribers(handler, sender, EventArgs.Empty, null);
        }

        /// <summary>
		/// Raise event and with a TargetInvocationException is catched it will not throw an exception, but it will log it and continue to next item on invocation list.
		/// </summary>
		/// <param name='handler'>
		/// Handler.
		/// </param>
		/// <param name='sender'>
		/// Sender.
		/// </param>
		public static void RaiseSafe(this EventHandler handler, object sender, ISHLogStrategy log)
        {
            RaiseToEnabledSubscribers(handler, sender, EventArgs.Empty, log);
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
        public static void Raise<TEventArgs>(this EventHandler<TEventArgs> handler, object sender, TEventArgs e)
            where TEventArgs : EventArgs
        {
            RaiseToEnabledSubscribers(handler, sender, e, null);
        }

        /// <summary>
		/// Raise event and with a TargetInvocationException is catched it will not throw an exception, but it will log it and continue to next item on invocation list.
		/// </summary>
		/// <param name='handler'>
		/// Handler.
		/// </param>
		/// <param name='sender'>
		/// Sender.
		/// </param>
		public static void RaiseSafe<TEventArgs>(this EventHandler<TEventArgs> handler, object sender, TEventArgs e, ISHLogStrategy log)
            where TEventArgs : EventArgs
        {
            RaiseToEnabledSubscribers(handler, sender, e, log);
        }

        private static void RaiseToEnabledSubscribers(MulticastDelegate handler, object sender, EventArgs e, ISHLogStrategy log)
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

                    try
                    {
                        s.DynamicInvoke(sender, e);
                    }
                    catch (TargetInvocationException ex)
                    {
                        if (log == null)
                        {
                            throw ex.InnerException;
                        }
                        else
                        {
                            log.Debug("Error invoke event handler: {0}", ex.InnerException.Message);
                        }
                    }
                }
            }
        }
        #endregion
    }
}