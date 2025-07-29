using Godot;
using DialogueManagerRuntime;

namespace MyFathersHomeProject.Scripts.Shared.Modules.Interactables.Custom;
public partial class DadKnocksSceneSetupAction : Node, IAction
{
    private AnimatedSprite2D? doorAnimatedSprite;
    
    public void Action()
    {
        doorAnimatedSprite = GetParent().GetParent().GetNode<AnimatedSprite2D>("Objects/Door");
        
        doorAnimatedSprite.Play("open");
        // play door open sound
        
        DialogueManager.DialogueEnded += OnDialogueFinished;
    }
    
    private void OnDialogueFinished(Resource _)
    {
        doorAnimatedSprite?.Play("closed");
        DialogueManager.DialogueEnded -= OnDialogueFinished;
    }
}