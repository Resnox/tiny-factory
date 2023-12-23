using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TinyFactory.Component;

public struct Sprite
{
    public Texture2D Texture;
    public Color Color = Color.White;

    public Sprite()
    {
        Texture = null;
    }
}