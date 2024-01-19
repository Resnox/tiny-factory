using Microsoft.Xna.Framework;
using TinyFactory.Engine.Input.Value;

namespace TinyFactory.Engine.Input.Composite;

public class TwoAxisComposite : IInputValue<Vector2>
{
    private readonly IInputValue<float> xAxis;
    private readonly IInputValue<float> yAxis;

    public TwoAxisComposite(IInputValue<float> xAxis, IInputValue<float> yAxis)
    {
        this.xAxis = xAxis;
        this.yAxis = yAxis;
    }

    public TwoAxisComposite(IInputValue<float> left, IInputValue<float> right, IInputValue<float> up,
        IInputValue<float> down) : this(new OneAxisComposite(left, right), new OneAxisComposite(down, up))
    {
    }

    #region IInputValue<Vector2> Members

    public Vector2 GetValue()
    {
        return new Vector2(
            xAxis.GetValue(),
            yAxis.GetValue()
        );
    }

    #endregion
}