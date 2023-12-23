using Arch.Core;

namespace TinyFactory.ECS;

public abstract class BaseSystem : ISystem
{
    protected BaseSystem(World world)
    {
        this.world = world;
    }

    protected World world { get; private set; }

    public virtual void Dispose()
    {
    }

    public virtual void Initialize()
    {
    }

    public virtual void BeforeUpdate(in double deltaTime)
    {
    }

    public virtual void Update(in double deltaTime)
    {
    }

    public virtual void AfterUpdate(in double deltaTime)
    {
    }

    public virtual void Render()
    {
    }
}