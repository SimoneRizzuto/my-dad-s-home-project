using Godot;
using System;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;
using DialogueManagerRuntime;
using MyFathersHomeProject.Scripts.Camera;
using MyFathersHomeProject.Scripts.Player;
using MyFathersHomeProject.Scripts.Character;
using MyFathersHomeProject.Scripts.Dialogue.Base;
using MyFathersHomeProject.Scripts.Dialogue.Actor;
using MyFathersHomeProject.Scripts.Shared.Constants;

namespace MyFathersHomeProject.Scripts.Dialogue;
public partial class DialogueDirector : Node2D, IAsyncDialogueVariables, IDisposable
{
    public static DialogueDirector Instance { get; private set; }
    
    private bool _inCutscene;
    
    private double millisecondsToPass = 1000;
    public Task ActionCompleted => ActionGiven.Task;
    public TaskCompletionSource ActionGiven { get; set; } = new();
    private readonly Stopwatch stopwatch = new();
    
    private DirectorDirection _directorDirectionToPlay;
    
    public override void _Ready()
    {
        Instance = this;
        
        DialogueManager.DialogueEnded += FinishCutscene;
        LoadActorsIntoCurrentScene();
    }
    
    public bool LoadActorsIntoCurrentScene()
    {
        var scriptPath = $"res://Scripts/Dialogue/{nameof(CastCrew)}.cs";
        try
        {
            var scriptLoaded = ResourceLoader.Load(scriptPath);
            var scriptAsVariant = (Variant)scriptLoaded;
            
            var currentScene = GetTree().CurrentScene;
            var script = GetTree().CurrentScene.GetScript();
            if (script.Obj != null)
            {
                GD.PrintErr($"Script {script.Obj} already exists in the CurrentScene \"{currentScene.Name}\". Cannot load \"{scriptPath}\" as a script. Actor directions may stop functioning.");
                return false;
            }
            
            GetTree().CurrentScene.SetScript(scriptAsVariant);
        }
        catch (Exception ex)
        {
            GD.PrintErr($"Cannot load \"{scriptPath}\" as a script. Actor directions may stop functioning. ${ex.Message}");
        }
        
        return true;
    }
    
    public void TriggerCutscene(Resource dialogueResource, string title)
    {
        if (_inCutscene) return;
        _inCutscene = true;
        
        SetActorsCharacterState(CharacterState.Cutscene);
        ShowDialogueBalloon(dialogueResource, title);
    }
    
    public void ToggleCameraSmoothing(bool enabled)
    {
        PlayerCamera.Instance.ToggleSmoothing(enabled);
    }
    
    private void ShowDialogueBalloon(Resource dialogueResource, string title)
    {
        DialogueManager.ShowDialogueBalloon(dialogueResource, title);
    }
    
    private void SetActorsCharacterState(CharacterState state)
    {
        var actorModuleNodes = GetTree().GetNodesInGroup(NodeGroup.ActorModule);
        var actorModules = actorModuleNodes.Cast<ActorModule>().ToList();
        
        foreach (var actor in actorModules)
        {
            actor.SetCharacterState(state);
        }
    }
    
    #region cutscene directions
    
    public async Task Wait(double seconds = 1)
    {
        millisecondsToPass = seconds * 1000;
        await SetupActionTask(DirectorDirection.Wait);
    }
    
    #endregion
    
    #region process directions
    
    private async Task SetupActionTask(DirectorDirection dialogueDirection)
    {
        ActionGiven = new TaskCompletionSource();
        _directorDirectionToPlay = dialogueDirection;
        await ActionCompleted;
    }
    
    private void FinishTask()
    {
        stopwatch.Reset();
        _directorDirectionToPlay = DirectorDirection.Nothing;
        ActionGiven.TrySetResult();
    }
    
    public override void _Process(double delta)
    {
        if (Oliver.Instance?.CharacterState != CharacterState.Cutscene) return;
        
        switch (_directorDirectionToPlay)
        {
            case DirectorDirection.Nothing:
                break;
            case DirectorDirection.Wait:
                Process_Wait();
                break;
        }
    }
    
    private void Process_Wait()
    {
        if (!stopwatch.IsRunning)
        {
            stopwatch.Restart();
        }
        
        if (stopwatch.ElapsedMilliseconds > millisecondsToPass)
        {
            FinishTask();
        }
    }
    
    #endregion
    
    #region signals
    
    public void FinishCutscene(Resource dialogueResource)
    {
        _inCutscene = false;
        
        SetActorsCharacterState(CharacterState.Gameplay);
    }
    
    public void Dispose()
    {
        DialogueManager.DialogueEnded -= FinishCutscene;
    }
    
    #endregion
}
