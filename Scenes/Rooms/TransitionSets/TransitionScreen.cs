using System.Threading.Tasks;
using Godot;
using MyFathersHomeProject.SaveFiles;
using MyFathersHomeProject.Scripts.Singletons.SceneSwitcher;

namespace MyFathersHomeProject.Scenes.Rooms.TransitionSets;

public partial class TransitionScreen : CanvasLayer
{
	public enum TextMode
	{
		Typewriter = 0,
		Fade = 1,
	}

	[Export] public TextMode Mode = TextMode.Typewriter;
	[Export] public bool TypewriterSoundEffect = true;
	[Export] public float TypeSpeed = 0.1f;
	[Export] public float VisibleTime = 2.0f;
	[Export] public double FadeInDelay;
	[Export] public float FadeOutDelay;

	[Export] public PackedScene NextScene;
	[Export] public string RichText = "test";
	[Export] public double Buffer = 2;
	[Export] public double FadeSpeed = 3;
	[Export] public double FontSize = 24;
	[Export] public int SetId = 0;

	// getters
	private Label RichTextLabel => GetNode<Label>("Label");
	private AnimationPlayer TypewriterAnimation => GetNode<AnimationPlayer>("TypewriterAnimation");
	private AudioStreamPlayer TypewriterSound => GetNode<AudioStreamPlayer>("TypewriterSound");

	// variables
	private Tween tween;

	public override void _Ready()
	{
		RichTextLabel.Text = RichText;
		RichTextLabel.AddThemeFontSizeOverride("font_size", 24);
		CenterLabelOnScreen(RichTextLabel);

		_ = RunSequence();

		if (SetId <= 0) return;
		
		switch (SetId)
		{
			case 1:
				SaveManager.SaveGameData(1);
				break;
			case 2:
				SaveManager.SaveGameData(2);
				break;
			case 3:
				SaveManager.SaveGameData(3);
				break;
			case 4:
				SaveManager.SaveGameData(4);
				break;
			case 5:
				SaveManager.SaveGameData(5);
				break;
			case 6:
				SaveManager.SaveGameData(6);
				break;
			case 7:
				SaveManager.SaveGameData(7);
				break;
		}
	}

	private async Task RunSequence()
	{
		switch (Mode)
		{
			case TextMode.Fade:
				await RunFadeSequence();
				break;

			case TextMode.Typewriter:
				await RunTypewriterSequence();
				break;
		}
	}

	private async Task RunFadeSequence()
	{
		RichTextLabel.VisibleCharacters = -1;
		SetLabelAlpha(0f);

		await Wait(FadeInDelay);

		FadeInLabel();

		await Wait(VisibleTime);
		await Wait(FadeOutDelay);

		FadeOutLabel();
	}

	private async Task RunTypewriterSequence()
	{
		SetLabelAlpha(1f);
		RichTextLabel.VisibleCharacters = 0;

		int length = RichTextLabel.Text.Length;

		for (int i = 0; i < length; i++)
		{
			RichTextLabel.VisibleCharacters++;

			PlayTypewriterSound();

			await Wait(TypeSpeed);
		}

		await Wait(VisibleTime);

		FadeOutLabel();
	}

	private async Task Wait(double seconds)
	{
		await ToSignal(GetTree().CreateTimer(seconds), SceneTreeTimer.SignalName.Timeout);
	}

	private void PlayTypewriterSound()
	{
		if (!TypewriterSoundEffect)
			return;

		// Restart the sound for each character
		TypewriterSound.Stop();
		TypewriterSound.Play();
	}

	private void SetLabelAlpha(float alpha)
	{
		Color c = RichTextLabel.Modulate;
		c.A = alpha;
		RichTextLabel.Modulate = c;
	}


	private void DoSceneSwitch()
	{
		SceneSwitcher.Instance?.TransitionToScene(NextScene);
	}

	private void FadeOutLabel()
	{
		tween = GetTree().CreateTween();
		tween.TweenProperty(RichTextLabel, "modulate:a", 0.0f, FadeSpeed);
		tween.TweenCallback(Callable.From(DoSceneSwitch));
	}

	private void FadeInLabel()
	{
		var color = RichTextLabel.Modulate;
		color.A = 0.0f;
		RichTextLabel.Modulate = color;

		tween = GetTree().CreateTween();
		tween.TweenProperty(RichTextLabel, "modulate:a", 1.0f, FadeSpeed);
		ToSignal(tween, Tween.SignalName.Finished);
		RichTextLabel.Visible = true;
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