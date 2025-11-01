using Godot;
using MyFathersHomeProject.Scripts.Camera;
using MyFathersHomeProject.Scripts.Menus;
using MyFathersHomeProject.Scripts.Shared.Constants;

namespace MyFathersHomeProject.Scripts.Singletons.Pause;

public partial class Pause : Node
{
	// This is the singleton which checks if pause has been pressed and therefore everything needs to be frozen from here
	// as well as put the pause menu up to use. The pause menu should also only be seen when in game and not in any other 
	// menu. 
	
	// getters
	private bool paused = false; 
	
	// variables 
	const float menuFadeInitialiseTime = 2f;

	public override void _Process(double delta)
	{
		
		if (Input.IsActionJustPressed(InputMapAction.Pause))
		{
			TogglePause();
		}
	}
	private void TogglePause()
	{
		paused = !paused;
		GetTree().Paused = paused;
		FadeUtil.Instance?.FadeIn(menuFadeInitialiseTime);
		PauseMenuModule.Instance?.GoToPauseMenu();
	}
	
}