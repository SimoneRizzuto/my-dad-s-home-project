using Godot;
using System.Collections.Generic;
using MyFathersHomeProject.Scripts.Player;
using MyFathersHomeProject.Scripts.Shared.Helpers;
using MyFathersHomeProject.Scripts.Shared.Modules.Interactables;

namespace MyFathersHomeProject.Scripts.Singletons.InteractionAllocator;
public partial class InteractionAllocator : Node, IInteractionAllocator
{
	public static InteractionAllocator? Instance { get; private set; }
	
	public override void _Process(double delta)
	{
		var oliver = Oliver.Instance;
		if (oliver == null) return;
		
		AddInRangeInteractable();
		RemoveInRangeInteractable();
	}
	
	public override void _EnterTree()
	{
		Instance = this;
	}
	
	public (InteractableModule closest, List<InteractableModule> others) FindClosestInteractableModule()
	{
		var oliver = Oliver.Instance;
		var closestDistance = float.MaxValue;
		InteractableModule? closestInteractableModule = null;
		
		var interactorModules = GetTree().CurrentScene
			.GetChildrenRecursive<InteractableModule>();
		
		foreach (var i in interactorModules)
		{
			var distance = oliver.GlobalPosition.DistanceTo(i.GlobalPosition);
			
			if (distance < closestDistance)
			{
				closestDistance = distance;
				closestInteractableModule = i;
			}
		}

		var nonClosestInteractableModules = new List<InteractableModule>(interactorModules);
		if (closestInteractableModule != null) nonClosestInteractableModules.Remove(closestInteractableModule);
		
		return (closestInteractableModule, nonClosestInteractableModules);
	}
	
	public void AddInRangeInteractable()
	{
		var (closest, _) = FindClosestInteractableModule();
		
		// Set the _inRange variable to true for the closest
		closest.InRange = true;
		closest.ClosestToOliver = true;
	}
	
	public void RemoveInRangeInteractable()
	{
		var (_, nonclosest) = FindClosestInteractableModule();
		
		// Loop through list of nonclosest and set all _inRange to False
		foreach (var i in nonclosest)
		{
			i.InRange = false;
			i.ClosestToOliver = false;
		}
	}
}