using System;
using System.Collections.Generic;

namespace TinyFactory.Engine.Input;

public abstract class ActionsMap
{
    public Dictionary<string, InputAction> actions = new();
    public InputManager manager;

    public ActionsMap(InputManager inputManager)
    {
        manager = inputManager;
    }

    public bool Enabled { get; set; }

    public void Update()
    {
        if (!Enabled) return;
        foreach (var action in actions.Values) action.Update();
    }

    public T GetAction<T>(string name) where T : InputAction
    {
        if (actions.TryGetValue(name, out var value)) return (T)value;

        throw new ArgumentException($"{typeof(T)} unknown");
    }

    public void RegisterAction<T>(string name) where T : InputAction
    {
        if (Activator.CreateInstance(typeof(T), this) is not InputAction actionMap) return;

        actions[name] = actionMap;
    }
}