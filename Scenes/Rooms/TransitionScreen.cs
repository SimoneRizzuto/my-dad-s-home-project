using Godot;
using System.Diagnostics;
using MyFathersHomeProject.Scripts.Singletons.SceneSwitcher;

namespace MyFathersHomeProject.Scenes.Rooms;
public partial class TransitionScreen : CanvasLayer
{
	[Export] public PackedScene NextScene;
	[Export] public string RichText = "test";
	[Export] public double DelayInitialFade;
	[Export] public double Buffer = 2;
	[Export] public double FadeSpeed = 3;
	[Export] public double FontSize = 24;
	
	// getters
	private Label RichTextLabel => GetNode<Label>("Label");
	
	// variables
	private readonly Stopwatch stopwatch = new();
	private Tween tween;
	private SceneSwitcher sceneSwitcher;
	private bool delayFadeFinished;
	
	public override void _Ready()
	{
		sceneSwitcher = SceneSwitcher.Instance;
		RichTextLabel.Text = RichText;
		RichTextLabel.AddThemeFontSizeOverride("font_size", 24);
		CenterLabelOnScreen(RichTextLabel);
		
		if (DelayInitialFade == 0)
		{
			FadeInLabel();
		}
	}
	
	public override void _Process(double delta)
	{
		if (DelayInitialFade > 0 && !delayFadeFinished)
		{
			if (!stopwatch.IsRunning)
			{
				stopwatch.Restart();
			}
			
			if (stopwatch.Elapsed.TotalSeconds >= DelayInitialFade)
			{
				FadeInLabel();
			}
			
			return;
		}
		
		if (!stopwatch.IsRunning || !(stopwatch.Elapsed.TotalSeconds >= Buffer)) return;
		stopwatch.Stop();
		FadeOutLabel();
	}
	
	private void DoSceneSwitch()
	{
		sceneSwitcher.TransitionToScene(NextScene);
	}
	
	private void FadeOutLabel()
	{
		tween = GetTree().CreateTween();
		tween.TweenProperty(RichTextLabel, "modulate:a", 0.0f, FadeSpeed);
		tween.TweenCallback(Callable.From(DoSceneSwitch));
	}
	
	private void FadeInLabel()
	{
		// Ensure label starts fully transparent
		var color = RichTextLabel.Modulate;
		color.A = 0.0f;
		RichTextLabel.Modulate = color;
		
		tween = GetTree().CreateTween();
		tween.TweenProperty(RichTextLabel, "modulate:a", 1.0f, FadeSpeed);
		tween.TweenCallback(Callable.From(OnFadeInComplete));
		RichTextLabel.Visible = true;
	}
	
	private void OnFadeInComplete()
	{
		stopwatch.Restart();
		delayFadeFinished = true;
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