using System;
using System.Collections.Generic;
using TinyFactory.Engine.Core.Interfaces;

namespace TinyFactory.ECS;

public class SystemGroup : ISystem, IInitializable, IPreUpdatable, IUpdatable, IPostUpdatable, IRenderable, IDisposable
{
    protected readonly List<ISystem> Systems = new();

    public SystemGroup(params ISystem[] systems)
    {
        foreach (var system in systems)
            Add(system);
    }

    public void Dispose()
    {
        for (var index = 0; index < Systems.Count; index++)
        {
            var entry = Systems[index];
            if (entry is IDisposable disposable) disposable.Dispose();
        }
    }

    public void Initialize()
    {
        for (var index = 0; index < Systems.Count; index++)
        {
            var entry = Systems[index];
            if (entry is IInitializable initializable) initializable.Initialize();
        }
    }

    public void AfterUpdate(in float deltaTime)
    {
        for (var index = 0; index < Systems.Count; index++)
        {
            var entry = Systems[index];
            if (entry is IPostUpdatable postUpdatable) postUpdatable.AfterUpdate(deltaTime);
        }
    }

    public void BeforeUpdate(in float deltaTime)
    {
        for (var index = 0; index < Systems.Count; index++)
        {
            var entry = Systems[index];
            if (entry is IPreUpdatable preUpdatable) preUpdatable.BeforeUpdate(deltaTime);
        }
    }

    public void Render()
    {
        for (var index = 0; index < Systems.Count; index++)
        {
            var entry = Systems[index];
            if (entry is IRenderable renderable) renderable.Render();
        }
    }

    public void Update(in float deltaTime)
    {
        for (var index = 0; index < Systems.Count; index++)
        {
            var entry = Systems[index];
            if (entry is IUpdatable updatable) updatable.Update(deltaTime);
        }
    }

    public SystemGroup Add(params ISystem[] systems)
    {
        Systems.Capacity = Math.Max(Systems.Capacity, Systems.Count + systems.Length);

        Systems.AddRange(systems);

        return this;
    }
}