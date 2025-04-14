using System;
using Godot;
using MyFathersHomeProject.Scripts.Shared.Constants;

namespace MyFathersHomeProject.Scripts.Shared.Modules.Interactables;
public partial class ItemInteractable : Area2D, IInteractable
{
    [Signal] public delegate void TriggeredItemEventHandler();
    [Export] public TriggerMode TriggerMode;
    
    private bool areaTriggered = false;
    
    public override void _PhysicsProcess(double delta)
    {
        switch (TriggerMode)
        {
            case TriggerMode.Collision:
                if ((areaTriggered == true) && (Input.IsActionPressed(InputMapAction.Interact)))
                {
                    Interact();
                }

                return;
            case TriggerMode.Input:
                if (areaTriggered == true)
                {
                    Interact();
                }
                return;
        }
    }
    
    private void OnBodyEntered(Node2D body)
    {
        switch (TriggerMode)
        {
            case TriggerMode.Collision:
                if (body.IsInGroup(NodeGroup.Oliver))
                {
                    areaTriggered = true;
                }

                return;
            case TriggerMode.Input:
                areaTriggered = true;
                return;
        }
        
    }
    
    private void OnBodyExited(Node2D body)
    {
        switch (TriggerMode)
        {
            case TriggerMode.Collision:
                if (body.IsInGroup(NodeGroup.Oliver))
                {
                    areaTriggered = false;
                }
                return;
            case TriggerMode.Input:
                areaTriggered = false;
                return;
        }
        
    }
    
    public virtual void Interact()
    {
        EmitSignal(nameof(TriggeredItem));
    }
}

public enum TriggerMode
{
    Collision,
    Input,
}