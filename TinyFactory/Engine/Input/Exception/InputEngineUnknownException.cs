namespace TinyFactory.Engine.Input.Exception;

public class InputEngineUnknownException : System.Exception
{
    public InputEngineUnknownException(string engineName) : base($"Invalid engine: {engineName}")
    {
    }
}