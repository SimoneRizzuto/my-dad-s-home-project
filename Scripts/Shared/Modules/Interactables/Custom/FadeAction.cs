using Godot;
using MyFathersHomeProject.Scripts.Camera;
using MyFathersHomeProject.Scripts.Shared.Helpers;

namespace MyFathersHomeProject.Scripts.Shared.Modules.Interactables.Custom;
[GlobalClass]
public partial class FadeAction : Node, IAction
{
    [Export] public float Time = 5f;
    [Export] public FadeBehaviour FadeBehavior;
    [Export] public string FadePattern = FadePatternConstants.GradientHorizontal;
    
    // getters
    private FadeUtil FadeUtil => GetNode<FadeUtil>("/root/FadeUtil");
    
    // variables
    private bool fadeHasBegun;
    
    public override void _Ready()
    {
        FadeUtil.FadeFinished += Test;
    }
    
    public void Action()
    {
        if (fadeHasBegun) return;
        
        switch (FadeBehavior)
        {
            case FadeBehaviour.In:
                FadeUtil.FadeIn(time: Time, pattern: FadePattern, smooth: true);
                break;
            case FadeBehaviour.Out:
                FadeUtil.FadeOut(time: Time, pattern: FadePattern, smooth: true);
                break;
        }
        
        fadeHasBegun = true;
    }
    
    public void Test()
    {
        GD.Print("the connection worked.");
    }
}

public enum FadeBehaviour
{
    In,
    Out,
}