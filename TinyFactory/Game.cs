using System;
using System.Collections.Generic;
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
    private SpriteBatch SpriteBatch;
    private SystemGroup SystemGroup;
    private Texture2D TestTexture;
    private World World;
    private TextureManager TextureManager;
    private Camera Camera;
    private InputManager InputManager;

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
        SystemGroup = new SystemGroup();
        InputManager = new InputManager();
        Camera = new Camera(InputManager, GraphicsDevice.Viewport.Bounds);

        Window.ClientSizeChanged += (sender, args) =>
        {
            Camera.SetBounds(GraphicsDevice.Viewport.Bounds);
        };

        for (var i = -10; i <= 10; i++)
        {
            for (var j = -10; j <= 10; j++)
            {
                World.Create(
                    new Position
                    {
                        X = i,
                        Y = j
                    },
                    new Sprite
                    {
                        TextureIndex = 1
                    }
                );
            }
        }
        
        SystemGroup.Add(new MovementSystem(World));
        SystemGroup.Add(new SpriteRendererSystem(World, TextureManager, SpriteBatch, Camera));
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
        var deltaTime = gameTime.ElapsedGameTime.TotalSeconds;

        InputManager.BeforeUpdate();
        SystemGroup.BeforeUpdate(deltaTime);

        // Run game logic in here. Do NOT render anything here!
        base.Update(gameTime);

        Camera.Update(deltaTime);
        
        SystemGroup.Update(deltaTime);

        SystemGroup.AfterUpdate(deltaTime);
        InputManager.AfterUpdate();
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