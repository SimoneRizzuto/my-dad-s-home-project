using System.Linq;
using Godot;
using MyFathersHomeProject.Scripts.Menus;
using MyFathersHomeProject.Scripts.Shared.Helpers;

namespace MyFathersHomeProject.Scenes.Menus.PauseMenu;

public partial class LetsContinueButton : Button
{
	private MenuModule? Menu => GetTree().Root
		.GetChildrenRecursive<MenuModule>()
		.FirstOrDefault();
	public override void _Pressed()
	{
		if ((Menu?.menuMode == MenuModule.MenuMode.PauseMenu))
		{
			Menu?.LetsContinueGame();
		}
		else
		{
			GD.Print("Some logic for go inside");
		}
	}
}
