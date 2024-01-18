using System.Collections.Generic;
using TinyFactory.Engine.Input.Value;

namespace TinyFactory.Engine.Input;

public class InputAction
{
    private List<IInputValue> values = new List<IInputValue>();
    
    public InputAction(params IInputValue [] values)
    {
        this.values.AddRange(values);
    }
    
    public void Add(IInputValue value)
    {
        values.Add(value);
    }
    
    public T GetValue<T>()
    {
        foreach (var value in values)
        {
            if (value is IInputValue<T> inputValue)
                return inputValue.GetValue();
        }

        return default;
    }
}