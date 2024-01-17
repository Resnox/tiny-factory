namespace TinyFactory.Engine.Input.Action;

public interface IInputValue<T>
{
    public T GetValue();
}