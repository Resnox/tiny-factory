using System.Runtime.CompilerServices;
using Arch.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyFactory.Component;
using TinyFactory.ECS;

namespace TinyFactory.System;

public class SpriteRendererSystem : BaseSystem
{
    private readonly SpriteBatch spriteBatch;
    private readonly QueryDescription queryDescription = new QueryDescription().WithAll<Position, Sprite>();

    public SpriteRendererSystem(World world, SpriteBatch spriteBatch) : base(world)
    {
        this.spriteBatch = spriteBatch;
    }

    public override void Render()
    {
        var render = new RenderJob(spriteBatch);
        world.InlineQuery<RenderJob, Position, Sprite>(queryDescription, ref render);
    }

    private readonly struct RenderJob : IForEach<Position, Sprite>
    {
        private readonly SpriteBatch spriteBatch;

        public RenderJob(SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Update(ref Position position, ref Sprite sprite)
        {
            spriteBatch.Draw(
                sprite.Texture,
                new Rectangle((int)position.X, (int)position.Y, 32, 32),
                sprite.Color
            );
        }
    }
}