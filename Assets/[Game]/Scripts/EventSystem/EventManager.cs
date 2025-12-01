using System;
using System.Collections.Generic;
using UnityEngine;

public interface IGameEvent { }

public static class EventManager
{
    private static readonly Dictionary<Type, HashSet<Delegate>> _eventMap = new();

#if UNITY_EDITOR
    private static bool _debug = false;
    public static void SetDebug(bool value) => _debug = value;
#endif

    public static void Add<T>(Action<T> handler) where T : struct, IGameEvent
    {
        var type = typeof(T);
        if (!_eventMap.TryGetValue(type, out var set))
        {
            set = new HashSet<Delegate>();
            _eventMap[type] = set;
        }
        set.Add(handler);
    }

    public static void AddOnce<T>(Action<T> handler) where T : struct, IGameEvent
    {
        Action<T> wrapper = null;
        wrapper = (T evt) =>
        {
            Remove<T>(wrapper);
            handler(evt);
        };
        Add<T>(wrapper);
    }

    public static Action<T> AddOnceReturnWrapper<T>(Action<T> handler) where T : struct, IGameEvent
    {
        Action<T> wrapper = null;
        wrapper = (T evt) =>
        {
            Remove<T>(wrapper);
            handler(evt);
        };
        Add<T>(wrapper);
        return wrapper;
    }

    public static void Remove<T>(Action<T> handler) where T : struct, IGameEvent
    {
        var type = typeof(T);
        if (_eventMap.TryGetValue(type, out var set))
        {
            set.Remove(handler);
            if (set.Count == 0)
                _eventMap.Remove(type);
        }
    }

    public static void Raise<T>(T evt) where T : struct, IGameEvent
    {
        var type = typeof(T);

#if UNITY_EDITOR
        if (_debug) Debug.Log($"[EventManager] Raise: {type.Name}");
#endif

        if (!_eventMap.TryGetValue(type, out var set) || set.Count == 0)
            return;

        var handlers = new List<Delegate>(set);

        foreach (var handler in handlers)
        {
            if (handler is Action<T> action)
            {
                try
                {
                    action(evt);
                }
                catch (Exception e)
                {
                    Debug.LogError($"Error processing event {type.Name}: {e}");
                }
            }
        }
    }

    public static void ClearAll() => _eventMap.Clear();
}