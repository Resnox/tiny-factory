using TinyFactory.Engine.Input.Value;
using Mouse = TinyFactory.Engine.Input.Engine.Mouse;

namespace TinyFactory.Engine.Input.Binding;

public class MouseWheelBinding : IInputValue<float>
{
    private readonly Mouse engine;

    public MouseWheelBinding(Mouse engine)
    {
        this.engine = engine;
    }

    #region IInputValue<float> Members

    public float GetValue()
    {
        return engine.ScrollWheelDelta;
    }

    #endregion
}