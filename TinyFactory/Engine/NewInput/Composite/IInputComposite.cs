namespace TinyFactory.Engine.NewInput.Composite;

public interface IInputComposite<T>
{
    public abstract T GetValue();
}