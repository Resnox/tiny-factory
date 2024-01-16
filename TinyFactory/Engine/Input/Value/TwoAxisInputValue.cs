using Microsoft.Xna.Framework;

namespace TinyFactory.Engine.Input.Value;

public readonly struct TwoAxisInputValue: IInputValue<Vector2>
{
    private readonly InputAction leftAction;
    private readonly InputAction rightAction;
    private readonly InputAction upAction;
    private readonly InputAction downAction;

    public TwoAxisInputValue(InputAction leftAction, InputAction rightAction, InputAction upAction,
        InputAction downAction)
    {
        this.leftAction = leftAction;
        this.rightAction = rightAction;
        this.upAction = upAction;
        this.downAction = downAction;
    }
    
    public TwoAxisInputValue(InputManager inputManager, string leftName, string rightName, string upName, string downName): this(inputManager.GetAction(leftName), inputManager.GetAction(rightName), inputManager.GetAction(upName), inputManager.GetAction(downName))
    {
        
    }

    public Vector2 GetValue()
    {
        var x = 0;
        var y = 0;

        if (leftAction.Down)
            x = -1;
        else if (rightAction.Down)
            x = 1;

        if (upAction.Down)
            y = -1;
        else if (downAction.Down)
            y = 1;

        return new Vector2(x, y);
    }
}