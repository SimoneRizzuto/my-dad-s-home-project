using Godot;
using System;
using MyFathersHomeProject.Scripts.Shared.Modules.Door;
using MyFathersHomeProject.Scripts.Singletons.SceneSwitcher;

namespace MyFathersHomeProject.Scripts.Shared.Modules.Interactables;
[GlobalClass]
public partial class DoorNavigationAction : Node, IAction
{
    [Export] public string NavigateToUid = SceneSwitcher.Set1_LivingRoom;
    
    public void Action()
    {
        var uid = NavigateToUid;
        
        var doorNode = GetParent().GetParent();
        if (doorNode is DoorModule door)
        {
            if (door.Locked) return;
            door.Closed = false;
            
            if (!string.IsNullOrEmpty(door.NavigateToUid))
            {
                uid = door.NavigateToUid;
            }
        }
        
        SceneSwitcher.Instance?.TransitionToScene(uid);
    }
}