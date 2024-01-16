namespace TinyFactory.Engine.Input;

public interface IInputCondition
{
    public bool IsValid(InputManager inputManager);
}