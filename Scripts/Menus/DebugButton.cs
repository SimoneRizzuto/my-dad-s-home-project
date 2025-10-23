using Godot;
using System.Linq;
using MyFathersHomeProject.Scripts.Shared.Helpers;

namespace MyFathersHomeProject.Scripts.Menus;
public partial class DebugButton : Button
{
	private MainMenuModule? MainMenu => GetTree().CurrentScene
		.GetChildrenRecursive<MainMenuModule>()
		.FirstOrDefault();
	public override void _Pressed()
	{
		MainMenu?.GoToDebugMenu();
	}
}