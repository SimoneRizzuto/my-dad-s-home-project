using Godot;
using MyFathersHomeProject.Scripts.Singletons.SceneStates;

namespace MyFathersHomeProject.Scripts.Shared.Modules.Interactables.Custom;
[GlobalClass]
public partial class EnableBackOnComputerTriggerAction : Node, IAction
{
    public void Action()
    {
        if (!SceneStates.Instance.ClothesBeingHeld)
        {
            var backOnComputerTrigger = GetParent().GetParent().GetNode("BackOnComputerTrigger");
            backOnComputerTrigger.ProcessMode = ProcessModeEnum.Inherit;
        }
    }
}