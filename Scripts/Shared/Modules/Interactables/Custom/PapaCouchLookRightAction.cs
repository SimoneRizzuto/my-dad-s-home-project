using Godot;
using MyFathersHomeProject.Scripts.Dialogue;

namespace MyFathersHomeProject.Scripts.Shared.Modules.Interactables.Custom;
[GlobalClass]
public partial class PapaCouchLookRightAction : Node, IAction
{
    public void Action()
    {
        CastCrew.Instance.Papa.PlayAnimation("sit couch look right");
    }
}