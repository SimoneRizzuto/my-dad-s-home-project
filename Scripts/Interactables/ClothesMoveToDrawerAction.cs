using System.Linq;
using Godot;
using MyFathersHomeProject.Scripts.Player;
using MyFathersHomeProject.Scripts.Shared.Modules;

namespace MyFathersHomeProject.Scripts.Interactables;
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