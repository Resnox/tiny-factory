namespace TinyFactory.Engine.update;

public interface IPreUpdatable
{
    public void BeforeUpdate(in float deltaTime);
}