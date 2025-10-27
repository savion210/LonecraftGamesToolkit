using System;
using UnityEngine.Events;

namespace LonecraftGames.Toolkit.Core.Utilis
{
    public static class EventUtils
    {
        /// <summary>Invoke a UnityEvent safely (null-checked).</summary>
        public static void SafeInvoke(this UnityEvent evt) => evt?.Invoke();

        /// <summary>Invoke a UnityEvent&lt;T&gt; safely (null-checked).</summary>
        public static void SafeInvoke<T>(this UnityEvent<T> evt, T arg) => evt?.Invoke(arg);

        /// <summary>Execute an action safely, catching exceptions if they occur.</summary>
        public static bool TryInvoke(Action action, Action<Exception> onError = null)
        {
            try
            {
                action?.Invoke();
                return true;
            }
            catch (Exception e)
            {
                onError?.Invoke(e);
                return false;
            }
        }
    }
}