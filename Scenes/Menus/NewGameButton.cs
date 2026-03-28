using System;
using System.Linq;
using Godot;
using MyFathersHomeProject.Scripts.Camera;
using MyFathersHomeProject.Scripts.Menus;
using MyFathersHomeProject.Scripts.Shared.Constants;
using MyFathersHomeProject.Scripts.Shared.Extensions;
using MyFathersHomeProject.Scripts.Shared.Helpers;
using MyFathersHomeProject.Scripts.Singletons.SceneSwitcher;

public partial class NewGameButton : Button
{
	private MenuModule? Menu => GetTree().Root
		.GetChildrenRecursive<MenuModule>()
		.FirstOrDefault();
	
	public override async void _Pressed()
	{
		switch (Menu?.MenuMode)
		{
			case MenuMode.PauseMenu:
				// Should never reach this code
				throw new NotImplementedException();
			case MenuMode.MainMenu:
				
				// Reset / Hide Menu
				ResetPauseMenu();
				
				// Load the initial Transition Scene for the first set
				SceneSwitcher.Instance?.TransitionToScene(SceneSwitcher.Set1TransitionScene);

				break;
			case null:
				throw new NotImplementedException();
			default:
				throw new ArgumentOutOfRangeException();
		}
	}
	
	private void ResetPauseMenu()
	{
		FadeUtil.Instance?.FadeIn(NodeExtensions.MenuFadeInitialiseTime);
		Menu?.ResetPauseMenu();
		GetTree().Paused = false;
	}

}

