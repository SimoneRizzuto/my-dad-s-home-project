using Godot;
using MyFathersHomeProject.Scripts.Camera;
using MyFathersHomeProject.Scripts.Singletons.SceneSwitcher;

namespace MyFathersHomeProject.Scenes.Dialogue;

public partial class Credits : CanvasLayer
{
	[Export] public float ScrollSpeed = 60f;

	private VBoxContainer CreditsContainer = new();
	
	private Timer ScrollTimer = new();
	private bool Scrolling = false;

	public override void _Ready()
	{
		CreditsContainer = GetNode<VBoxContainer>("CreditsContainer");
		AddChild(ScrollTimer);
		ScrollTimer.Timeout += EndCredits;
	}

	public override void _Process(double delta)
	{
		if (CreditsContainer.Position.Y + CreditsContainer.Size.Y < 100)
		{
			WaitForEffect();
			Scrolling = true;
			return;
		}

		CreditsContainer.Position -= new Vector2(0, ScrollSpeed * (float)delta);
	}

	private void WaitForEffect()
	{
		if (Scrolling) return;
		ScrollTimer.Start(5.0f);
	}

	private void EndCredits()
	{
		RemoveChild(ScrollTimer);
		ScrollTimer.QueueFree();

		if (FadeUtil.Instance != null)
		{
			FadeUtil.Instance.FadeOut(5);
			FadeUtil.Instance.FadeFinished += ReturnToMainMenu;
		}
	}

	private void ReturnToMainMenu()
	{
		SceneSwitcher.Instance?.TransitionToScene(SceneSwitcher.MainMenuTrigger, false);
		if (FadeUtil.Instance != null)
		{
			FadeUtil.Instance.FadeFinished -= ReturnToMainMenu;
		}
	}
}