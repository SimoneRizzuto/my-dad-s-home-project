using Godot;
using MyFathersHomeProject.Scripts.Camera;
using MyFathersHomeProject.Scripts.Shared.Extensions;
using MyFathersHomeProject.Scripts.Singletons.SceneSwitcher;

public partial class Credits : Control
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
		// Quit or change scene when done
		if (CreditsContainer.Position.Y + CreditsContainer.Size.Y < 100)
		{
			WaitForEffect();
			Scrolling = true;
			return;
		}
		
		// Move credits upward
		CreditsContainer.Position -= new Vector2(0, ScrollSpeed * (float)delta);
		
	}

	private void WaitForEffect()
	{
		if (Scrolling) return;
		// Start Stopwatch
		ScrollTimer.Start(5.0f);
	}

	private void EndCredits()
	{
		RemoveChild(ScrollTimer);
		ScrollTimer.QueueFree();
		
		// Fade Credits & Return to Main Menu
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
	

	/*public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("ui_accept"))
		{
			// Skip credits
			GetTree().Quit();
		}
	}*/
}