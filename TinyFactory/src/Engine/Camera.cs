using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TinyFactory.Engine;

public class Camera
{
    public double X;
    public double Y;
    public Rectangle Bounds;
    public double OrthographicSize;
    
    public Camera(Viewport viewport)
    {
        X = 0;
        Y = 0;
        Bounds = viewport.Bounds;
        OrthographicSize = 5;
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
        displayPosition.X = (float)((x - 0.5) * pixelPerUnit) + Bounds.Width / 2f;
        displayPosition.Y = (float)((y - 0.5) * pixelPerUnit) + Bounds.Height / 2f;
        return displayPosition;
    }

    public float GetPixelPerUnit()
    {
        return Math.Max(
            (float)(Bounds.Width / OrthographicSize / 2),
            (float)(Bounds.Height / OrthographicSize / 2)
        );
    }
}