using System.Linq;
using Godot;
using MyFathersHomeProject.Scripts.Camera;
using MyFathersHomeProject.Scripts.Menus;
using MyFathersHomeProject.Scripts.Shared.Constants;
using MyFathersHomeProject.Scripts.Shared.Extensions;
using MyFathersHomeProject.Scripts.Shared.Helpers;

namespace MyFathersHomeProject.Scripts.Singletons.Pause;

public partial class Pause : Node
{
	// This is the singleton which checks if pause has been pressed and therefore everything needs to be frozen from here
	// as well as put the pause menu up to use. The pause menu should also only be seen when in game and not in any other 
	// menu. 
	
	// getters
	private PackedScene pausePackedScene;
	private PauseMenuModule pauseMenu;
	private MainMenuModule? mainMenu;
	private bool mainObservedOnce = false; 

	public override void _Ready()
	{
		pausePackedScene = GD.Load<PackedScene>("res://Scenes/Menus/PauseMenu/PauseMenuModule.tscn");
		pauseMenu = pausePackedScene.Instantiate<PauseMenuModule>();
		GetTree().Root.CallDeferred(Node.MethodName.AddChild, pauseMenu);
	}

	public override void _Process(double delta)
	{
		var mainMenu = GetTree().CurrentScene.GetChildrenRecursive<MainMenuModule>().FirstOrDefault();
		if (mainMenu != null)
		{
			mainObservedOnce = true;
			return;
		}
		
		if (Input.IsActionJustPressed(InputMapAction.Pause) && (GetTree().Paused == false))
		{
			TogglePause();
		}
	}
	private void TogglePause()
	{
		if (GetTree().Paused == true || mainObservedOnce == false) return;
		GetTree().Paused = true;
		FadeUtil.Instance?.FadeIn(NodeExtensions.menuFadeInitialiseTime);
		pauseMenu.GoToPauseMenu();

	}
	
}