using Godot;
using MyFathersHomeProject.Scripts.Dialogue;

namespace MyFathersHomeProject.Scripts.Shared.Modules.Interactables;
public partial class DialogueTriggerAction : Node2D, IAction
{
    [Export] public Resource DialogueResource;
    [Export] public string Title;
    
    public void Action()
    {
        DialogueDirector.Instance.TriggerCutscene(DialogueResource, Title);
    }
}