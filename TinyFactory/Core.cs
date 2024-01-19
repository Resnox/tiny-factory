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
using TinyFactory.Engine.Input.Enum;
using TinyFactory.Engine.Texture;
using TinyFactory.Game;
using GamePad = TinyFactory.Engine.Input.Engine.GamePad;
using Keyboard = TinyFactory.Engine.Input.Engine.Keyboard;
using XnaGame = Microsoft.Xna.Framework.Game;

namespace TinyFactory;

public class Core : XnaGame
{
    private SystemGroup SystemGroup;
    private World World;

    public Core()
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

    public GraphicsDeviceManager Gdm { get; }
    public Camera Camera { get; private set; }
    public InputManager InputManager { get; private set; }
    public SpriteBatch SpriteBatch { get; private set; }
    public TextureManager TextureManager { get; private set; }
    public CameraController CameraController { get; private set; }

    protected override void Initialize()
    {
        base.Initialize();

        World = World.Create();
        SystemGroup = new SystemGroup();

        Window.ClientSizeChanged += (sender, args) => { Camera.Viewport = GraphicsDevice.Viewport; };

        for (var i = 0; i <= 10; i++)
        for (var j = 0; j <= 10; j++)
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

        InputManager = new InputManager();
        InputManager.RegisterActionMap("Camera").RegisterAction("Move",
            new TwoAxisComposite(
                InputManager.GetEngine<Keyboard>().PressingKey(Keys.Q),
                InputManager.GetEngine<Keyboard>().PressingKey(Keys.D),
                InputManager.GetEngine<Keyboard>().PressingKey(Keys.Z),
                InputManager.GetEngine<Keyboard>().PressingKey(Keys.S)
            ), 
            InputManager.GetEngine<GamePad>().Joystick(GamePadJoystick.LeftStick, PlayerIndex.One)
        );

        Camera = new Camera(this);
        CameraController = new CameraController(InputManager, Camera);
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

        // PostUpdate //
        SystemGroup.AfterUpdate(deltaTime);
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