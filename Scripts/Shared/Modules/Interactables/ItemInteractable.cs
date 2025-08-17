using Godot;
using MyFathersHomeProject.Scripts.Shared.Constants;

namespace MyFathersHomeProject.Scripts.Shared.Modules.Interactables;
public partial class ItemInteractable : Area2D, IAction
{
    [Signal] public delegate void TriggeredItemEventHandler();
    [Export] public TriggerMode TriggerMode;
    [Export] public bool DisableOnInteract;
    
    private bool InputInteract => Input.IsActionPressed(InputMapAction.Interact);
    
    private bool areaTriggered;
    
    public override void _PhysicsProcess(double delta)
    {
        var triggerInteract = false;
        switch (TriggerMode)
        {
            case TriggerMode.Collision:
                triggerInteract = areaTriggered;
                break;
            case TriggerMode.Input:
                triggerInteract = areaTriggered && InputInteract;
                break;
            case TriggerMode.CollisionEnteredOrExited:
                break;
        }
        
        if (triggerInteract)
        {
            Action();
        }
    }
    
    private void OnBodyEntered(Node2D body)
    {
        switch (TriggerMode)
        {
            case TriggerMode.Collision:
                areaTriggered = body.IsInGroup(NodeGroup.Oliver);
                break;
            case TriggerMode.Input:
                areaTriggered = true;
                break;
            case TriggerMode.CollisionEntered:
            case TriggerMode.CollisionEnteredOrExited:
                Action();
                break;
        }
    }
    
    private void OnBodyExited(Node2D body)
    {
        switch (TriggerMode)
        {
            case TriggerMode.Collision:
                if (body.IsInGroup(NodeGroup.Oliver)) areaTriggered = false;
                break;
            case TriggerMode.Input:
                areaTriggered = false;
                break;
            case TriggerMode.CollisionExited:
            case TriggerMode.CollisionEnteredOrExited:
                Action();
                break;
        }
    }
    
    public virtual void Action()
    {
        EmitSignal(nameof(TriggeredItem));
        if (DisableOnInteract)
        {
            Monitoring = false;
        }
    }
}

public enum TriggerMode
{
    Collision,
    Input,
    CollisionEntered,
    CollisionExited,
    CollisionEnteredOrExited
}