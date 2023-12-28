using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TinyFactory.Engine;

public class Camera
{
    protected double X;
    protected double Y;
    protected Rectangle Bounds;
    protected double OrthographicSize;
    
    protected InputManager InputManager;

    public Camera(InputManager inputManager, Rectangle bounds)
    {
        X = 0;
        Y = 0;
        Bounds = bounds;
        OrthographicSize = 5;
        InputManager = inputManager;
    }

    public void SetPosition(double x, double y)
    {
        X = x;
        Y = y;
    }

    public void SetOrthographicSize(double orthographicSize)
    {
        OrthographicSize = orthographicSize < 0.05 ? 0.05 : orthographicSize;
    }

    public void SetBounds(Rectangle bounds)
    {
        Bounds = bounds;
    }
    
    public Vector2 PositionToScreen(double x, double y)
    {
        var pixelPerUnit = GetPixelPerUnit();
        
        Vector2 displayPosition;
        displayPosition.X = (float)((x - 0.5 - X) * pixelPerUnit) + Bounds.Width / 2f;
        displayPosition.Y = (float)((y - 0.5 - Y) * pixelPerUnit) + Bounds.Height / 2f;
        return displayPosition;
    }

    public float GetPixelPerUnit()
    {
        return Math.Max(
            (float)(Bounds.Width / OrthographicSize / 2),
            (float)(Bounds.Height / OrthographicSize / 2)
        );
    }

    public void Update(double deltaTime)
    {
        int moveX = 0, moveY = 0;

        if (InputManager.IsKey(Keys.Q))
        {
            moveX -= 1;
        }
        if (InputManager.IsKey(Keys.D))
        {
            moveX += 1;
        }
        if (InputManager.IsKey(Keys.Z))
        {
            moveY -= 1;
        }
        if (InputManager.IsKey(Keys.S))
        {
            moveY += 1;
        }

        var mouseWheelDelta = InputManager.GetMouseWheelDelta();
        if (Math.Abs(mouseWheelDelta) > 0.1f)
        {
            SetOrthographicSize(OrthographicSize + 0.1 * mouseWheelDelta);
        }
        
        SetPosition(X + deltaTime * moveX * 5, Y + deltaTime * moveY * 5);
    }
}