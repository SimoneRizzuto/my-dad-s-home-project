using Godot;
using MyFathersHomeProject.Scripts.Dialogue;

namespace MyFathersHomeProject.Scripts.Shared.Modules.Interactables.Custom;
[GlobalClass]
public partial class PapaCouchLookAheadAction : Node, IAction
{
    public void Action()
    {
        CastCrew.Instance.Papa.PlayAnimation("sit couch right");
    }
}