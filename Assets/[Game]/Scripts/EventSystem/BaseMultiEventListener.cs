using UnityEngine;
using System;
using System.Collections.Generic;
using System.Reflection;

public abstract class BaseMultiEventListener : MonoBehaviour
{
    private readonly List<(Type type, Delegate handler, bool once)> _registrations = new();
    private static readonly Dictionary<Type, MethodInfo> _cachedRemoveMethods = new();

    protected void AddHandler<TEvent>(Action<TEvent> handler)
        where TEvent : struct, IGameEvent
    {
        EventManager.Add<TEvent>(handler);
        _registrations.Add((typeof(TEvent), handler, false));
    }

    protected void AddHandlerOnce<TEvent>(Action<TEvent> handler)
        where TEvent : struct, IGameEvent
    {
        EventManager.AddOnce<TEvent>(handler);
        _registrations.Add((typeof(TEvent), handler, true));
    }

    protected virtual void OnDisable()
    {
        for (int i = 0; i < _registrations.Count; i++)
        {
            var (type, del, once) = _registrations[i];

            if (!_cachedRemoveMethods.TryGetValue(type, out var method))
            {
                method = typeof(EventManager)
                    .GetMethod("Remove")!
                    .MakeGenericMethod(type);

                _cachedRemoveMethods[type] = method;
            }

            method.Invoke(null, new object[] { del });
        }

        _registrations.Clear();
    }
}
