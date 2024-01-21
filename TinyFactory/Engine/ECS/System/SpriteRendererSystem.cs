using System;
using System.Runtime.CompilerServices;
using Arch.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyFactory.Engine.Core.Interfaces;
using TinyFactory.Engine.ECS.Component;
using TinyFactory.Engine.Texture;

namespace TinyFactory.Engine.ECS.System;

public class SpriteRendererSystem : AbstractSystem, IRenderable
{
    private readonly QueryDescription queryDescription = new QueryDescription().WithAll<Transform, Sprite>();
    private readonly SpriteBatch spriteBatch;
    private readonly TextureManager textureManager;

    public SpriteRendererSystem(World world, TextureManager textureManager, SpriteBatch spriteBatch) :
        base(world)
    {
        this.textureManager = textureManager;
        this.spriteBatch = spriteBatch;
    }

    #region IRenderable Members

    public void Render()
    {
        var render = new RenderJob(textureManager, spriteBatch);
        World.InlineQuery<RenderJob, Transform, Sprite>(queryDescription, ref render);
    }

    #endregion

    #region Nested type: RenderJob

    private readonly struct RenderJob : IForEach<Transform, Sprite>
    {
        private readonly TextureManager textureManager;
        private readonly SpriteBatch spriteBatch;

        public RenderJob(TextureManager textureManager, SpriteBatch spriteBatch)
        {
            this.textureManager = textureManager;
            this.spriteBatch = spriteBatch;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Update(ref Transform transform, ref Sprite sprite)
        {
            var texture = textureManager.GetTextureByIndex(sprite.TextureIndex);

            spriteBatch.Draw(
                texture, 
                transform.Position, 
                null, 
                Color.White, 
                0f, 
                Vector2.One / 2f, 
                1f / MathF.Max(texture.Width, texture.Height), 
                SpriteEffects.None, 
                0f
            );
        }
    }

    #endregion
}