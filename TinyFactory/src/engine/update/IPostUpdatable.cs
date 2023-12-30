namespace TinyFactory.Engine.update;

public interface IPostUpdatable
{
    public void AfterUpdate(in float deltaTime);
}