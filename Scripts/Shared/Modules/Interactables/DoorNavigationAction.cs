using Godot;
using System;
using MyFathersHomeProject.Scripts.Shared.Modules.Door;
using MyFathersHomeProject.Scripts.Singletons.SceneSwitcher;

namespace MyFathersHomeProject.Scripts.Shared.Modules.Interactables;
[GlobalClass]
public partial class DoorNavigationAction : Node, IAction
{
    private DoorModule Door => GetParent().GetParent<DoorModule>();
    
    public void Action()
    {
        Door.Closed = false;
        
        SceneSwitcher.Instance.TransitionToScene(SceneSwitcher.Set1_LivingRoom);
    }
}