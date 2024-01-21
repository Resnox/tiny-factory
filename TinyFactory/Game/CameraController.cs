using System;
using Microsoft.Xna.Framework;
using TinyFactory.Engine.Core;
using TinyFactory.Engine.Input;

namespace TinyFactory.Game;

public class CameraController
{
    private readonly Camera camera;
    private readonly InputManager inputManager;
    private readonly float movementSpeed = 5f;
    private readonly float zoomSpeed = 0.5f;
    private float targetZoom;
    
    public CameraController(InputManager inputManager, Camera camera)
    {
        this.camera = camera;
        this.inputManager = inputManager;
        this.targetZoom = this.camera.Zoom;
    }

    public void Update(float deltaTime)
    {
        var movement = inputManager
            .GetActionMap("Camera")
            .GetAction("Move")
            .GetValue<Vector2>();

        camera.Position += deltaTime * movement * movementSpeed;
        
        var zoom = inputManager
            .GetActionMap("Camera")
            .GetAction("Zoom")
            .GetValue<float>();
        
        targetZoom += deltaTime * zoom * zoomSpeed;
        targetZoom = Math.Clamp(targetZoom, Camera.MIN_ZOOM, Camera.MAX_ZOOM);
        
        camera.Zoom = MathHelper.Lerp(camera.Zoom, targetZoom, deltaTime * 5f);
    }
}