using Godot;
using System;
using System.Linq;
using MyFathersHomeProject.Scripts.Menus;
using MyFathersHomeProject.Scripts.Shared.Helpers;
using MyFathersHomeProject.Scripts.Shared.Constants;
using MyFathersHomeProject.Scripts.Singletons.SceneSwitcher;

namespace MyFathersHomeProject.Scenes.Menus;
public partial class NewGameButton : Button
{
	private MenuModule? Menu => GetTree().Root
		.GetChildrenRecursive<MenuModule>()
		.FirstOrDefault();
	
	public override void _Pressed()
	{
		switch (Menu?.MenuMode)
		{
			case MenuMode.MainMenu:
				// Load the initial Transition Scene for the first set
				SceneSwitcher.Instance?.TransitionToScene(SceneSwitcher.Set1TransitionScene);
				
				Menu?.ResetPauseMenu();
				GetTree().Paused = false;
				break;
			default:
				// Should never reach this code
				throw new NotImplementedException();
		}
	}

}