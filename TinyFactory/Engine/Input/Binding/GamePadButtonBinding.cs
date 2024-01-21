using Microsoft.Xna.Framework;
using TinyFactory.Engine.Input.Enum;
using TinyFactory.Engine.Input.Value;
using GamePad = TinyFactory.Engine.Input.Engine.GamePad;

namespace TinyFactory.Engine.Input.Binding;

public class GamePadButtonBinding : IInputValue<float>
{
    private readonly GamePadButton button;
    private readonly GamePad engine;
    private readonly PlayerIndex playerIndex;

    public GamePadButtonBinding(GamePad engine, GamePadButton button, PlayerIndex playerIndex)
    {
        this.engine = engine;
        this.button = button;
        this.playerIndex = playerIndex;
    }

    #region IInputValue<float> Members

    public float GetValue()
    {
        return engine.IsPressed(button, playerIndex) ? 1f : 0f;
    }

    #endregion
}