using System;
using System.IO;
using Arch.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TinyFactory.Engine.Core;
using TinyFactory.Engine.ECS;
using TinyFactory.Engine.ECS.Component;
using TinyFactory.Engine.ECS.System;
using TinyFactory.Engine.Input;
using TinyFactory.Engine.Input.Composite;
using TinyFactory.Engine.Texture;
using TinyFactory.Game;
using GamePad = TinyFactory.Engine.Input.Engine.GamePad;
using Keyboard = TinyFactory.Engine.Input.Engine.Keyboard;
using Mouse = TinyFactory.Engine.Input.Engine.Mouse;
using XnaGame = Microsoft.Xna.Framework.Game;

namespace TinyFactory;

public class GameCore : XnaGame
{
    private SystemGroup SystemGroup;
    private World World;

    public GameCore()
    {
        Gdm = new GraphicsDeviceManager(this);
        // Typically you would load a config here...
        Gdm.PreferredBackBufferWidth = 720;
        Gdm.PreferredBackBufferHeight = 405;
        Gdm.IsFullScreen = false;
        Window.AllowUserResizing = true;
        Gdm.SynchronizeWithVerticalRetrace = true;
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    public GraphicsDeviceManager Gdm { get; }
    public Camera Camera { get; private set; }
    public InputManager InputManager { get; private set; }
    public SpriteBatch SpriteBatch { get; private set; }
    public TextureManager TextureManager { get; private set; }
    public CameraController CameraController { get; private set; }
    public CursorController CursorController { get; private set; }

    protected override void Initialize()
    {
        base.Initialize();

        World = World.Create();
        SystemGroup = new SystemGroup();

        Window.ClientSizeChanged += (sender, args) => { Camera.Viewport = GraphicsDevice.Viewport; };

        for (var i = -10; i < 10; i++)
        for (var j = -10; j < 10; j++)
            World.Create(
                new Transform
                {
                    Position = new Vector2(i, j),
                },
                new Sprite
                {
                    TextureIndex = TextureManager.GetTextureIndexByName("tile"),
                }
            );

        //SystemGroup.Add(new MovementSystem(World));
        SystemGroup.Add(new SpriteRendererSystem(World, TextureManager, SpriteBatch));
        SystemGroup.Initialize();

        InputManager = new InputManager();
        InputManager
            .RegisterActionMap("Camera")
            .RegisterAction("Move",
                new TwoAxisComposite(
                    InputManager.GetEngine<Keyboard>().KeyBinding(Keys.Q),
                    InputManager.GetEngine<Keyboard>().KeyBinding(Keys.D),
                    InputManager.GetEngine<Keyboard>().KeyBinding(Keys.Z),
                    InputManager.GetEngine<Keyboard>().KeyBinding(Keys.S)
                )
            )
            .RegisterAction("Zoom",
                InputManager.GetEngine<Mouse>().WheelDelta()
            );

        InputManager
            .RegisterActionMap("Gameplay")
            .RegisterAction("CursorMovement",
                InputManager.GetEngine<Mouse>().MousePosition()
            );

        Camera = new Camera(this);
        CameraController = new CameraController(InputManager, Camera);
        CursorController = new CursorController(InputManager, Camera);
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
        Camera.Update();
        InputManager.Update();
        SystemGroup.BeforeUpdate(deltaTime);

        // Update //
        base.Update(gameTime);
        SystemGroup.Update(deltaTime);
        CameraController.Update(deltaTime);
        CursorController.Update(deltaTime);

        // PostUpdate //
        SystemGroup.AfterUpdate(deltaTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        // Render stuff in here. Do NOT run game logic in here!
        base.Draw(gameTime);
        GraphicsDevice.Clear(Color.Black);
        SpriteBatch.Begin(SpriteSortMode.Deferred,
            BlendState.AlphaBlend,
            SamplerState.PointClamp, 
            null,
            null,
            null,
            Camera.Transform
        );
        SystemGroup.Render();
        CursorController.Render(SpriteBatch, TextureManager.GetTextureByName("cursor"));
        SpriteBatch.End();
    }
}