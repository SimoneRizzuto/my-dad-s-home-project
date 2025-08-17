using Godot;

namespace MyFathersHomeProject.Scripts.Shared.Modules.Interactables.Custom;
[GlobalClass]
public partial class BinPedalAction : Node, IAction
{
    private bool isOpen;
    
    public override void _Process(double delta)
    {
        var animationToPlay = "kitchen bin ";
        
        switch (isOpen)
        {
            case true:
                animationToPlay += "open";
                break;
            case false:
                animationToPlay += "closed";
                break;
        }
        
        var binSprite = GetParent().GetParent<AnimatedSprite2D>();
        binSprite.Play(animationToPlay);
    }
    
    public void Action()
    {
        isOpen = !isOpen;
    }
}