namespace TinyFactory.Engine.Input.Value;

public readonly struct OneAxisInputValue : IInputValue<float>
{
    private readonly InputAction leftAction;
    private readonly InputAction rightAction;

    public OneAxisInputValue(InputAction leftAction, InputAction rightAction)
    {
        this.leftAction = leftAction;
        this.rightAction = rightAction;
    }
    
    public OneAxisInputValue(InputManager inputManager, string leftName, string rightName): this(inputManager.GetAction(leftName), inputManager.GetAction(rightName))
    {
        
    }

    #region IInputValue<float> Members

    public float GetValue()
    {
        var x = 0;

        if (leftAction.Down)
            x = -1;
        else if (rightAction.Down)
            x = 1;

        return x;
    }

    #endregion
}