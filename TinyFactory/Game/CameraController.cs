using Microsoft.Xna.Framework;
using TinyFactory.Engine.Core;
using TinyFactory.Engine.Input;

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
        var movement = inputManager
            .GetActionMap("Camera")
            .GetAction("Move")
            .GetValue<Vector2>();

        camera.X += deltaTime * movement.X * movementSpeed;
        camera.Y += deltaTime * -movement.Y * movementSpeed;
    }
}