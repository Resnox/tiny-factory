using System;
using System.Collections.Generic;
using TinyFactory.Engine.Input.Engine;

namespace TinyFactory.Engine.Input;

public class InputManager
{
    private readonly Dictionary<string, ActionsMap> actionsMaps = new();
    private readonly Dictionary<Type, InputEngine> inputEngines = new();

    public InputManager()
    {
        RegisterEngine<Keyboard>();
        RegisterEngine<Mouse>();
    }

    public void Update()
    {
        foreach (var inputEngine in inputEngines.Values) inputEngine.Update();
    }

    public ActionsMap RegisterActionMap(string name)
    {
        actionsMaps[name] = new ActionsMap(this);
        return actionsMaps[name];
    }

    public ActionsMap GetActionMap(string name)
    {
        if (actionsMaps.TryGetValue(name, out var value)) return value;

        throw new ArgumentException($"{name} unknown");
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