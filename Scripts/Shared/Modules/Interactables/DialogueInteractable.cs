using Godot;
using MyFathersHomeProject.Scripts.Dialogue;

namespace MyFathersHomeProject.Scripts.Shared.Modules.Interactables;
public partial class DialogueInteractable : ItemInteractable
{
    [Export] public Resource DialogueResource;
    [Export] public string Title;
    
    public override void Interact()
    {
        DialogueDirector.Instance.TriggerCutscene(DialogueResource, Title);
        if (DisableOnInteract)
        {
            Monitoring = false;
        }
    }
}