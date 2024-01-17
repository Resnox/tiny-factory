using Microsoft.Xna.Framework.Input;
using Keyboard = TinyFactory.Engine.NewInput.Engine.Keyboard;

namespace TinyFactory.Engine.NewInput.Condition;

public class KeyPressedCondition : InputCondition
{
    private readonly Keys key;

    public KeyPressedCondition(InputManager inputManager, Keys key) : base(inputManager)
    {
        this.key = key;
    }

    public override bool IsValid()
    {
        return inputManager.GetEngine<Keyboard>().IsKeyPressed(key);
    }
}