using Arch.Core;

namespace TinyFactory.Engine.ECS;

public abstract class AbstractSystem : ISystem
{
    protected AbstractSystem(World world)
    {
        World = world;
    }

    protected World World { get; private set; }
}