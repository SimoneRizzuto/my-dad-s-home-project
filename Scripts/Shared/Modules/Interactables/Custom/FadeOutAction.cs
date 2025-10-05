using Godot;
using MyFathersHomeProject.Scripts.Shared.Helpers;

namespace MyFathersHomeProject.Scripts.Shared.Modules.Interactables.Custom;
[GlobalClass]
public partial class FadeOutAction : Node, IAction
{
    private bool fadeHasBegun;
    public void Action()
    {
        if (fadeHasBegun) return;
        
        FadeHelper.FadeOut(time: 5f, pattern: FadePattern.GradientHorizontal, smooth: true);
        fadeHasBegun = true;
    }
}