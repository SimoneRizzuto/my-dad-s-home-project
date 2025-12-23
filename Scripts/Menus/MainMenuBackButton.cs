using Godot;
using System.Linq;
using MyFathersHomeProject.Scripts.Camera;
using MyFathersHomeProject.Scripts.Shared.Extensions;
using MyFathersHomeProject.Scripts.Shared.Helpers;
using MyFathersHomeProject.Scripts.Singletons.SceneSwitcher;

namespace MyFathersHomeProject.Scripts.Menus;
public partial class MainMenuBackButton : Button
{
	private MainMenuModule? MainMenu => GetTree().CurrentScene
		.GetChildrenRecursive<MainMenuModule>()
		.FirstOrDefault();
	private PauseMenuModule? PauseMenu => GetTree().Root
		.GetChildrenRecursive<PauseMenuModule>()
		.FirstOrDefault();
	public override async void _Pressed()
	{
		if (MainMenu is not null)
		{
			MainMenu?.GoToMainMenu();
			return;
		}
		
		if (PauseMenu is not null)
		{
			FadeUtil.Instance?.FadeOut(NodeExtensions.MenuFadeInitialiseTime);
			if (MainMenu is null){PauseMenu?.ResetPauseMenu();}
			await ToSignal(GetTree().CreateTimer(NodeExtensions.MenuFadeInitialiseTime), SceneTreeTimer.SignalName.Timeout);
			GetTree().Paused = false; 
			SceneSwitcher.Instance?.TransitionToScene(SceneSwitcher.MainMenuScreen);
		}
	}
}