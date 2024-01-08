namespace TinyFactory.Engine.Core.Interfaces;

public interface IPreUpdatable
{
    public void BeforeUpdate(in float deltaTime);
}