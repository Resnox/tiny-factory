namespace TinyFactory.Engine.Input.Exception;

public class InputActionUnknownException : System.Exception
{
    public InputActionUnknownException(string inputAction) : base($"Invalid action name: {inputAction}")
    {
    }
}