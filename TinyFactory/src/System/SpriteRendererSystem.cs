using Arch.Core;
using Microsoft.Xna.Framework.Graphics;
using TinyFactory.Component;
using TinyFactory.ECS;

namespace TinyFactory.System;

public class SpriteRendererSystem : BaseSystem
{
    private readonly SpriteBatch SpriteBatch;
    
    public SpriteRendererSystem(World world, SpriteBatch spriteBatch) : base(world)
    {
        SpriteBatch = spriteBatch;
    }

    public override void Render()
    {
        world.Query(new QueryDescription().WithAll<Transform, Sprite>(),
            (ref Transform transform, ref Sprite sprite) =>
            {
                SpriteBatch.Draw(sprite.Texture, transform.Position, sprite.Color);
            }
        );
        
    }
}