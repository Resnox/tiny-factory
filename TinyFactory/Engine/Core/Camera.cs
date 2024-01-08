using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TinyFactory.Engine.Core.Interfaces;

namespace TinyFactory.Engine.Core;

public class Camera : IPreUpdatable, IUpdatable
{
    protected InputManager InputManager;

    private float zoom;

    public Camera(InputManager inputManager, Viewport viewport)
    {
        X = 0;
        Y = 0;
        Viewport = viewport;
        Zoom = 1;
        InputManager = inputManager;
    }

    public Matrix Transform { get; private set; }
    public Viewport Viewport { get; set; }
    public float X { get; set; }
    public float Y { get; set; }

    public float Zoom
    {
        get => zoom;
        set => zoom = value < 0.05f ? 0.05f : value;
    }

    #region IPreUpdatable Members

    public void BeforeUpdate(in float deltaTime)
    {
        Transform = Matrix.CreateTranslation(-X, -Y, 0) *
                    Matrix.CreateScale(Math.Max(
                        Viewport.Width / Zoom / 2,
                        Viewport.Height / Zoom / 2
                    )) *
                    Matrix.CreateTranslation(Viewport.Width / 2f, Viewport.Height / 2f, 0);
    }

    #endregion

    #region IUpdatable Members

    public void Update(in float deltaTime)
    {
        int moveX = 0, moveY = 0;

        if (InputManager.IsKey(Keys.Q)) moveX -= 1;
        if (InputManager.IsKey(Keys.D)) moveX += 1;
        if (InputManager.IsKey(Keys.Z)) moveY -= 1;
        if (InputManager.IsKey(Keys.S)) moveY += 1;

        var mouseWheelDelta = InputManager.GetMouseWheelDelta();
        if (Math.Abs(mouseWheelDelta) > 0.1f) Zoom -= mouseWheelDelta;

        X += deltaTime * moveX * 5;
        Y += deltaTime * moveY * 5;
    }

    #endregion
}