using Godot;
using System.Diagnostics;
using MyFathersHomeProject.Scripts.Player;
using MyFathersHomeProject.Scripts.Shared.Modules.Door;

namespace MyFathersHomeProject.Scripts.Shared.Modules.Interactables.Custom;
[GlobalClass]
public partial class SitOnToiletAction : Node, IAction
{
    // getters
    private DoorModule Door => GetParent().GetParent<DoorModule>();
    
    // variables
    private readonly Stopwatch watch = new();
    
    public void Action()
    {
        Door.Closed = false;
        watch.Restart();
        
        if (Oliver.Instance != null)
        {
            Oliver.Instance.Visible = false;
        }
    }
    
    public override void _Process(double delta)
    {
        if (!Door.Closed && watch.Elapsed.Seconds > 0.8f)
        {
            Door.Closed = true;
            watch.Stop();
        }
    }
}