using Godot;
using System.Diagnostics;
using System.Threading.Tasks;
using MyFathersHomeProject.Scripts.Character;
using MyFathersHomeProject.Scripts.Dialogue.Base;

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
        
        await SetupActionTask(DialogueDirection.Move,0);
    }
    public async Task MoveRight(int pixels)
    {
        unitsToMove = pixels;
        initialXPosition = (int)CharacterBody.Position.X;
        
        await SetupActionTask(DialogueDirection.Move, 0);
    }
    
    public async Task MoveToXPosition(int xPosition)
    {
        targetXToMoveTo = xPosition;
        initialXPosition = (int)CharacterBody.Position.X;
        
        await SetupActionTask(DialogueDirection.MoveToPosition, 0);
    } 
    
    public async Task Wait(double seconds)
    {
        await SetupActionTask(DialogueDirection.Nothing, seconds);
    } 
    
    #endregion
    
    #region process logic
    
    // async task variables
    public Task ActionCompleted => ActionGiven.Task;
    public TaskCompletionSource ActionGiven { get; set; } = new();
    private double millisecondsToPass = 1000;
    private readonly Stopwatch stopwatch = new();
    
    // state
    private DialogueDirection dialogueDirectionToPlay;
    
    private async Task SetupActionTask(DialogueDirection dialogueDirection, double seconds)
    {
        ActionGiven = new TaskCompletionSource();
        millisecondsToPass = seconds * 1000;
        dialogueDirectionToPlay = dialogueDirection;
        
        await ActionCompleted;
    }
    
    private void FinishTask()
    {
        stopwatch.Stop();
        ActionGiven.TrySetResult();
    }
    
    public override void _PhysicsProcess(double delta)
    {
        if (!stopwatch.IsRunning)
        {
            stopwatch.Restart();
        }
        
        if (stopwatch.ElapsedMilliseconds > millisecondsToPass)
        {
            FinishTask();
        }
        
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
            dialogueDirectionToPlay = DialogueDirection.Nothing;
            ActionGiven.TrySetResult();
        }
    }
    
    #endregion
}
