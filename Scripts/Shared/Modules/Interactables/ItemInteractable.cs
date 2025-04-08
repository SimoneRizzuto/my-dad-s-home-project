using Godot;
using MyFathersHomeProject.Scripts.Shared.Constants;

namespace MyFathersHomeProject.Scripts.Shared.Modules.Interactables;
public partial class ItemInteractable : Area2D, IInteractable
{
    [Signal] public delegate void TriggeredEventHandler();
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
    
    private void OnArea2dBodyEntered(Node2D body)
    {
        if (body.IsInGroup(NodeGroup.Oliver))
        {
            areaTriggered = true;
        }
    }
    
    private void OnArea2dBodyExited(Node2D body)
    {
        if (body.IsInGroup(NodeGroup.Oliver))
        {
            areaTriggered = false;
        }
    }
    
    public void Interact()
    {
        EmitSignal(nameof(Triggered));
    }
}

public enum TriggerMode
{
    Collision,
    Input,
}