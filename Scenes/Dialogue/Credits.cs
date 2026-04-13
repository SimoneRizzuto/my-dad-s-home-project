using Godot;
using System;

public partial class Credits : Control
{
	[Export] public float ScrollSpeed = 15f;

	private VBoxContainer CreditsContainer = new();
	
	private Timer ScrollTimer = new();

	public override void _Ready()
	{
		CreditsContainer = GetNode<VBoxContainer>("CreditsContainer");
		ScrollTimer.Timeout += ReturnToMainMenu;
	}

	public override void _Process(double delta)
	{
		// Move credits upward
		CreditsContainer.Position -= new Vector2(0, Mathf.Round(ScrollSpeed * (float)delta));

		// Quit or change scene when done
		if (CreditsContainer.Position.Y + CreditsContainer.Size.Y < 0)
		{
			// Start Stopwatch
			ScrollTimer.Start(5.0f);
		}
	}

	private void ReturnToMainMenu()
	{
		
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