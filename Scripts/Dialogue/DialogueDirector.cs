using Godot;
using System;

public partial class DialogueDirector : Node2D
{
    public override void _Ready()
    {
        LoadActorsIntoCurrentScene();
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
}
