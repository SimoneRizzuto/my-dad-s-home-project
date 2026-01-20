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
		if (Menu?.menuMode == MenuModule.MenuMode.PauseMenu)
		{
			//if (Menu == null) return;
			//FadeUtil.Instance?.FadeOut(NodeExtensions.MenuFadeInitialiseTime);
			//await ToSignal(GetTree().CreateTimer(NodeExtensions.MenuFadeInitialiseTime), SceneTreeTimer.SignalName.Timeout);
			
			Menu?.ResetMainMenu();
			Menu?.GoToMainMenu();
		}
		else
		{
			if (FadeUtil.Instance != null)
			{
				FadeUtil.Instance.FadeOut();
				FadeUtil.Instance.FadeFinished += () => GetTree().Quit();
			}
		}
	}
}