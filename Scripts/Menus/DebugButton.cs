using Godot;
using System.Linq;
using MyFathersHomeProject.Scripts.Shared.Helpers;

namespace MyFathersHomeProject.Scripts.Menus;
public partial class DebugButton : Button
{
	private MainMenuModule? MainMenu => GetTree().CurrentScene
		.GetChildrenRecursive<MainMenuModule>()
		.FirstOrDefault();
	private PauseMenuModule? PauseMenu => GetTree().Root
		.GetChildrenRecursive<PauseMenuModule>()
		.FirstOrDefault();
	public override void _Pressed()
	{
		// Are we in the main menu?
		if (MainMenu is not null)
		{
			MainMenu?.GoToDebugMenu();
			return;
		}
		
		// Or are we in the pause menu
		if (PauseMenu is not null)
		{
			PauseMenu?.GoToDebugMenu();
		}
		
	}
}