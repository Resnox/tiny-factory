using Microsoft.Xna.Framework.Input;

namespace TinyFactory.Engine.Input.Keyboard;

public class KeyboardEngine: IInputEngine
{
    public KeyboardState PreviousState { get; private set; }
    public KeyboardState CurrentState { get; private set; }
    
    public void Setup(InputManager inputManager)
    {
        PreviousState = Microsoft.Xna.Framework.Input.Keyboard.GetState();
        CurrentState = Microsoft.Xna.Framework.Input.Keyboard.GetState();
    }

    public void Update()
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