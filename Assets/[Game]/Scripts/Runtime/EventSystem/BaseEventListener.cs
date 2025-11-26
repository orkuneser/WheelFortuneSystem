using UnityEngine;

public abstract class BaseEventListener<TEvent> : MonoBehaviour where TEvent : struct, IGameEvent
{
    protected virtual void OnEnable() => EventManager.Add<TEvent>(OnEvent);
    protected virtual void OnDisable() => EventManager.Remove<TEvent>(OnEvent);
    protected abstract void OnEvent(TEvent evt);
}