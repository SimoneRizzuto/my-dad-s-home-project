using Godot;
using System.Diagnostics;
using MyFathersHomeProject.Scripts.Singletons.SceneSwitcher;

public partial class TransitionScreen : Control
{
    [Export] public PackedScene NextScene;
    [Export] public string RichText;
    
    private Label _richTextLabel = new Label();
    private Tween _tween;
    private SceneSwitcher _sceneSwitcher;
    private Stopwatch _stopwatch = new Stopwatch();

    public override void _Ready()
    {
        _sceneSwitcher = SceneSwitcher.Instance;
        AddChild(_richTextLabel);
        _richTextLabel = GetNode<Label>("RichTextLabel");
        _richTextLabel.Text = RichText;
        
        _stopwatch.Start();
    }
    
    public override void _Process(double delta)
    {
        if (!_stopwatch.IsRunning || !(_stopwatch.Elapsed.TotalSeconds >= 2)) return;
        _stopwatch.Stop();
        CallDeferred(nameof(DoSceneSwitch));
    }
    
    
    private void DoSceneSwitch()
    {
        _sceneSwitcher.TransitionToScene(null, NextScene);
    }

    private void TypeWriterText()
    {
        _tween.TweenProperty(_richTextLabel, "visible_ratio", 1.0, 2.0).From(0.0);
    }
}
