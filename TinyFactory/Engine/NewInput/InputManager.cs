using System;
using System.Collections.Generic;
using TinyFactory.Engine.NewInput.Engine;

namespace TinyFactory.Engine.NewInput;

public class InputManager
{
    private readonly Dictionary<Type, ActionsMap> actionsMaps = new();
    private readonly Dictionary<Type, InputEngine> inputEngines = new();

    public void Update()
    {
        foreach (var inputEngine in inputEngines.Values) inputEngine.Update();
        foreach (var inputAction in actionsMaps.Values) inputAction.Update();
    }

    public void RegisterActionMap<T>(ActionsMap actionsMap) where T : ActionsMap
    {
        if (Activator.CreateInstance(typeof(T), this) is not ActionsMap actionMap) return;

        actionsMaps[typeof(T)] = actionMap;
    }

    public T GetActionMap<T>() where T : ActionsMap
    {
        if (actionsMaps.TryGetValue(typeof(T), out var value)) return (T)value;

        throw new ArgumentException($"{typeof(T)} unknown");
    }

    public void RegisterEngine<T>() where T : InputEngine
    {
        if (Activator.CreateInstance(typeof(T), this) is not InputEngine engine) return;

        engine.Setup();
        inputEngines[typeof(T)] = engine;
    }

    public T GetEngine<T>() where T : InputEngine
    {
        if (inputEngines.TryGetValue(typeof(T), out var value)) return (T)value;

        throw new ArgumentException($"{typeof(T)} unknown");
    }
}