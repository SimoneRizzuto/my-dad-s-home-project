using Godot;
using System.Linq;
using MyFathersHomeProject.Scripts.Shared.Helpers;

namespace MyFathersHomeProject.Scripts.Menus;
public partial class DebugButton : Button
{
	private MenuModule? Menu => GetTree().CurrentScene
		.GetChildrenRecursive<MenuModule>()
		.FirstOrDefault();

	public override void _Pressed()
	{
		// Are we in the main menu?
		if (Menu is not null)
		{
			Menu?.GoToDebugMenu();

		}
		
		
	}
}