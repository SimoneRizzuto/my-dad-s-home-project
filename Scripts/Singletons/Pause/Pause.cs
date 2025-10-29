using Godot;
using MyFathersHomeProject.Scripts.Menus;
using MyFathersHomeProject.Scripts.Shared.Constants;

namespace MyFathersHomeProject.Scripts.Singletons.Pause;

public partial class Pause : Node
{
	// This is the singleton which checks if pause has been pressed and therefore everything needs to be frozen from here
	// as well as put the pause menu up to use. The pause menu should also only be seen when in game and not in any other 
	// menu. 
	
	// getters
	private bool paused => Input.IsActionPressed(InputMapAction.Pause);
	public static PauseMenuModule? Instance { get; private set;}
	
	// variables 
	const float menuFadeInitialiseTime = 2f;
	public override void _Process(double delta)
	{
		if (!paused)
		{
			return;
			
		}
		
		//FadeUtil.Instance?.FadeIn(menuFadeInitialiseTime);
		//GoToPauseMenu();
	}
}