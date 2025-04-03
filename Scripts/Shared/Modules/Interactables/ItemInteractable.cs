using System;
using Godot;
using MyFathersHomeProject.Scripts.Shared.Helpers;

namespace MyFathersHomeProject.Scripts.Shared.Modules.Interactables;

public enum TriggerMode
{
    Collision, 
    Input
}
public partial class ItemInteractable : Node2D, IInteractable
{
    
    [Export] public TriggerMode TriggerMode;

    private Oliver oliver = new();

    public override void _Ready()
    {
        var tree = GetTree();
        oliver = GetNodeHelper.GetOliver(tree);

        if (TriggerMode == TriggerMode.Collision)
        {
            
        }
    }

    public void Interact()
    {
        throw new NotImplementedException();
    }
}