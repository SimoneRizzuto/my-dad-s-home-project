using System;
using System.Text.RegularExpressions;
using Godot;
using MyFathersHomeProject.Scripts.Shared.Helpers;
using MyFathersHomeProject.Scripts.Shared.Constants;
using MyFathersHomeProject.Scripts.Shared.Signals;

namespace MyFathersHomeProject.Scripts.Shared.Modules.Interactables;

public enum TriggerMode
{
    Collision, 
    Input
}
public partial class ItemInteractable : Node2D, IInteractable
{
    [Signal] public delegate void TriggeredEventHandler();
    [Export] public TriggerMode TriggerMode;

    private Oliver oliver = new();
    private bool isAreaTriggered = false;

    public override void _Ready()
    {
        var tree = GetTree();
        oliver = GetNodeHelper.GetOliver(tree);
        
    }

    public override void _PhysicsProcess(double delta)
    {
        switch (TriggerMode)
        {
            case TriggerMode.Collision:
                if ((isAreaTriggered == true) && (Input.IsActionPressed(InputMapAction.Interact)))
                {
                    Interact();
                }

                return;
            case TriggerMode.Input:
                if (isAreaTriggered == true)
                {
                    Interact();
                }
                return;
        }
        
    }

    public void OnArea2dBodyEntered(Node2D body)
    {
        if (body.IsInGroup(NodeGroup.Oliver))
        {
            isAreaTriggered = true;
        }
        
    }

    public void OnArea2dBodyExited(Node2D body)
    {
        if (body.IsInGroup(NodeGroup.Oliver))
        {
            isAreaTriggered = false;
        }
    }

    public void Interact()
    {
        EmitSignal(nameof(Triggered));
    }

}