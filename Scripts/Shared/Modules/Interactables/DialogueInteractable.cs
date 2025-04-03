using DialogueManagerRuntime;
using Godot;
using MyFathersHomeProject.Scripts.Shared.Helpers;

namespace MyFathersHomeProject.Scripts.Shared.Modules.Interactables;
public partial class DialogueInteractable : ItemInteractable
{
    [Export] public Resource DialogueScript;
    [Export] public string DialogueStartString;
    
    private Oliver oliver = new();
    
    public override void _Ready()
    { 
        var tree = GetTree();
        oliver = GetNodeHelper.GetOliver(tree);
    }

    public void Interact()
    {
        if (oliver.GetStateMachine().IsInteracting) return;
        
        DialogueManager.ShowDialogueBalloon(DialogueScript, DialogueStartString);
        DialogueManager.DialogueEnded += SetupGameplayAfterDialogueEnded;

        oliver.ToggleInteracting(true);
    }

    private void SetupGameplayAfterDialogueEnded(Resource dialogueResource)
    {
        oliver.SetPlayerState(PlayerState.Idle);
        oliver.ToggleInteracting(false);
        
        DialogueManager.DialogueEnded -= SetupGameplayAfterDialogueEnded;
    }
}