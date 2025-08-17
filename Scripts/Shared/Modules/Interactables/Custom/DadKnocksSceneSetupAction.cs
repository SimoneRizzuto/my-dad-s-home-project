using Godot;
using DialogueManagerRuntime;
using MyFathersHomeProject.Scripts.Dialogue;
using MyFathersHomeProject.Scripts.Singletons.SceneStates;

namespace MyFathersHomeProject.Scripts.Shared.Modules.Interactables.Custom;
[GlobalClass]
public partial class DadKnocksSceneSetupAction : Node, IAction
{
    public void Action()
    {
        DialogueManager.DialogueEnded += OnDialogueFinished;
    }
    
    private void OnDialogueFinished(Resource _)
    {
        try
        {
            DialogueDirector.Instance.CloseAllDoors();
            
            if (!SceneStates.Instance.ClothesBeingHeld)
            {
                GetParent().QueueFree();
            }
        }
        finally
        {
            DialogueManager.DialogueEnded -= OnDialogueFinished;
        }
    }
}