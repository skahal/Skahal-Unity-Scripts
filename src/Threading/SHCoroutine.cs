#region Usings
using UnityEngine;
using System.Collections;
using System;
using System.Threading;
#endregion

namespace Skahal.Threading
{
    /// <summary>
    /// Coroutine facilities.
    /// </summary>
    public static class SHCoroutine
    {
        #region Public methods

        /// <summary>
        /// Start the specified action in parallel.
        /// <param name="delay">The deelay, in seconds, to start the action.</param>
        /// <param name="action">The action to be executed.</param>
        /// </summary>
        public static Coroutine Start(float delay, Action action)
        {
            ValidateTimeScale();
            return SH.StartCoroutine(Run(delay, action));
        }

        /// <summary>
        /// Stop the specified coroutine.
        /// </summary>
        /// <param name="coroutine">Coroutine.</param>
        public static void Stop(Coroutine coroutine)
        {
            SH.StopCoroutine(coroutine);
        }

        /// <summary>
        /// Start the specified action in parallel.
        /// </summary>
        public static void StartEndOfFrame(Action action)
        {
            ValidateTimeScale();
            SH.StartCoroutine(RunEndOfFrame(action));
        }

        /// <summary>
        /// Start the specified actions in parallel
        /// </summary>
        public static void Start(float delay, params Action[] actions)
        {
            ValidateTimeScale();

            foreach (var action in actions)
            {
                SH.StartCoroutine(Run(delay, action));
            }
        }

        /// <summary>
        /// Start the specified actions in parallel
        /// </summary>
        public static void Start(params Action[] actions)
        {
            Start(0, actions);
        }

        /// <summary>
        /// Repeat the specified action with interval between from and to values.
        /// </summary>
        public static void Repeat(float interval, float from, float to, Action<float> action)
        {
            ValidateTimeScale();
            SH.StartCoroutine(RunRepeat(interval, from, to, action));
        }


        /// <summary>
        /// Performs a ping-pong repeat action with interval between from and to values.
        /// </summary>
        public static void PingPong(float interval, float from, float to, Func<float, bool> action)
        {
            ValidateTimeScale();
            SH.StartCoroutine(RunPingPong(interval, from, to, action));
        }

        /// <summary>
        /// Performs a loop repeat action with interval between from and to values.
        /// </summary>
        public static void Loop(float interval, float from, float to, Func<float, bool> action)
        {
            ValidateTimeScale();
            SH.StartCoroutine(RunLoop(interval, from, to, action));
        }

        /// <summary>
		/// Performs a loop repeat action with interval
		/// </summary>
		public static void Loop(float interval, Action action)
        {
            ValidateTimeScale();
            SH.StartCoroutine(RunLoop(interval, 0, float.MaxValue, (i) =>
            {
                action();
                return true;
            }));
        }

        /// <summary>
        /// Waits for the waitForCondition function returns true.
        /// </summary>
        public static void WaitFor(Func<bool> waitForCondition, Action waitEndedAction)
        {
            ValidateTimeScale();
            SH.StartCoroutine(RunWaitFor(waitForCondition, waitEndedAction));
        }

        #endregion

        #region Private methods

        private static void ValidateTimeScale()
        {
            if (Time.timeScale <= 0)
            {
                Debug.LogWarning("SHThread: Time.timeScale should be greater than zero to use this method.");
            }
        }

        private static IEnumerator Run(float delay, Action action)
        {
            yield return new WaitForSeconds(delay);

            try
            {
                action();
            }
            catch (Exception ex)
            {
                Debug.LogWarning("SHThread.Start: error while executing action - " + ex.Message);
            }
        }

        private static IEnumerator RunEndOfFrame(Action action)
        {
            yield return new WaitForEndOfFrame();

            try
            {
                action();
            }
            catch (Exception ex)
            {
                Debug.LogWarning("SHThread.Start: error while executing action - " + ex.Message);
            }
        }

        private static IEnumerator RunRepeat(float interval, float from, float to, Action<float> action)
        {
            for (float i = from; i < to; i++)
            {
                action(i);
                yield return new WaitForSeconds(interval);
            }
        }

        private static IEnumerator RunPingPong(float interval, float from, float to, Func<float, bool> action)
        {
            bool isCancelled = false;

            for (float i = from; i < to; i++)
            {

                if (!action(i))
                {
                    isCancelled = true;
                    break;
                }

                yield return new WaitForSeconds(interval);
            }

            if (!isCancelled)
            {
                for (float i = to - 1; i >= from; i--)
                {

                    if (!action(i))
                    {
                        isCancelled = true;
                        break;
                    }

                    yield return new WaitForSeconds(interval);
                }

                if (!isCancelled)
                {
                    SH.StartCoroutine(RunPingPong(interval, from, to, action));
                }
            }
        }

        private static IEnumerator RunLoop(float interval, float from, float to, Func<float, bool> action)
        {
            bool isCancelled = false;

            for (float i = from; i < to; i++)
            {

                if (!action(i))
                {
                    isCancelled = true;
                    break;
                }

                yield return new WaitForSeconds(interval);
            }

            if (!isCancelled)
            {
                SH.StartCoroutine(RunLoop(interval, from, to, action));
            }

        }

        private static IEnumerator RunWaitFor(Func<bool> waitForCondition, Action waitEndedAction)
        {
            while (!waitForCondition())
            {
                yield return new WaitForSeconds(1f);
            }

            waitEndedAction();
        }

        #endregion
    }
}