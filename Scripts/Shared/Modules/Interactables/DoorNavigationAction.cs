using System.Linq;
using Godot;
using MyFathersHomeProject.Scripts.Player;
using MyFathersHomeProject.Scripts.Shared.Constants;
using MyFathersHomeProject.Scripts.Shared.Helpers;
using MyFathersHomeProject.Scripts.Shared.Modules.Door;
using MyFathersHomeProject.Scripts.Singletons.SceneSwitcher;

namespace MyFathersHomeProject.Scripts.Shared.Modules.Interactables;
[GlobalClass]
public partial class DoorNavigationAction : Node, IAction
{
    [Export] public string NavigateToUid = SceneSwitcher.Set1_LivingRoom;
    [Export] public bool DisableNavigation;
    
    public void Action()
    {
        if (DisableNavigation) return;
        
        var uid = NavigateToUid;

        var doorName = "";
        
        var doorNode = GetParent().GetParent();
        if (doorNode is DoorModule door)
        {
            if (door.Locked) return;
            door.Closed = false;
            doorName = door.DoorName;
            
            if (!string.IsNullOrEmpty(door.NavigateToUid))
            {
                uid = door.NavigateToUid;
            }
        }
        
        SceneSwitcher.Instance?.TransitionToScene(uid);

        var tree = GetTree();
        
        // Find door by group name "door" and the "doorName".
        var doorNodes = tree.GetNodesInGroup(NodeGroup.Door);
        
        // last or default is such a bad idea, but it works, so long as both scenes are spawned at the same time
        var doorToMoveTo = doorNodes.Cast<DoorModule>().LastOrDefault(x => x.DoorName == doorName);
        if (doorToMoveTo == null)
        {
            GD.PrintErr($"{nameof(doorToMoveTo)} was null. DoorName: {doorName}");
            return;
        }
        
        // Find player by group name "player".
        var oliver = Oliver.Instance;
        if (oliver == null)
        {
            GD.PrintErr($"{nameof(oliver)} was null. UID: DoorName: {doorName}");
            return;
        }

        oliver.GlobalPosition = new Vector2(doorToMoveTo.GlobalPosition.X + 14, doorToMoveTo.GlobalPosition.Y);
        oliver.SetDirection(doorToMoveTo.ExitDirection);
    }
}