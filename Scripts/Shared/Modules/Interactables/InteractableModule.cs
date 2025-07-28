using Godot;
using MyFathersHomeProject.Scripts.Shared.Constants;

namespace MyFathersHomeProject.Scripts.Shared.Modules.Interactables;
public partial class InteractableModule : Area2D
{
	//[Signal] public delegate void TriggeredItemEventHandler();
	[Export] public TriggerMode TriggerMode;
	[Export] public bool DisableOnInteract;
	
	// getters
	private bool InputInteract => Input.IsActionPressed(InputMapAction.Interact);
	
	// variables
	private bool _areaTriggered;
	
	public override void _PhysicsProcess(double delta)
	{
		var triggerInteract = false;
		switch (TriggerMode)
		{
			case TriggerMode.Collision:
				triggerInteract = _areaTriggered;
				break;
			case TriggerMode.Input:
				triggerInteract = _areaTriggered && InputInteract;
				break;
		}
        
		if (triggerInteract)
		{
			Interact();
		}
	}
	
	public void Interact()
	{
		foreach (var child in GetChildren())
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
		switch (TriggerMode)
		{
			case TriggerMode.Collision:
				_areaTriggered = body.IsInGroup(NodeGroup.Oliver);
				break;
			case TriggerMode.Input:
				_areaTriggered = true;
				break;
		}
	}
    
	private void OnBodyExited(Node2D body)
	{
		switch (TriggerMode)
		{
			case TriggerMode.Collision:
				if (body.IsInGroup(NodeGroup.Oliver)) _areaTriggered = false;
				break;
			case TriggerMode.Input:
				_areaTriggered = false;
				break;
		}
	}
}
