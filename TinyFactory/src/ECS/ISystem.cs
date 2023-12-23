using System;

namespace TinyFactory.ECS;

public interface ISystem : IDisposable
{
    void Initialize();

    void BeforeUpdate(in double deltaTime);

    void Update(in double deltaTime);

    void AfterUpdate(in double deltaTime);

    void Render();
}