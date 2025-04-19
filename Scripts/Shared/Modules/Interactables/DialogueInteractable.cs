using Godot;
using MyFathersHomeProject.Scripts.Shared.Helpers;
using MyFathersHomeProject.Scripts.Shared.Signals;

namespace MyFathersHomeProject.Scripts.Shared.Modules.Interactables;
public partial class DialogueInteractable : ItemInteractable
{
    [Export] public Resource DialogueScript;
    
    private Oliver oliver = new();
    
    public override void _Ready()
    { 
        var tree = GetTree();
        oliver = GetNodeHelper.GetOliver(tree);
    }

    public void Interact()
    {
        EmitSignal(nameof(CustomSignals.Triggered));
    }
}