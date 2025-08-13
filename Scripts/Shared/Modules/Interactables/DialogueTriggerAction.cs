using Godot;
using MyFathersHomeProject.Scripts.Dialogue;

namespace MyFathersHomeProject.Scripts.Shared.Modules.Interactables;
[GlobalClass]
public partial class DialogueTriggerAction : Node, IAction
{
    [Export] public Resource DialogueResource;
    [Export] public string Title;
    
    public void Action()
    {
        DialogueDirector.Instance.TriggerCutscene(DialogueResource, Title);
    }
}