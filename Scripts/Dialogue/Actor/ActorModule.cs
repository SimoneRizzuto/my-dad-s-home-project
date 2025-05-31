using Godot;
using System.Threading.Tasks;
using MyFathersHomeProject.Scripts.Character;
using MyFathersHomeProject.Scripts.Dialogue.Base;
using MyFathersHomeProject.Scripts.Shared.Constants;

namespace MyFathersHomeProject.Scripts.Dialogue.Actor;
public partial class ActorModule : Node2D, IAsyncDialogueVariables
{
    public CharacterBody2D CharacterBody => GetParentOrNull<CharacterBody2D>();
    public AnimatedSprite2D MainSprite => GetParent().GetNode<AnimatedSprite2D>($"{nameof(MainSprite)}");
    public CollisionShape2D MainShape => GetParent().GetNode<CollisionShape2D>($"{nameof(MainShape)}");
    public ICharacter Character => (ICharacter)CharacterBody;
    
    public void SetCharacterState(CharacterState state)
    {
        Character.CharacterState = state;
    }
    
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
    
    public void PlayAnimation(string animationString)
    {
        Character.Move(0);
        Character.PlayAnimation(animationString);
        dialogueDirectionToPlay = DialogueDirection.Nothing;
    }
    
    public void SetDirection(int direction)
    {
        if (direction < 0) Character.SetDirection(Direction.Left);
        else if (direction > 0) Character.SetDirection(Direction.Right);
    }
    
    #endregion
    
    #region process logic
    
    // async task variables
    public Task ActionCompleted => ActionGiven.Task;
    public TaskCompletionSource ActionGiven { get; set; } = new();
    
    // state
    private DialogueDirection dialogueDirectionToPlay;
    
    private async Task SetupActionTask(DialogueDirection dialogueDirection)
    {
        ActionGiven = new TaskCompletionSource();
        dialogueDirectionToPlay = dialogueDirection;
        
        await ActionCompleted;
    }
    
    public override void _PhysicsProcess(double delta)
    {
        if (Character.CharacterState != CharacterState.Cutscene) return;
        
        switch (dialogueDirectionToPlay)
        {
            case DialogueDirection.Nothing:
                break;
            case DialogueDirection.Idle:
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
            dialogueDirectionToPlay = DialogueDirection.Idle;
            ActionGiven.TrySetResult();
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
            dialogueDirectionToPlay = DialogueDirection.Idle;
            ActionGiven.TrySetResult();
        }
    }
    
    #endregion
}
