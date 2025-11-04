using System.Linq;
using Godot;
using MyFathersHomeProject.Scripts.Shared.Helpers;

namespace MyFathersHomeProject.Scripts.Menus;
public partial class OptionsButton : Button
{
	private MainMenuModule? MainMenu => GetTree().CurrentScene
		.GetChildrenRecursive<MainMenuModule>()
		.FirstOrDefault();
	private PauseMenuModule? PauseMenu => GetTree().Root
		.GetChildrenRecursive<PauseMenuModule>()
		.FirstOrDefault();
	public override void _Pressed()
	{
		if (MainMenu is not null)
		{
			MainMenu?.GoToSettingsMenu();
			return;
		}

		if (PauseMenu is not null)
		{
			PauseMenu?.GoToSettingsMenu();
		}
	}
}