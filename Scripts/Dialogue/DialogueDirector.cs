using Godot;
using System;
using System.Linq;
using DialogueManagerRuntime;
using MyFathersHomeProject.Scripts.Dialogue;
using MyFathersHomeProject.Scripts.Character;
using MyFathersHomeProject.Scripts.Shared.Constants;
using MyFathersHomeProject.Scripts.Shared.Helpers;

public partial class DialogueDirector : Node2D, IDisposable
{
    public static DialogueDirector Instance { get; private set; }
    
    private bool inCutscene;
    
    public override void _Ready()
    {
        Instance = this;
        
        LoadActorsIntoCurrentScene();
    }
    
    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed(InputMapAction.Debug1))
        {
            var oliver = GetNodeHelper.GetOliver(GetTree());
            if (oliver.CharacterState == CharacterState.Cutscene) return;
            
            TriggerCutscene(GD.Load("res://Assets/Dialogue/test-dialogue.dialogue"), "debug");
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
