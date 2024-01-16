using System;
using System.Collections.Generic;
using System.Dynamic;
using TinyFactory.Engine.Input.Keyboard;
using TinyFactory.Engine.Input.Mouse;

namespace TinyFactory.Engine.Input;

public class InputManager : DynamicObject
{
    private readonly Dictionary<string, InputAction> inputActions = new();
    private readonly Dictionary<string, IInputEngine> inputEngines = new();

    public InputManager()
    {
        RegisterEngine("keyboard", new KeyboardEngine());
        RegisterEngine("mouse", new KeyboardEngine());
    }

    public KeyboardEngine Keyboard => (KeyboardEngine)GetEngine("keyboard");
    public MouseEngine Mouse => (MouseEngine)GetEngine("mouse");

    public void Update()
    {
        foreach (var inputEngine in inputEngines.Values) inputEngine.Update();
        foreach (var inputAction in inputActions.Values) inputAction.Update(this);
    }

    public void RegisterAction(string name, params IInputCondition[] conditions)
    {
        inputActions[name] = new InputAction(conditions);
    }

    public InputAction GetAction(string actionName)
    {
        if (inputActions.TryGetValue(actionName, out var value)) return value;

        throw new ArgumentException($"{actionName} unknown");
    }

    public void RegisterEngine(string name, IInputEngine engine)
    {
        inputEngines[name] = engine;
    }

    public IInputEngine GetEngine(string engineName)
    {
        if (inputEngines.TryGetValue(engineName, out var value)) return value;

        throw new ArgumentException($"{engineName} unknown");
    }
}