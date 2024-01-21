using Microsoft.Xna.Framework.Input;
using TinyFactory.Engine.Input.Value;
using Keyboard = TinyFactory.Engine.Input.Engine.Keyboard;

namespace TinyFactory.Engine.Input.Binding;

public class KeyUpBinding : IInputValue<float>
{
    private readonly Keyboard engine;
    private readonly Keys key;

    public KeyUpBinding(Keyboard engine, Keys key)
    {
        this.engine = engine;
        this.key = key;
    }

    #region IInputValue<float> Members

    public float GetValue()
    {
        return engine.IsKeyReleased(key) && engine.WasKeyPressed(key) ? 1f : 0f;
    }

    #endregion
}