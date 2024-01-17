using Microsoft.Xna.Framework;

namespace TinyFactory.Engine.NewInput.Composite;

public class TwoAxisComposite: InputComposite<Vector2>
{
    public TwoAxisComposite(ActionsMap actionsMap) : base(actionsMap)
    {
        
    }

    public override Vector2 GetValue()
    {
        throw new System.NotImplementedException();
    }

    public override void Update()
    {
        throw new System.NotImplementedException();
    }
}