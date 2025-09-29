using System.Collections.Generic;
using Godot;
using MyFathersHomeProject.Scripts.Player;
using MyFathersHomeProject.Scripts.Shared.Modules.Interactables;

namespace MyFathersHomeProject.Scripts.Singletons.SceneSwitcher;
using MyFathersHomeProject.Scripts.Shared.Helpers;
public partial class InteractionAllocator : Node, IInteractionAllocator
{
    
    public static InteractionAllocator Instance { get; private set; }
    
    public override void _EnterTree()
    {
        Instance = this;
    }
    
    private Node? GetMain()
    {
        var tree = GetTree();
        if (tree == null)
        {
            GD.PrintErr("Scene tree is null. InteractionAllocator is being accessed too early.");
            return null;
        }
        return tree.CurrentScene;
    }

    public (InteractableModule closest, List<InteractableModule> others) FindClosestInteractableModule()
    {
        var currentScene = GetMain();

        var Oliver = GetNodeHelper.GetOliver(GetTree());
        InteractableModule closestInteractableModule = null;
        float closestDistance = float.MaxValue;
        
        List<InteractableModule> interactorModules = currentScene.GetChildrenRecursive<InteractableModule>();

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
        
        // Add this module where though?


    }

    public void RemoveInRangeInteractable()
    {
        var (closest, _) = FindClosestInteractableModule();
        
        //Remove this module where though?
    }
}