using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace TinyFactory.Engine;

public enum KeyState
{
    Waiting,
    Started,
    Pressed,
    Released
}

public class InputManager
{
    private readonly Dictionary<Keys, KeyState> keyStates = new();
    private int mousewheelValue;

    public void BeforeUpdate()
    {
        foreach (var key in Enum.GetValues<Keys>())
            if (Keyboard.GetState().IsKeyDown(key))
            {
                keyStates[key] = KeyState.Pressed;
            }
            else if (Keyboard.GetState().IsKeyUp(key))
            {
                keyStates[key] = KeyState.Released;
            }
            else
            {
                keyStates.TryGetValue(key, out var keyState);

                switch (keyState)
                {
                    case KeyState.Released:
                        keyStates[key] = KeyState.Waiting;
                        break;

                    case KeyState.Started:
                        keyStates[key] = KeyState.Pressed;
                        break;

                    default:
                        keyStates[key] = keyState;
                        break;
                }
            }
    }

    public void AfterUpdate()
    {
        mousewheelValue = Mouse.GetState().ScrollWheelValue;
    }

    public KeyState GetKeyState(Keys key)
    {
        return keyStates.TryGetValue(key, out var keyState) ? keyState : KeyState.Waiting;
    }

    public bool IsKeyDown(Keys key)
    {
        return GetKeyState(key) == KeyState.Pressed;
    }

    public bool IsKeyUp(Keys key)
    {
        return GetKeyState(key) == KeyState.Released;
    }

    public bool IsKey(Keys key)
    {
        return GetKeyState(key) == KeyState.Pressed || GetKeyState(key) == KeyState.Started;
    }

    public int GetMouseWheelDelta()
    {
        return (mousewheelValue - Mouse.GetState().ScrollWheelValue) / 120;
    }
}