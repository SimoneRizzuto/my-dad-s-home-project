using Godot;
using System.Collections.Generic;
using System.Linq;
using MyFathersHomeProject.Scripts.Player;
using MyFathersHomeProject.Scripts.Shared.Helpers;
using MyFathersHomeProject.Scripts.Shared.Modules.Interactables;
using MyFathersHomeProject.Scripts.Singletons.SceneSwitcher;

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
    
    public (InteractableModule? closest, List<InteractableModule> others) FindClosestInteractableModule()
    {
        var oliver = Oliver.Instance;
        InteractableModule closestInteractableModule = null;
        float closestDistance = float.MaxValue;
        
        List<InteractableModule> interactorModules = GetTree().CurrentScene.GetChildrenRecursive<InteractableModule>();
        
        foreach (var i in interactorModules)
        {
            float distance = oliver.GlobalPosition.DistanceTo(i.GlobalPosition);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestInteractableModule = i;
            }
        }
        
        List<InteractableModule> nonCloestInteractableModules = new List<InteractableModule>(interactorModules);
        if (closestInteractableModule != null) nonCloestInteractableModules.Remove(closestInteractableModule);

        return (closestInteractableModule, nonCloestInteractableModules);
    }
    
    public void AddInRangeInteractable()
    {
        var (closest, _) = FindClosestInteractableModule();
        
        // Set the _inRange variable to true for the closest
        if  (closest == null) return;
        closest.InRange = true;
        closest.ClosestToOliver = true;
    }

    public void RemoveInRangeInteractable()
    {
        var (_, nonclosest) = FindClosestInteractableModule();
        
        // Loop through list of nonclosest and set all _inRange to False
        if (nonclosest.Count == 0) return;
        foreach (var i in nonclosest)
        {
            i.InRange = false; 
            i.ClosestToOliver = false;
        }
    }
}