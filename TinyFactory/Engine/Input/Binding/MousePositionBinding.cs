using Microsoft.Xna.Framework;
using TinyFactory.Engine.Input.Value;
using Mouse = TinyFactory.Engine.Input.Engine.Mouse;

namespace TinyFactory.Engine.Input.Binding;

public class MousePositionBinding : IInputValue<Vector2>
{
    private readonly Mouse engine;

    public MousePositionBinding(Mouse engine)
    {
        this.engine = engine;
    }

    #region IInputValue<float> Members

    public Vector2 GetValue()
    {
        return engine.CurrentCursorPosition;
    }

    #endregion
}