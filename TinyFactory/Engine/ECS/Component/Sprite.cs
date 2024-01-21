using Microsoft.Xna.Framework;

namespace TinyFactory.Engine.ECS.Component;

public struct Sprite
{
    public int TextureIndex = -1;
    public Color Color = Color.White;

    public Sprite()
    {
    }
}