using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TinyFactory.Engine.Texture;

public class TextureManager
{
    private readonly GraphicsDevice graphicsDevice;
    private readonly Dictionary<string, int> nameIndex;
    private readonly List<Texture2D> textures;

    public TextureManager(GraphicsDevice graphicsDevice)
    {
        textures = new List<Texture2D>();
        nameIndex = new Dictionary<string, int>();

        this.graphicsDevice = graphicsDevice;

        AddWhitePixelTexture();
    }

    public Texture2D this[int index] => textures[index];
    public Texture2D this[string textureName] => textures[nameIndex[textureName]];

    public void AddWhitePixelTexture()
    {
        var whitePixel = new Texture2D(graphicsDevice, 1, 1, false, SurfaceFormat.Color);
        whitePixel.SetData(new[] { Color.White });

        AddTexture("whitePixel", whitePixel);
    }

    public void AddTexture(string textureName, Texture2D texture)
    {
        nameIndex.Add(textureName, textures.Count);
        textures.Add(texture);
    }
}