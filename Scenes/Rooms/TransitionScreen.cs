using Godot;
using System.Diagnostics;
using MyFathersHomeProject.Scripts.Singletons.SceneSwitcher;

public partial class TransitionScreen : Control
{
    [Export] public PackedScene NextScene;
    [Export] string richText;
    
    private Label RichTextLabel = new Label();
    private Tween tween;
    private SceneSwitcher SceneSwitcher = new();
    private Stopwatch stopwatch = new Stopwatch();

    public override void _Ready()
    {
        RichTextLabel = GetNode<Label>("RichTextLabel");
        RichTextLabel.Text = richText;
        
        stopwatch.Start();
    }
    
    public override void _Process(double delta)
    {
        if (!stopwatch.IsRunning || !(stopwatch.Elapsed.TotalSeconds >= 2)) return;
        stopwatch.Stop();
        SceneSwitcher.TransitionToScene(null, NextScene);
    }

    private void TypeWriterText()
    {
        tween.TweenProperty(RichTextLabel, "visible_ratio", 1.0, 2.0).From(0.0);
    }
}
