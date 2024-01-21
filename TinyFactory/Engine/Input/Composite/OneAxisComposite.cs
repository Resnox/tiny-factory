using TinyFactory.Engine.Input.Value;

namespace TinyFactory.Engine.Input.Composite;

public class OneAxisComposite : IInputValue<float>
{
    private readonly IInputValue<float> negative;
    private readonly IInputValue<float> positive;

    public OneAxisComposite(IInputValue<float> negative, IInputValue<float> positive)
    {
        this.negative = negative;
        this.positive = positive;
    }

    #region IInputValue<float> Members

    public float GetValue()
    {
        return positive.GetValue() - negative.GetValue();
    }

    #endregion
}