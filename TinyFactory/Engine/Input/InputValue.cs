namespace TinyFactory.Engine.Input;

public interface IInputValue<T>
{
    public T GetValue();
}