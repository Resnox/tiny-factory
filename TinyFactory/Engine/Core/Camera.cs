using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TinyFactory.Engine.Core;

public class Camera
{
    private float zoom;

    public Camera(GameCore gameCore)
    {
        Position = Vector2.Zero;
        Viewport = gameCore.GraphicsDevice.Viewport;
        Zoom = 10f;
    }

    public Matrix Transform { get; private set; }
    public Viewport Viewport { get; set; }
    public Vector2 Position { get; set; }
    
    public const float MIN_ZOOM = 1f;
    public const float MAX_ZOOM = 20f;

    public float Zoom
    {
        get => zoom;
        set => zoom = Math.Clamp(value, MIN_ZOOM, MAX_ZOOM);
    }

    public void Update()
    {
        Transform = Matrix.CreateTranslation(-Position.X, -Position.Y, 0) *
                    Matrix.CreateScale(Math.Max(
                        Viewport.Width / Zoom / 2,
                        Viewport.Height / Zoom / 2
                    )) *
                    Matrix.CreateTranslation(Viewport.Width / 2f, Viewport.Height / 2f, 0);
    }
}