using System;
using Microsoft.Xna.Framework;
using TinyFactory.Engine.Input.Enum;
using TinyFactory.Engine.Input.Value;
using GamePad = TinyFactory.Engine.Input.Engine.GamePad;

namespace TinyFactory.Engine.Input.Binding;

public class GamePadJoystickBinding : IInputValue<Vector2>
{
    private readonly GamePad engine;
    private readonly GamePadJoystick joystick;
    private readonly PlayerIndex playerIndex;

    public GamePadJoystickBinding(GamePad engine, GamePadJoystick joystick, PlayerIndex playerIndex)
    {
        this.engine = engine;
        this.joystick = joystick;
        this.playerIndex = playerIndex;
    }

    #region IInputValue<Vector2> Members

    public Vector2 GetValue()
    {
        return joystick switch
        {
            GamePadJoystick.LeftStick => engine.GetLeftJoystick(playerIndex),
            GamePadJoystick.RightStick => engine.GetRightJoystick(playerIndex),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    #endregion
}