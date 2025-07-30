using Godot;
using DialogueManagerRuntime;
using MyFathersHomeProject.Scripts.Singletons.SceneStates;

namespace MyFathersHomeProject.Scripts.Shared.Modules.Interactables.Custom;
public partial class DadKnocksSceneSetupAction : Node, IAction
{
    private AnimatedSprite2D? doorAnimatedSprite;
    
    public void Action()
    {
        doorAnimatedSprite = GetParent().GetParent().GetNode<AnimatedSprite2D>("Objects/Door");
        
        doorAnimatedSprite.Play("open");
        // play door open sound

        var clothesBeingHeld = SceneStates.Instance.ClothesPickedUp && !SceneStates.Instance.ClothesPutAway;
        if (!clothesBeingHeld)
        {
            DialogueManager.DialogueEnded += OnDialogueFinished;
        }
    }
    
    private void OnDialogueFinished(Resource _)
    {
        doorAnimatedSprite?.Play("closed");
        DialogueManager.DialogueEnded -= OnDialogueFinished;
        
        GetParent().QueueFree();
    }
}