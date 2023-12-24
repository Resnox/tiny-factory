using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TinyFactory.Engine;

public class TextureManager
{
    private List<Texture2D> textures;
    private Dictionary<string, int> nameIndex;

    private GraphicsDevice graphicsDevice;

    public TextureManager(GraphicsDevice graphicsDevice)
    {
        textures = new List<Texture2D>();
        nameIndex = new Dictionary<string, int>();
        
        this.graphicsDevice = graphicsDevice;
        
        AddWhitePixelTexture();
    }

    public void AddWhitePixelTexture()
    {
        var whitePixel = new Texture2D(graphicsDevice, 1, 1, false, SurfaceFormat.Color);
        whitePixel.SetData(new[] {Color.White});
        
        AddTexture("whitePixel", whitePixel);
    }

    public void AddTexture(string textureName, Texture2D texture)
    {
        nameIndex.Add(textureName, textures.Count);
        textures.Add(texture);
    }

    public Texture2D this[int index] => textures[index];
    public Texture2D this[string textureName] => textures[nameIndex[textureName]];
}