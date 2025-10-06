using Godot;
using System;
using MyFathersHomeProject.Scripts.Camera;
using MyFathersHomeProject.Scripts.Singletons.SceneSwitcher;

namespace MyFathersHomeProject.Scripts.Shared.Modules.Interactables.Custom;
[GlobalClass]
public partial class FadeAction : Node, IAction
{
    [Export] public float Time = 5f;
    [Export] public FadeBehaviour FadeBehavior;
    [Export] public string FadePattern = FadePatternConstants.GradientHorizontal;
    [Export] public string TransitionToUid = SceneSwitcher.Set1_LivingRoom;
    [Export] public bool TransitionEnabled;
    
    // getters
    private FadeUtil FadeUtil => GetNode<FadeUtil>("/root/FadeUtil");
    
    // variables
    private bool fadeHasBegun;
    
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
        
        FadeUtil.FadeFinished += NavigateToScene;
        
        fadeHasBegun = true;
    }
    
    private void NavigateToScene()
    {
        if (TransitionEnabled)
        {
            SceneSwitcher.Instance.TransitionToScene(TransitionToUid);
        }
        
        FadeUtil.FadeFinished -= NavigateToScene;
    }
}

public enum FadeBehaviour
{
    In,
    Out,
}