using System.Linq;
using Godot;
using MyFathersHomeProject.Scripts.Camera;
using MyFathersHomeProject.Scripts.Shared.Extensions;
using MyFathersHomeProject.Scripts.Shared.Helpers;
using MyFathersHomeProject.Scripts.Singletons.SceneSwitcher;

namespace MyFathersHomeProject.Scripts.Menus;
public partial class QuitButton : Button
{

	private MenuModule? Menu => GetTree().CurrentScene
		.GetChildrenRecursive<MenuModule>()
		.FirstOrDefault();

	public override async void _Pressed()
	{
		if (Menu?.menuMode == MenuModule.MenuMode.Pause)
		{
			Text = "Take a Break";
			FadeUtil.Instance?.FadeOut(NodeExtensions.MenuFadeInitialiseTime);
			if (Menu is null){Menu?.ResetPauseMenu();}
			await ToSignal(GetTree().CreateTimer(NodeExtensions.MenuFadeInitialiseTime), SceneTreeTimer.SignalName.Timeout);
			GetTree().Paused = false; 
			SceneSwitcher.Instance?.TransitionToScene(SceneSwitcher.MainMenuScreen);
			
		}
		else
		{
			Text = "Not Now";
			if (FadeUtil.Instance != null)
			{
				FadeUtil.Instance.FadeOut();
				FadeUtil.Instance.FadeFinished += () => GetTree().Quit();
			}
		}
		
	}
}