using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyFactory.Engine.Core;
using TinyFactory.Engine.Input;
using TinyFactory.Engine.Input.Engine;

namespace TinyFactory.Game;

public class CursorController
{
    private Point logicPosition;
    private Vector2 cursorPosition;
    private readonly InputManager inputManager;
    private readonly Camera camera;
    
    public CursorController(InputManager inputManager, Camera camera)
    {
        this.inputManager = inputManager;
        this.camera = camera;
    }
    
    public void Update(float deltaTime)
    {
        var mousePosition = inputManager
            .GetActionMap("Gameplay")
            .GetAction("CursorMovement")
            .GetValue<Vector2>();
        
        var worldMousePosition = Vector2.Transform(mousePosition, Matrix.Invert(camera.Transform));
        
        var mouseGridPosition = new Point((int)MathF.Floor(worldMousePosition.X), (int)MathF.Floor(worldMousePosition.Y));
        
        logicPosition = mouseGridPosition;
        cursorPosition = Vector2.Lerp(cursorPosition, new Vector2(logicPosition.X, logicPosition.Y), deltaTime * 10f);
    }
    
    public void Render(SpriteBatch spriteBatch, Texture2D texture)
    {
        spriteBatch.Draw(
            texture, 
            cursorPosition, 
            null, 
            Color.White, 
            0f, 
            Vector2.Zero, 
            1f / MathF.Max(texture.Width, texture.Height), 
            SpriteEffects.None, 
            0f
        );
    }
}