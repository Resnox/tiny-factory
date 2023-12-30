using System;
using System.IO;
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
    private Camera Camera;
    private InputManager InputManager;
    private SpriteBatch SpriteBatch;
    private SystemGroup SystemGroup;
    private Texture2D TestTexture;
    private TextureManager TextureManager;
    private World World;

    public Game()
    {
        Gdm = new GraphicsDeviceManager(this);
        // Typically you would load a config here...
        Gdm.PreferredBackBufferWidth = 480;
        Gdm.PreferredBackBufferHeight = 480;
        Gdm.IsFullScreen = false;
        Window.AllowUserResizing = true;
        Gdm.SynchronizeWithVerticalRetrace = true;
        Content.RootDirectory = "content";
    }

    protected override void Initialize()
    {
        base.Initialize();

        World = World.Create();
        SystemGroup = new SystemGroup();
        InputManager = new InputManager();
        Camera = new Camera(InputManager, GraphicsDevice.Viewport);

        Window.ClientSizeChanged += (sender, args) => { Camera.Viewport = GraphicsDevice.Viewport; };

        for (var i = 0; i <= 250; i++)
        for (var j = 0; j <= 250; j++)
            World.Create(
                new Position
                {
                    PosX = i,
                    PosY = j
                },
                new Sprite
                {
                    TextureIndex = 1
                }
            );

        //SystemGroup.Add(new MovementSystem(World));
        SystemGroup.Add(new SpriteRendererSystem(World, TextureManager, SpriteBatch));
        SystemGroup.Initialize();
    }

    protected override void LoadContent()
    {
        // Load textures, sounds, and so on in here...
        base.LoadContent();

        SpriteBatch = new SpriteBatch(GraphicsDevice);
        TextureManager = new TextureManager(GraphicsDevice);

        var dir = new DirectoryInfo(Content.RootDirectory + "/");
        if (!dir.Exists)
            throw new DirectoryNotFoundException();

        var files = dir.GetFiles("*.*");
        foreach (var file in files)
        {
            var textureName = file.Name.Split('.')[0];
            TextureManager.AddTexture(textureName, Content.Load<Texture2D>(textureName));
        }
    }

    protected override void UnloadContent()
    {
        // Clean up after yourself!
        base.UnloadContent();

        SpriteBatch.Dispose();
        TestTexture?.Dispose();
    }

    protected override void OnExiting(object sender, EventArgs args)
    {
        base.OnExiting(sender, args);

        World.Destroy(World);
    }

    protected override void Update(GameTime gameTime)
    {
        var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        // PreUpdate //
        InputManager.BeforeUpdate();
        SystemGroup.BeforeUpdate(deltaTime);
        Camera.BeforeUpdate(deltaTime);

        // Update //
        base.Update(gameTime);
        Camera.Update(deltaTime);
        SystemGroup.Update(deltaTime);

        // PostUpdate //
        SystemGroup.AfterUpdate(deltaTime);
        InputManager.AfterUpdate();
        Console.WriteLine("FPS: " + (int)(1d / gameTime.ElapsedGameTime.TotalSeconds));
    }

    protected override void Draw(GameTime gameTime)
    {
        // Render stuff in here. Do NOT run game logic in here!
        GraphicsDevice.Clear(Color.CornflowerBlue);
        SpriteBatch.Begin(SpriteSortMode.Deferred,
            BlendState.AlphaBlend,
            null,
            null,
            null,
            null,
            Camera.Transform
        );
        base.Draw(gameTime);
        SystemGroup.Render();
        SpriteBatch.End();
    }
}