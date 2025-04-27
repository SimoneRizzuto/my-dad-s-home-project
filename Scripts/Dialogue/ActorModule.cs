using Godot;
using System.Threading.Tasks;
using MyFathersHomeProject.Scripts.Character;

namespace MyFathersHomeProject.Scripts.Dialogue;
public partial class ActorModule : Node2D
{
    public CharacterBody2D CharacterBody => GetParentOrNull<CharacterBody2D>();
    public AnimatedSprite2D MainSprite => GetParent().GetNode<AnimatedSprite2D>($"{nameof(MainSprite)}");
    public CollisionShape2D MainShape => GetParent().GetNode<CollisionShape2D>($"{nameof(MainShape)}");
    public ICharacter Character => (ICharacter)CharacterBody;
    
    #region cutscene directions
    
    private int unitsToMove;
    private int targetXToMoveTo;
    private int initialXPosition;
    
    public async Task MoveLeft(int pixels)
    {
        unitsToMove = -pixels;
        initialXPosition = (int)CharacterBody.Position.X;
        
        await SetupActionTask(DialogueDirection.Move);
    }
    public async Task MoveRight(int pixels)
    {
        unitsToMove = pixels;
        initialXPosition = (int)CharacterBody.Position.X;
        
        await SetupActionTask(DialogueDirection.Move);
    }
    
    public async Task MoveToXPosition(int xPosition)
    {
        targetXToMoveTo = xPosition;
        initialXPosition = (int)CharacterBody.Position.X;
        
        await SetupActionTask(DialogueDirection.MoveToPosition);
    } 
    
    #endregion
    
    #region process logic
    
    // async task variables
    private Task ActionCompleted => actionGiven.Task;
    private TaskCompletionSource actionGiven = new();
    
    // state
    private DialogueDirection dialogueDirectionToPlay;
    
    private async Task SetupActionTask(DialogueDirection dialogueDirection)
    {
        actionGiven = new TaskCompletionSource();
        dialogueDirectionToPlay = dialogueDirection;
        
        await ActionCompleted;
    }
    
    public override void _PhysicsProcess(double delta)
    {
        if (Character.CharacterState != CharacterState.Cutscene) return;
        
        switch (dialogueDirectionToPlay)
        {
            case DialogueDirection.Nothing:
                Character.Move(0);
                break;
            case DialogueDirection.Move:
                Process_Move();
                break;
            case DialogueDirection.MoveToPosition:
                Process_MoveToPosition();
                break;
        }
    }
    
    private void Process_Move()
    {
        switch (unitsToMove)
        {
            case < 0: Character.Move(-1); // left
                break;
            case > 0: Character.Move(1); // right
                break;
        }
        
        var targetPosition = initialXPosition + unitsToMove;
        var currentPosition = (int)CharacterBody.Position.X;
        
        if (currentPosition == targetPosition)
        {
            dialogueDirectionToPlay = DialogueDirection.Nothing;
            actionGiven.TrySetResult();
        }
    }
    
    private void Process_MoveToPosition()
    {
        var currentPosition = (int)CharacterBody.Position.X;
        if (targetXToMoveTo < currentPosition)
        {
            Character.Move(-1); // left
        }
        else if (targetXToMoveTo > currentPosition)
        {
            Character.Move(1); // right
        }
        else
        {
            dialogueDirectionToPlay = DialogueDirection.Nothing;
            actionGiven.TrySetResult();
        }
    }
    
    #endregion
}
