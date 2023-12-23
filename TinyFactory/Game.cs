using Arch.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyFactory.ECS;
using TinyFactory.System;
using XnaGame = Microsoft.Xna.Framework.Game;

namespace TinyFactory
{
    class Game : XnaGame
    { 
        private readonly GraphicsDeviceManager Gdm;
        private readonly World World;
        private readonly SpriteBatch SpriteBatch;
        private readonly SystemGroup SystemGroup;
        
        public Game()
        {
            Gdm = new GraphicsDeviceManager(this);
            // Typically you would load a config here...
            Gdm.PreferredBackBufferWidth = 1280;
            Gdm.PreferredBackBufferHeight = 720;
            Gdm.IsFullScreen = false;
            Gdm.SynchronizeWithVerticalRetrace = true;
            
            Content.RootDirectory = "Content";

            World = World.Create();
            SystemGroup = new SystemGroup();
            
            SpriteBatch = new SpriteBatch(Gdm.GraphicsDevice);

            SystemGroup.Add(new SpriteRendererSystem(World, SpriteBatch));

        }
        
        protected override void Initialize()
        {
            /* This is a nice place to start up the engine, after
             * loading configuration stuff in the constructor
             */
            base.Initialize();
            SystemGroup.Initialize();
        }

        protected override void LoadContent()
        {
            // Load textures, sounds, and so on in here...
            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            // Clean up after yourself!
            base.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            double deltaTime = gameTime.ElapsedGameTime.TotalSeconds;
            
            SystemGroup.BeforeUpdate(deltaTime);
            
            // Run game logic in here. Do NOT render anything here!
            base.Update(gameTime);
            SystemGroup.Update(deltaTime);
            
            SystemGroup.AfterUpdate(deltaTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            // Render stuff in here. Do NOT run game logic in here!
            GraphicsDevice.Clear(Color.CornflowerBlue);
            base.Draw(gameTime);
            SystemGroup.Render();
        }
    }
}