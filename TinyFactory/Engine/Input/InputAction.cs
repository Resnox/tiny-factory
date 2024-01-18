using System;
using System.Collections.Generic;
using TinyFactory.Engine.Input.Value;

namespace TinyFactory.Engine.Input;

public class InputAction
{
    private readonly List<IInputValue> values = new();

    public InputAction(params IInputValue[] values)
    {
        this.values.AddRange(values);
    }

    public void Add(IInputValue value)
    {
        values.Add(value);
    }

    public T GetValue<T>()
    {
        Console.WriteLine(values.Count);
        foreach (var value in values)
            if (value is IInputValue<T> inputValue &&
                !EqualityComparer<T>.Default.Equals(inputValue.GetValue(), default))
                return inputValue.GetValue();

        return default;
    }
}