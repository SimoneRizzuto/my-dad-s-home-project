using Godot;
using System.Linq;
using MyFathersHomeProject.Scripts.Shared.Constants;

namespace MyFathersHomeProject.Scripts.Shared.Modules.Interactables.Custom;
[GlobalClass]
public partial class EnableWindow : Node, IAction
{
    public void Action()
    {
        var window = GetTree().GetNodesInGroup(NodeGroup.Window).FirstOrDefault();
        if (window?.GetChild(0) is InteractableModule module)
        {
            module.ProcessMode = ProcessModeEnum.Inherit;
        }
    }
}