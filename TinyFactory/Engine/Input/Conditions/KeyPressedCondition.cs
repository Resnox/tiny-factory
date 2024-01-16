using Microsoft.Xna.Framework.Input;
using TinyFactory.Engine.Input.Keyboard;

namespace TinyFactory.Engine.Input.Conditions;

public class KeyPressedCondition : IInputCondition
{
    private readonly Keys key;

    public KeyPressedCondition(Keys key)
    {
        this.key = key;
    }

    #region IInputCondition Members

    public bool IsValid(InputManager inputManager)
    {
        return ((KeyboardEngine)inputManager.GetEngine("keyboard")).IsKeyPressed(key);
    }

    #endregion
}