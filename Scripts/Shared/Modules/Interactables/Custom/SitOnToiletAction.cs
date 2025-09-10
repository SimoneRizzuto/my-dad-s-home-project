using Godot;
using System.Diagnostics;
using DialogueManagerRuntime;
using MyFathersHomeProject.Scripts.Player;
using MyFathersHomeProject.Scripts.Shared.Constants;
using MyFathersHomeProject.Scripts.Shared.Modules.Door;

namespace MyFathersHomeProject.Scripts.Shared.Modules.Interactables.Custom;
[GlobalClass]
public partial class SitOnToiletAction : Node, IAction
{
    // getters
    private DoorModule Door => GetParent().GetParent<DoorModule>();
    private Node2D HallwayBedroom => GetParent().GetParent().GetParent().GetNode<Node2D>("./Walls+Floor/HallwayBedroom");
    private Node2D Fadeout => GetParent().GetParent().GetParent().GetNode<Node2D>("./Walls+Floor/FadeOut");
    
    // variables
    private readonly Stopwatch watch = new();
    
    public void Action()
    {
        DialogueManager.DialogueEnded += ShowOliver;
        
        OpenDoor();
        
        if (Oliver.Instance != null)
        {
            Oliver.Instance.Visible = false;
            Oliver.Instance.SetDirection(Direction.Left);
            
            HallwayBedroom.Visible = false;
            HallwayBedroom.ProcessMode = ProcessModeEnum.Disabled;
            Fadeout.Visible = true;
        }
    }
    
    private void OpenDoor()
    {
        Door.Closed = false;
        watch.Restart();
    }
    
    public override void _Process(double delta)
    {
        if (!Door.Closed && watch.Elapsed.Seconds > 0.25f)
        {
            Door.Closed = true;
            watch.Stop();
        }
    }
    
    private void ShowOliver(Resource dialogueresource)
    {
        if (Oliver.Instance != null)
        {
            OpenDoor();
            
            Oliver.Instance.Visible = true;
            DialogueManager.DialogueEnded -= ShowOliver;
        }
    }
}