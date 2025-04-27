using Godot;
using System;
using System.Linq;
using DialogueManagerRuntime;
using MyFathersHomeProject.Scripts.Dialogue;
using MyFathersHomeProject.Scripts.Character;
using MyFathersHomeProject.Scripts.Shared.Constants;

public partial class DialogueDirector : Node2D, IDisposable
{
    [Signal] public delegate void BeginCutsceneEventHandler(string dialogue = "", string title = "");

    private bool inCutscene;
    
    public override void _Ready()
    {
        LoadActorsIntoCurrentScene();
        
        BeginCutscene += TriggerCutscene;
    }
    
    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed(InputMapAction.Debug1))
        {
            TriggerCutscene("test-dialogue", "debug");
        }
    }
    
    private bool LoadActorsIntoCurrentScene()
    {
        var scriptPath = $"res://Scripts/Dialogue/{nameof(CastActors)}.cs";
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
    
    private void ShowDialogueBalloon(string dialogue, string title)
    {
        DialogueManager.ShowDialogueBalloon(GD.Load($"res://Assets/Dialogue/{dialogue}.dialogue"), title);
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
    
    #region signals
    
    private void TriggerCutscene(string dialogue, string title)
    {
        if (inCutscene) return;
        inCutscene = true;
        
        SetActorsCharacterState(CharacterState.Cutscene);
        ShowDialogueBalloon(dialogue, title);
    }
    
    private void FinishCutscene(Resource dialogueResource)
    {
        inCutscene = false;
        
        SetActorsCharacterState(CharacterState.Gameplay);
        DialogueManager.DialogueEnded -= FinishCutscene;
    }
    
    public void Dispose()
    {
        BeginCutscene -= TriggerCutscene;
        DialogueManager.DialogueEnded -= FinishCutscene;
    }
    
    #endregion
}
