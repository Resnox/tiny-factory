using Microsoft.Xna.Framework;
using TinyFactory.Engine.Input.Action;

namespace TinyFactory.Engine.Input.Composite;

public class TwoAxisComposite: IInputValue<Vector2>
{
    public IInputValue<float> XAxis { get; set; }
    public IInputValue<float> YAxis { get; set; }
    
    public Vector2 GetValue()
    {
        return new Vector2(
            XAxis.GetValue(),
            YAxis.GetValue()
        );
    }
}