using Godot;
using System.Linq;
using MyFathersHomeProject.Scripts.Shared.Helpers;

namespace MyFathersHomeProject.Scripts.Menus;
public partial class BackButton : Button
{
	private MenuModule? Menu => GetTree().Root
		.GetChildrenRecursive<MenuModule>()
		.FirstOrDefault();
	public override void _Pressed()
	{
		if (Menu?.menuMode == MenuModule.MenuMode.PauseMenu)
		{
			Menu?.GoToPauseMenu();
		}
		else
		{
			Menu?.GoToMainMenu();
		}
	}
}