using System;
using System.Collections.Generic;
using TinyFactory.Engine.Core.Interfaces;

namespace TinyFactory.Engine.ECS;

public class SystemGroup : ISystem, IInitializable, IPreUpdatable, IUpdatable, IPostUpdatable, IRenderable, IDisposable
{
    protected readonly List<ISystem> Systems = new();

    public SystemGroup(params ISystem[] systems)
    {
        foreach (var system in systems)
            Add(system);
    }

    #region IDisposable Members

    public void Dispose()
    {
        for (var index = 0; index < Systems.Count; index++)
        {
            var entry = Systems[index];
            if (entry is IDisposable disposable) disposable.Dispose();
        }
    }

    #endregion

    #region IInitializable Members

    public void Initialize()
    {
        for (var index = 0; index < Systems.Count; index++)
        {
            var entry = Systems[index];
            if (entry is IInitializable initializable) initializable.Initialize();
        }
    }

    #endregion

    #region IPostUpdatable Members

    public void AfterUpdate(in float deltaTime)
    {
        for (var index = 0; index < Systems.Count; index++)
        {
            var entry = Systems[index];
            if (entry is IPostUpdatable postUpdatable) postUpdatable.AfterUpdate(deltaTime);
        }
    }

    #endregion

    #region IPreUpdatable Members

    public void BeforeUpdate(in float deltaTime)
    {
        for (var index = 0; index < Systems.Count; index++)
        {
            var entry = Systems[index];
            if (entry is IPreUpdatable preUpdatable) preUpdatable.BeforeUpdate(deltaTime);
        }
    }

    #endregion

    #region IRenderable Members

    public void Render()
    {
        for (var index = 0; index < Systems.Count; index++)
        {
            var entry = Systems[index];
            if (entry is IRenderable renderable) renderable.Render();
        }
    }

    #endregion

    #region IUpdatable Members

    public void Update(in float deltaTime)
    {
        for (var index = 0; index < Systems.Count; index++)
        {
            var entry = Systems[index];
            if (entry is IUpdatable updatable) updatable.Update(deltaTime);
        }
    }

    #endregion

    public SystemGroup Add(params ISystem[] systems)
    {
        Systems.Capacity = Math.Max(Systems.Capacity, Systems.Count + systems.Length);

        Systems.AddRange(systems);

        return this;
    }
}