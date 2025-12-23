using Godot;
using MyFathersHomeProject.Scripts.Player;
using MyFathersHomeProject.Scripts.Character;
using MyFathersHomeProject.Scripts.Shared.Constants;

namespace MyFathersHomeProject.Scripts.Shared.Modules.Interactables;
public partial class InteractableModule : Area2D
{
	[Export] public TriggerMode TriggerMode;
	[Export] public bool DisableOnInteract;
	
	// getters
	private bool InputInteract => Input.IsActionJustPressed(InputMapAction.Interact);
	
	// variables
	public bool OliverIsColliding { get; private set; }
	public bool ClosestToOliver = false;
	
	public override void _PhysicsProcess(double delta)
	{
		var triggerInteract = false;
		switch (TriggerMode)
		{
			case TriggerMode.Collision:
				triggerInteract = OliverIsColliding;
				break;
			case TriggerMode.Input:
				triggerInteract = InputInteract && OliverIsColliding && ClosestToOliver;
				break;
		}
		
		if (triggerInteract)
		{
			Interact();
		}
	}
	
	public void Interact()
	{
		if (Oliver.Instance?.CharacterState == CharacterState.Cutscene) return;
		
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
				OliverIsColliding = body.IsInGroup(NodeGroup.Oliver);
				break;
			case TriggerMode.Input:
				OliverIsColliding = true;
				break;
			case TriggerMode.CollisionEntered:
			case TriggerMode.CollisionEnteredOrExited:
				Interact();
				break;
		}
	}
	
	private void OnBodyExited(Node2D body)
	{
		if (body is not Oliver) return;
		
		switch (TriggerMode)
		{
			case TriggerMode.Collision:
				if (body.IsInGroup(NodeGroup.Oliver)) OliverIsColliding = false;
				break;
			case TriggerMode.Input:
				OliverIsColliding = false;
				break;
			case TriggerMode.CollisionExited:
			case TriggerMode.CollisionEnteredOrExited:
				Interact();
				break;
		}
	}
}
