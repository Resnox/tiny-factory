using System;
using Microsoft.Xna.Framework;
using TinyFactory.Engine.Core;
using TinyFactory.Engine.Input;
using TinyFactory.Engine.Input.Value;

namespace TinyFactory.Game;

public class CameraController
{
    private readonly Camera camera;
    private readonly InputManager inputManager;
    private readonly float movementSpeed = 5f;

    public CameraController(InputManager inputManager, Camera camera)
    {
        this.camera = camera;
        this.inputManager = inputManager;
    }

    public void Update(float deltaTime)
    {
        var movement = new TwoAxisInputValue(inputManager, "Left", "Right", "Up", "Down").GetValue();
     
        camera.X += deltaTime * movement.X * movementSpeed;
        camera.Y += deltaTime * movement.Y * movementSpeed;
    }
}