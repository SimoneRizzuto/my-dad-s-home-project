using Godot;

namespace MyFathersHomeProject.Scripts.Singletons.SceneSwitcher;
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
    
    public void AddInRangeInteractable()
    {
        
    }

    public void RemoveInRangeInteractable()
    {
        
    }
}