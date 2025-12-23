using Godot;
using System.Collections.Generic;
using System.Linq;
using MyFathersHomeProject.Scripts.Player;
using MyFathersHomeProject.Scripts.Shared.Helpers;
using MyFathersHomeProject.Scripts.Shared.Modules.Interactables;

namespace MyFathersHomeProject.Scripts.Singletons.InteractionAllocator;
public partial class InteractionAllocator : Node
{
	public static InteractionAllocator? Instance { get; private set; }
	
	// variables
	public InteractableModule? ClosestInteractable = new();
	
	public override void _EnterTree()
	{
		Instance = this;
	}
	
	public override void _Process(double delta)
	{
		var allInteractables = GetTree().CurrentScene
			.GetChildrenRecursive<InteractableModule>()
			.Where(x => x.TriggerMode == TriggerMode.Input)
			.ToList();
		
		ClearOutInRangeInteractables(allInteractables);
		ProcessClosestInteractable(allInteractables);
	}
	
	private void ProcessClosestInteractable(List<InteractableModule> interactables)
	{
		if (interactables.Count == 0 || Oliver.Instance == null)
		{
			ClosestInteractable = null;
			return;
		}
		
		var oliver = Oliver.Instance;
		var closestDistance = float.MaxValue;
		
		foreach (var interactable in interactables)
		{
			var distance = oliver.GlobalPosition.DistanceTo(interactable.GlobalPosition);
			if (distance < closestDistance)
			{
				closestDistance = distance;
				ClosestInteractable = interactable;
			}
		}
		
		if (ClosestInteractable != null)
		{
			ClosestInteractable.ClosestToOliver = true;
		}
	}
	
	private void ClearOutInRangeInteractables(List<InteractableModule> interactables)
	{
		// Loop through list of nonclosest and set all _inRange to False
		foreach (var interactableModule in interactables)
		{
			interactableModule.ClosestToOliver = false;
		}
	}
}