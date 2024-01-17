﻿using Microsoft.Xna.Framework.Input;

namespace TinyFactory.Engine.NewInput.Engine;

public class Keyboard : InputEngine
{
    public Keyboard(InputManager manager) : base(manager)
    {
    }

    public KeyboardState PreviousState { get; private set; }
    public KeyboardState CurrentState { get; private set; }

    public override void Setup()
    {
        PreviousState = Microsoft.Xna.Framework.Input.Keyboard.GetState();
        CurrentState = Microsoft.Xna.Framework.Input.Keyboard.GetState();
    }

    public override void Update()
    {
        PreviousState = CurrentState;
        CurrentState = Microsoft.Xna.Framework.Input.Keyboard.GetState();
    }

    public bool IsKeyPressed(Keys key)
    {
        return CurrentState.IsKeyDown(key);
    }

    public bool WasKeyPressed(Keys key)
    {
        return PreviousState.IsKeyDown(key);
    }

    public bool IsKeyReleased(Keys key)
    {
        return CurrentState.IsKeyUp(key);
    }

    public bool WasKeyReleased(Keys key)
    {
        return PreviousState.IsKeyUp(key);
    }
}