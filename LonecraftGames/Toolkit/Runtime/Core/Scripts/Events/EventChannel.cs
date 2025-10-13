using System;
using UnityEngine;

namespace LonecraftGames.Toolkit.Core.Events
{
    /// Use for no-payload events.
    [Serializable] public struct Void { public static readonly Void Default = new(); }

    /// <summary>
    /// Generic ScriptableObject event channel.
    /// - No threading, no allocs if you use struct payloads.
    /// - Optional sticky (replay last) and simple logging.
    /// </summary>
    public class EventChannel<T> : ScriptableObject
    {
        [Header("Options")]
        [Tooltip("If true, remember last payload and optionally replay it to new subscribers.")]
        [SerializeField] private bool sticky;

        [Tooltip("If true, logs raises and listener count (useful while wiring).")]
        [SerializeField] private bool debugLog;

        private Action<T> _listeners;
        private bool _hasLast;
        private T _last;

        public int ListenerCount => _listeners?.GetInvocationList()?.Length ?? 0;

        public void Raise(T payload)
        {
            if (sticky) { _last = payload; _hasLast = true; }
            if (debugLog) Debug.Log($"[{name}] Raise<{typeof(T).Name}> (listeners={ListenerCount})", this);

            // Invoke safely so one bad handler doesn’t break the rest.
            var list = _listeners?.GetInvocationList();
            if (list == null) return;
            for (int i = 0; i < list.Length; i++)
            {
                try { ((Action<T>)list[i]).Invoke(payload); }
                catch (Exception ex) { Debug.LogException(ex, this); }
            }
        }

        /// <param name="replaySticky">If true and sticky has a value, immediately call the listener with the last payload.</param>
        public void RegisterListener(Action<T> listener, bool replaySticky = false)
        {
            _listeners += listener;
            if (replaySticky && sticky && _hasLast)
            {
                try { listener(_last); } catch (Exception ex) { Debug.LogException(ex, this); }
            }
        }

        public void RegisterOnce(Action<T> listener, bool replaySticky = false)
        {
            Action<T> wrapper = null;
            wrapper = (p) =>
            {
                UnregisterListener(wrapper);
                listener(p);
            };
            RegisterListener(wrapper, replaySticky);
        }

        public void UnregisterListener(Action<T> listener) => _listeners -= listener;

        public void ClearAll() => _listeners = null;
    }

    /// Concrete no-arg channel you can create from Assets menu.
    [CreateAssetMenu(menuName = "LonecraftGames/Events/VoidEventChannel", fileName = "VoidEventChannel")]
    public sealed class VoidEventChannel : EventChannel<Void> { }
}
