using Godot;

namespace MyFathersHomeProject.Scripts.Misc;
public partial class Stars : AnimatedSprite2D
{
    [Signal] public delegate void FinishedFadingInStarsEventHandler();
    
    // const
    private const double IncrementingValue = 0.3d;
    
    // variables
    private bool isFadingIn;
    private double visibility;
    
    public override void _Process(double delta)
    {
        if (!isFadingIn) return;
        
        if (visibility < 1)
        {
            visibility += IncrementingValue * delta;
        }
        else
        {
            EmitSignal(nameof(FinishedFadingInStars));
        }
        
        Modulate = new Color(Modulate.R, Modulate.G, Modulate.B, (float)visibility);
    }
    
    public void TriggerStarsFadingIn()
    {
        isFadingIn = true;
    }
}