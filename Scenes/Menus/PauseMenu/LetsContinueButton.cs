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
		//No PauseMenuModules found in the tree, the count of pausemenumodules is 0.
		GD.Print(GetTree().CurrentScene);
		PauseMenu?.LetsContinueGame();
	}
	
}
