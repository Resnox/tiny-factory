using Microsoft.Xna.Framework.Input;

namespace TinyFactory.Engine.Input;

public class KeyboardCapabilitiesManager : IInputCapabilitiesManager
{
    private KeyboardState currentState;
    private KeyboardState previousState;

    public void Update()
    {
        previousState = currentState;
        currentState = Keyboard.GetState();
    }
}