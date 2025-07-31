using Godot;
using System.Linq;
using MyFathersHomeProject.Scripts.Player;
using MyFathersHomeProject.Scripts.Singletons.SceneStates;

namespace MyFathersHomeProject.Scripts.Shared.Modules.Interactables.Custom;
[GlobalClass]
public partial class ClothesMoveToDrawerAction : Node, IAction
{
    // getters
    private Oliver? Oliver => Oliver.Instance;
    
    public void Action()
    {
        if (Oliver == null) return;
        
        var clothes = Oliver.GetChildren().FirstOrDefault(x => x.Name == "Clothes");
        clothes?.QueueFree();
        
        SceneStates.Instance.ClothesPutAway = true;
        
        QueueFree();
    }
}