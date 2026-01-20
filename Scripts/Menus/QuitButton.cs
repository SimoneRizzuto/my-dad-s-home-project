using System.Linq;
using Godot;
using MyFathersHomeProject.Scripts.Camera;
using MyFathersHomeProject.Scripts.Shared.Extensions;
using MyFathersHomeProject.Scripts.Shared.Helpers;

namespace MyFathersHomeProject.Scripts.Menus;

public partial class QuitButton : Button
{
	private MenuModule? Menu => GetTree().Root
		.GetChildrenRecursive<MenuModule>()
		.FirstOrDefault();

	public override async void _Pressed()
	{
		switch (Menu?.menuMode)
		{
			case MenuModule.MenuMode.PauseMenu:
				if (FadeUtil.Instance != null)
				{
					FadeUtil.Instance.FadeOut(NodeExtensions.MenuFadeInitialiseTime);
					FadeUtil.Instance.FadeFinished += ReturnToMainMenu;
				}
				break;
			case MenuModule.MenuMode.MainMenu:
				if (FadeUtil.Instance != null)
				{
					FadeUtil.Instance.FadeOut();
					FadeUtil.Instance.FadeFinished += () => GetTree().Quit();
				}

				break;
		}
	}

	private void ReturnToMainMenu()
	{
		
		Menu?.ResetMainMenu();
		Menu?.GoToMainMenu();
		if (FadeUtil.Instance != null)
		{
			FadeUtil.Instance.FadeIn(NodeExtensions.MenuFadeInitialiseTime);
			FadeUtil.Instance.FadeFinished -= ReturnToMainMenu;
		}
	}
}