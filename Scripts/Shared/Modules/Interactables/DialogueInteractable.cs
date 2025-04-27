using Godot;

namespace MyFathersHomeProject.Scripts.Shared.Modules.Interactables;
public partial class DialogueInteractable : ItemInteractable
{
    [Export] public Resource DialogueResource;
    [Export] public string Title;
    
    public override void Interact()
    {
        var path = DialogueResource.ResourcePath;
        
        EmitSignal(nameof(DialogueDirector.BeginCutscene), path, Title);
    }
}