using System.Runtime.CompilerServices;
using Arch.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyFactory.ECS.Component;
using TinyFactory.Engine;

namespace TinyFactory.ECS.System;

public class SpriteRendererSystem : BaseSystem
{
    private readonly SpriteBatch spriteBatch;
    private readonly Camera camera;
    private readonly QueryDescription queryDescription = new QueryDescription().WithAll<Position, Sprite>();

    public SpriteRendererSystem(World world, SpriteBatch spriteBatch, Camera camera) : base(world)
    {
        this.spriteBatch = spriteBatch;
        this.camera = camera;
    }

    public override void Render()
    {
        var render = new RenderJob(spriteBatch, camera);
        world.InlineQuery<RenderJob, Position, Sprite>(queryDescription, ref render);
    }

    private readonly struct RenderJob : IForEach<Position, Sprite>
    {
        private readonly SpriteBatch spriteBatch;
        private readonly Camera camera;
        
        public RenderJob(SpriteBatch spriteBatch, Camera camera)
        {
            this.spriteBatch = spriteBatch;
            this.camera = camera;
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Update(ref Position position, ref Sprite sprite)
        {
            var realPosition = camera.PositionToScreen(position.X, position.Y);
            var pixelPerUnit = camera.GetPixelPerUnit();
            
            spriteBatch.Draw(
                sprite.Texture,
                new Rectangle((int)realPosition.X, (int)realPosition.Y, (int)pixelPerUnit, (int)pixelPerUnit),
                sprite.Color
            );
        }
    }
}