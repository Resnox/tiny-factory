using Microsoft.Xna.Framework;
using TinyFactory.Engine.Input.Value;
using Mouse = TinyFactory.Engine.Input.Engine.Mouse;

namespace TinyFactory.Engine.Input.Binding;

public class MouseDeltaBinding : IInputValue<Vector2>
{
    private readonly Mouse engine;

    public MouseDeltaBinding(Mouse engine)
    {
        this.engine = engine;
    }

    #region IInputValue<float> Members

    public Vector2 GetValue()
    {
        return engine.CursorPositionDelta;
    }

    #endregion
}