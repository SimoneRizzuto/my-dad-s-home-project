using Godot;
using MyFathersHomeProject.Scripts.Shared.Helpers;

namespace MyFathersHomeProject.Scripts.Shared.Modules.Interactables.Custom;
[GlobalClass]
public partial class FadeAction : Node, IAction
{
    [Export] public float Time = 5f;
    [Export] public FadeBehaviour FadeBehavior;
    [Export] public string FadePattern = FadePatternConstants.GradientHorizontal;
    
    private bool fadeHasBegun;
    public void Action()
    {
        if (fadeHasBegun) return;
        
        switch (FadeBehavior)
        {
            case FadeBehaviour.In:
                FadeHelper.FadeIn(time: Time, pattern: FadePattern, smooth: true);
                break;
            case FadeBehaviour.Out:
                FadeHelper.FadeOut(time: Time, pattern: FadePattern, smooth: true);
                break;
        }
        
        FadeHelper.OnFinished(Test);
        
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