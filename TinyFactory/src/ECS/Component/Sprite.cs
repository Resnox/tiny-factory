using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyFactory.Engine;

namespace TinyFactory.ECS.Component;

public struct Sprite
{
    public int TextureIndex;
    public Color Color = Color.White;

    public Sprite()
    {
        TextureIndex = 0;
    }
}