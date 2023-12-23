using System;
using Arch.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyFactory.Component;
using TinyFactory.ECS;
using TinyFactory.System;
using XnaGame = Microsoft.Xna.Framework.Game;
using JScheduler = JobScheduler.JobScheduler;

namespace TinyFactory;

internal class Game : XnaGame
{
    private readonly GraphicsDeviceManager Gdm;
    private SpriteBatch SpriteBatch;
    private SystemGroup SystemGroup;
    private Texture2D TestTexture;
    private World World;

    public Game()
    {
        Gdm = new GraphicsDeviceManager(this);
        // Typically you would load a config here...
        Gdm.PreferredBackBufferWidth = 1280;
        Gdm.PreferredBackBufferHeight = 720;
        Gdm.IsFullScreen = false;
        Gdm.SynchronizeWithVerticalRetrace = true;

        Content.RootDirectory = "Content";
    }

    protected override void Initialize()
    {
        base.Initialize();

        World = World.Create();

        var random = new Random();
        for (var i = 0; i < 100_000; i++)
            World.Create(
                new Position
                {
                    X = random.NextSingle() * 1280 - 64,
                    Y = random.NextSingle() * 720 - 64
                },
                new Sprite
                {
                    Texture = TestTexture
                }
            );

        SystemGroup = new SystemGroup();
        SystemGroup.Add(new MovementSystem(World));
        SystemGroup.Add(new SpriteRendererSystem(World, SpriteBatch));
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