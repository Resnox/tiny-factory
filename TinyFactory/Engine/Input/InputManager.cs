using System.Collections.Generic;
using TinyFactory.Engine.Core.Interfaces;

namespace TinyFactory.Engine.Input;

public class InputManager : IUpdatable
{
    private readonly Dictionary<string, IInputEngine> inputEngine;

    public InputManager()
    {
        inputEngine = new Dictionary<string, IInputEngine>();
    }

    #region IUpdatable Members

    public void Update(in float deltaTime)
    {
    }

    #endregion

    public void AddInputService(string name, IInputEngine engine)
    {
        inputEngine[name] = engine;
    }
}