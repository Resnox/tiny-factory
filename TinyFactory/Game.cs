using System;
using Arch.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyFactory.ECS;
using TinyFactory.ECS.Component;
using TinyFactory.ECS.System;
using TinyFactory.Engine;
using XnaGame = Microsoft.Xna.Framework.Game;

namespace TinyFactory;

internal class Game : XnaGame
{
    private readonly GraphicsDeviceManager Gdm;
    private SpriteBatch SpriteBatch;
    private SystemGroup SystemGroup;
    private Texture2D TestTexture;
    private World World;
    private Camera Camera;

    public Game()
    {
        Gdm = new GraphicsDeviceManager(this);
        // Typically you would load a config here...
        Gdm.PreferredBackBufferWidth = 480;
        Gdm.PreferredBackBufferHeight = 480;
        Gdm.IsFullScreen = false;
        Window.AllowUserResizing = true;
        Gdm.SynchronizeWithVerticalRetrace = true;

        Content.RootDirectory = "Content";
    }

    protected override void Initialize()
    {
        base.Initialize();

        World = World.Create();

        Camera = new Camera(GraphicsDevice.Viewport);
        Window.ClientSizeChanged += (sender, args) =>
        {
            Camera.SetBounds(GraphicsDevice.Viewport.Bounds);
        };

        for (int i = -10; i <= 10; i++)
        {
            for (int j = -10; j <= 10; j++)
            {
                World.Create(
                    new Position
                    {
                        X = i,
                        Y = j
                    },
                    new Sprite
                    {
                        Texture = TestTexture
                    }
                );
            }
        }

        SystemGroup = new SystemGroup();
        SystemGroup.Add(new MovementSystem(World));
        SystemGroup.Add(new SpriteRendererSystem(World, SpriteBatch, Camera));
        SystemGroup.Initialize();
    }
    
    protected override void LoadContent()
    {
        // Load textures, sounds, and so on in here...
        base.LoadContent();

        TestTexture = Content.Load<Texture2D>("Test");
        SpriteBatch = new SpriteBatch(GraphicsDevice);
    }

    protected override void UnloadContent()
    {
        // Clean up after yourself!
        base.UnloadContent();

        SpriteBatch.Dispose();
        TestTexture.Dispose();
    }

    protected override void OnExiting(object sender, EventArgs args)
    {
        base.OnExiting(sender, args);       
        
        World.Destroy(World);
    }

    protected override void Update(GameTime gameTime)
    {
        var deltaTime = gameTime.ElapsedGameTime.TotalSeconds;

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
        
        SpriteBatch.Begin();
        base.Draw(gameTime);
        SystemGroup.Render();
        SpriteBatch.End();
    }
}