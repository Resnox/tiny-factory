namespace TinyFactory.Engine.Input;

public interface IInputEngine
{
    public void Setup(InputManager inputManager);
    public void Update();
}