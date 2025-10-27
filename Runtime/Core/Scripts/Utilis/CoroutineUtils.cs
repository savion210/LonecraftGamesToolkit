using System;
using System.Collections;
using UnityEngine;

namespace LonecraftGames.Toolkit.Core.Utilis
{
    public static class CoroutineUtils
    {
        public static Coroutine InvokeAfter(this MonoBehaviour host, float delay, Action action)
        {
            if (!host) return null;
            return host.StartCoroutine(InvokeAfter_Co(delay, action));
        }

        private static IEnumerator InvokeAfter_Co(float delay, Action action)
        {
            yield return new WaitForSeconds(delay);
            action?.Invoke();
        }

        public static void RestartCoroutine(this MonoBehaviour host, ref Coroutine handle, IEnumerator routine)
        {
            if (!host) return;
            if (handle != null) host.StopCoroutine(handle);
            handle = host.StartCoroutine(routine);
        }
    }
}