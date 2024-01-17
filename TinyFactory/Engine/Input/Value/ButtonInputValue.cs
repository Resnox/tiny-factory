namespace TinyFactory.Engine.Input.Value;

public readonly struct ButtonInputValue : IInputValue<bool>
{
    private readonly InputAction buttonAction;

    public ButtonInputValue(InputAction buttonAction)
    {
        this.buttonAction = buttonAction;
    }

    public ButtonInputValue(InputManager inputManager, string buttonName) : this(inputManager.GetAction(buttonName))
    {
    }

    #region IInputValue<bool> Members

    public bool GetValue()
    {
        return buttonAction.Down;
    }

    #endregion
}