using Microsoft.Xna.Framework;

namespace TinyFactory.Component;

public struct Transform
{
    public Vector2 Position = Vector2.Zero;
    public Vector2 Scale = Vector2.One;

    public Transform()
    {
    }
}