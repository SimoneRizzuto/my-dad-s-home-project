using Godot;
using MyFathersHomeProject.Scripts.Player;
using MyFathersHomeProject.Scripts.Shared.Constants;

namespace MyFathersHomeProject.Scripts.Shared.Modules.Interactables;
public partial class InteractableModule : Area2D
{
	//[Signal] public delegate void TriggeredItemEventHandler();
	[Export] public TriggerMode TriggerMode;
	[Export] public bool DisableOnInteract;
	
	// getters
	private bool InputInteract => Input.IsActionJustPressed(InputMapAction.Interact);
	
	// variables
	private bool _inRange;
	
	public override void _PhysicsProcess(double delta)
	{
		var triggerInteract = false;
		switch (TriggerMode)
		{
			case TriggerMode.Collision:
				triggerInteract = _inRange;
				break;
			case TriggerMode.Input:
				triggerInteract = _inRange && InputInteract;
				break;
		}
        
		if (triggerInteract)
		{
			Interact();
		}
	}
	
	public void Interact()
	{
		var children = GetChildren();
		foreach (var child in children)
		{
			if (child is IAction actionToTrigger)
			{
				actionToTrigger.Action();
			}
		}

		if (DisableOnInteract)
		{
			Monitoring = false;
		}
	}
	
	// signals
	private void OnBodyEntered(Node2D body)
	{
		if (body is not Oliver) return;
		
		switch (TriggerMode)
		{
			case TriggerMode.Collision:
				_inRange = body.IsInGroup(NodeGroup.Oliver);
				break;
			case TriggerMode.Input:
				_inRange = true;
				break;
		}
	}
    
	private void OnBodyExited(Node2D body)
	{
		if (body is not Oliver) return;
		
		switch (TriggerMode)
		{
			case TriggerMode.Collision:
				if (body.IsInGroup(NodeGroup.Oliver)) _inRange = false;
				break;
			case TriggerMode.Input:
				_inRange = false;
				break;
		}
	}
}
