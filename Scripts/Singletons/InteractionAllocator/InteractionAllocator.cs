using System.Collections.Generic;
using Godot;
using MyFathersHomeProject.Scripts.Player;
using MyFathersHomeProject.Scripts.Shared.Modules.Interactables;

namespace MyFathersHomeProject.Scripts.Singletons.SceneSwitcher;
using MyFathersHomeProject.Scripts.Shared.Helpers;
public partial class InteractionAllocator : Node, IInteractionAllocator
{
    
    public static InteractionAllocator Instance { get; private set; }

    public override void _Process(double delta)
    {
        AddInRangeInteractable();
        RemoveInRangeInteractable();
    }

    public override void _EnterTree()
    {
        Instance = this;
    }
    

    public (InteractableModule closest, List<InteractableModule> others) FindClosestInteractableModule()
    {
        var Oliver = GetNodeHelper.GetOliver(GetTree());
        InteractableModule closestInteractableModule = null;
        float closestDistance = float.MaxValue;
        
        List<InteractableModule> interactorModules = GetTree().CurrentScene.GetChildrenRecursive<InteractableModule>();
        

        foreach (var i in interactorModules)
        {
            float distance = Oliver.GlobalPosition.DistanceTo(i.GlobalPosition);

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
        closest._inRange = true;
        closest.closestToOliver = true;

    }

    public void RemoveInRangeInteractable()
    {
        var (_, nonclosest) = FindClosestInteractableModule();
        
        // Loop through list of nonclosest and set all _inRange to False
        foreach (var i in nonclosest)
        {
            i._inRange = false; 
            i.closestToOliver = false;
        }
    }
}