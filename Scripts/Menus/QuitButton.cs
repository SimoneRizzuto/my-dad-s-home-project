using System.Linq;
using Godot;
using MyFathersHomeProject.Scripts.Camera;
using MyFathersHomeProject.Scripts.Shared.Constants;
using MyFathersHomeProject.Scripts.Shared.Extensions;
using MyFathersHomeProject.Scripts.Shared.Helpers;
using MyFathersHomeProject.Scripts.Singletons.SceneSwitcher;

namespace MyFathersHomeProject.Scripts.Menus;

public partial class QuitButton : Button
{
	private MenuModule? Menu => GetTree().Root
		.GetChildrenRecursive<MenuModule>()
		.FirstOrDefault();

	public override async void _Pressed()
	{
		switch (Menu?.MenuMode)
		{
			case MenuMode.PauseMenu:
				if (FadeUtil.Instance != null)
				{
					FadeUtil.Instance.FadeOut();
					FadeUtil.Instance.FadeFinished += ReturnToMainMenu;
				}

				break;
			case MenuMode.MainMenu:
				if (FadeUtil.Instance != null)
				{
					FadeUtil.Instance.FadeOut(NodeExtensions.MenuFadeInitialiseTime);
					FadeUtil.Instance.FadeFinished += () => GetTree().Quit();
				}

				break;
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
}