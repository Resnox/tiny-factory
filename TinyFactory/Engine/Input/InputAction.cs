using System.Collections.Generic;

namespace TinyFactory.Engine.Input;

public class InputAction
{
    private readonly List<IInputCondition> inputConditions;
    private bool previousState;

    public InputAction(params IInputCondition[] conditions)
    {
        previousState = false;
        inputConditions = new List<IInputCondition>(conditions);
        Down = false;
        previousState = false;
    }

    public bool Released => previousState && !Down;
    public bool Pressed => !previousState && Down;
    public bool Down { get; private set; }

    public bool Up => !Down;

    public void Update(InputManager inputManager)
    {
        previousState = Down;
        foreach (var inputCondition in inputConditions)
        {
            if (!inputCondition.IsValid(inputManager)) continue;

            Down = true;
            return;
        }

        Down = false;
    }
}