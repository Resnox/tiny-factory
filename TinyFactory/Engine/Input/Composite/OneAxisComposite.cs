using TinyFactory.Engine.Input.Action;

namespace TinyFactory.Engine.Input.Composite;

public class OneAxisComposite: IInputValue<float>
{
    private IInputValue<bool> left;
    private IInputValue<bool> right;

    public OneAxisComposite(IInputValue<bool> left, IInputValue<bool> right)
    {
        this.left = left;
        this.right = right;
    }

    public float GetValue()
    {
        return (right.GetValue() ? 1 : 0) - (left.GetValue() ? 1 : 0);
    }
}