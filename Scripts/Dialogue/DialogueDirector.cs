using Godot;
using System;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;
using DialogueManagerRuntime;
using MyFathersHomeProject.Scripts.Character;
using MyFathersHomeProject.Scripts.Dialogue.Base;
using MyFathersHomeProject.Scripts.Dialogue.Actor;
using MyFathersHomeProject.Scripts.Shared.Constants;

namespace MyFathersHomeProject.Scripts.Dialogue;
public partial class DialogueDirector : Node2D, IAsyncDialogueVariables, IDisposable
{
    public static DialogueDirector Instance { get; private set; }
    
    private bool inCutscene;
    private double millisecondsToPass = 1000;
    public Task ActionCompleted => ActionGiven.Task;
    public TaskCompletionSource ActionGiven { get; set; } = new();
    private readonly Stopwatch stopwatch = new();
    
    public override void _Ready()
    {
        Instance = this;
        
        LoadActorsIntoCurrentScene();
    }
    
    public override void _Process(double delta)
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
    
    private bool LoadActorsIntoCurrentScene()
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
        if (inCutscene) return;
        inCutscene = true;
        
        SetActorsCharacterState(CharacterState.Cutscene);
        ShowDialogueBalloon(dialogueResource, title);
    }
    
    private void ShowDialogueBalloon(Resource dialogueResource, string title)
    {
        DialogueManager.ShowDialogueBalloon(dialogueResource, title);
        DialogueManager.DialogueEnded += FinishCutscene;
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
    
    private async Task SetupActionTask(double seconds)
    {
        ActionGiven = new TaskCompletionSource();
        millisecondsToPass = seconds * 1000;
        
        await ActionCompleted;
    }
    
    private void FinishTask()
    {
        stopwatch.Stop();
        ActionGiven.TrySetResult();
    }
    
    public async Task Wait(double seconds = 1)
    {
        await SetupActionTask(seconds);
    } 
    
    #region signals
    
    private void FinishCutscene(Resource dialogueResource)
    {
        inCutscene = false;
        
        SetActorsCharacterState(CharacterState.Gameplay);
        DialogueManager.DialogueEnded -= FinishCutscene;
    }
    
    public void Dispose()
    {
        DialogueManager.DialogueEnded -= FinishCutscene;
    }
    
    #endregion
}
