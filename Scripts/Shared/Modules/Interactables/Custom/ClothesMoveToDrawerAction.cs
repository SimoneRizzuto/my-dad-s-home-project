using Godot;
using System.Linq;
using MyFathersHomeProject.Scripts.Player;

namespace MyFathersHomeProject.Scripts.Shared.Modules.Interactables.Custom;
public partial class ClothesMoveToDrawerAction : Node2D, IAction
{
    // getters
    private Oliver? Oliver => Oliver.Instance;
    
    public void Action()
    {
        if (Oliver == null) return;
        
        var clothes = Oliver.GetChildren().FirstOrDefault(x => x.Name == "Clothes");
        clothes?.QueueFree();
        
        QueueFree();
    }
}