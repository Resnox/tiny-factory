using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TinyFactory.Engine.Core;

public class Camera
{
    private TinyFactory.Core core;
    private float deltaTime;
    private float zoom;

    public Camera(TinyFactory.Core core)
    {
        this.core = core;
        X = 0;
        Y = 0;
        Viewport = core.GraphicsDevice.Viewport;
        Zoom = 1;
        deltaTime = 0;
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

    public void Update()
    {
        Transform = Matrix.CreateTranslation(-X, -Y, 0) *
                    Matrix.CreateScale(Math.Max(
                        Viewport.Width / Zoom / 2,
                        Viewport.Height / Zoom / 2
                    )) *
                    Matrix.CreateTranslation(Viewport.Width / 2f, Viewport.Height / 2f, 0);
    }
}