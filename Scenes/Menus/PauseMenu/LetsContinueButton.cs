using System.Linq;
using Godot;
using MyFathersHomeProject.Scripts.Menus;
using MyFathersHomeProject.Scripts.Shared.Helpers;

namespace MyFathersHomeProject.Scenes.Menus.PauseMenu;

public partial class LetsContinueButton : Button
{
	private PauseMenuModule? PauseMenu => GetTree().Root
		.GetChildrenRecursive<PauseMenuModule>()
		.FirstOrDefault();
	public override void _Pressed()
	{
		PauseMenu?.LetsContinueGame();
	}
	
}
