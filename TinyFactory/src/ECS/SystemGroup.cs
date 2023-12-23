using System;
using System.Collections.Generic;

namespace TinyFactory.ECS;

public class SystemGroup: ISystem
{
    protected readonly List<ISystem> Systems = new();

    public SystemGroup(params ISystem[] systems)
    {
        foreach (var system in systems)
            Add(system);
    }
    
    public SystemGroup Add(params ISystem[] systems)
    {
        Systems.Capacity = Math.Max(Systems.Capacity, Systems.Count + systems.Length);

        foreach (var system in systems)
            Add(system);

        return this;
    }

    public void Initialize()
    {
        for (var index = 0; index < Systems.Count; index++)
        {
            var entry = Systems[index];
            entry.Initialize();
        }    
    }
    
    public void BeforeUpdate(in double deltaTime)
    {
        for (var index = 0; index < Systems.Count; index++)
        {
            var entry = Systems[index];
            entry.BeforeUpdate(deltaTime);
        }    
    }

    public void Update(in double deltaTime)
    {
        for (var index = 0; index < Systems.Count; index++)
        {
            var entry = Systems[index];
            entry.Update(deltaTime);
        }    
    }

    public void AfterUpdate(in double deltaTime)
    {
        for (var index = 0; index < Systems.Count; index++)
        {
            var entry = Systems[index];
            entry.AfterUpdate(deltaTime);
        }    
    }

    public void Render()
    {
        for (var index = 0; index < Systems.Count; index++)
        {
            var entry = Systems[index];
            entry.Render();
        }    
    }

    public void Dispose()
    {
        for (var index = 0; index < Systems.Count; index++)
        {
            var entry = Systems[index];
            entry.Dispose();
        }    
    }
}