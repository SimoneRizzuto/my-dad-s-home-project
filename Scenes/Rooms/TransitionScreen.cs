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
        _richTextLabel = GetNode<Label>("Label");
        _richTextLabel.Text = RichText;
        CenterLabelOnScreen(_richTextLabel);
        FadeInLabel();
    }
    
    public override void _Process(double delta)
    {
        if (!_stopwatch.IsRunning || !(_stopwatch.Elapsed.TotalSeconds >= 2)) return;
        _stopwatch.Stop();
        FadeOutLabel();
    }
    
    
    private void DoSceneSwitch()
    {
        _sceneSwitcher.TransitionToScene(NextScene);
    }

    private void FadeOutLabel()
    {
        _tween = GetTree().CreateTween();
        _tween.TweenProperty(_richTextLabel, "modulate:a", 0.0f, 3.0f);
        _tween.TweenCallback(Callable.From(DoSceneSwitch));
    }
    
    private void FadeInLabel()
    {
        // Ensure label starts fully transparent
        var color = _richTextLabel.Modulate;
        color.A = 0.0f;
        _richTextLabel.Modulate = color;

        _tween = GetTree().CreateTween();
        _tween.TweenProperty(_richTextLabel, "modulate:a", 1.0f, 3.0f);
        _tween.TweenCallback(Callable.From(OnFadeInComplete));
    }

    private void OnFadeInComplete()
    {
        _stopwatch.Start();
    }
    
    private void CenterLabelOnScreen(Label label)
    {
        // This is done in the engine but for some reason not the same results
        label.AnchorLeft = 0.5f;
        label.AnchorTop = 0.5f;
        label.AnchorRight = 0.5f;
        label.AnchorBottom = 0.5f;
        label.OffsetLeft = -label.Size.X / 2;
        label.OffsetTop = -label.Size.Y / 2;
        label.OffsetRight = label.Size.X / 2;
        label.OffsetBottom = label.Size.Y / 2;
        label.HorizontalAlignment = HorizontalAlignment.Center;
        label.VerticalAlignment = VerticalAlignment.Center;

    }
}
