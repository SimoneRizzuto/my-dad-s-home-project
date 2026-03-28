using System;
using System.Linq;
using Godot;
using MyFathersHomeProject.SaveFiles;
using MyFathersHomeProject.Scripts.Camera;
using MyFathersHomeProject.Scripts.Menus;
using MyFathersHomeProject.Scripts.Shared.Constants;
using MyFathersHomeProject.Scripts.Shared.Extensions;
using MyFathersHomeProject.Scripts.Shared.Helpers;
using MyFathersHomeProject.Scripts.Singletons.SceneSwitcher;

public partial class LoadGameButton : Button
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
				
				// Get current scene
				var saveData = SaveManager.LoadGameData();
				var sceneNumber = saveData?.SetId;
				
				// Use Transition Scene
				switch (sceneNumber)
				{
					case 1: 
						SceneSwitcher.Instance?.TransitionToScene(SceneSwitcher.Set1TransitionScene);
						break;
					case 2: 
						SceneSwitcher.Instance?.TransitionToScene(SceneSwitcher.Set2TransitionScene);
						break;
					case 3: 
						SceneSwitcher.Instance?.TransitionToScene(SceneSwitcher.Set3TransitionScene);
						break;
					case 4: 
						SceneSwitcher.Instance?.TransitionToScene(SceneSwitcher.Set4TransitionScene);
						break;
					case 5: 
						SceneSwitcher.Instance?.TransitionToScene(SceneSwitcher.Set5TransitionScene);
						break;
					case 6: 
						SceneSwitcher.Instance?.TransitionToScene(SceneSwitcher.Set6TransitionScene);
						break;
				}
				
				

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
