using Godot;
using System.Linq;
using MyFathersHomeProject.Scripts.Shared.Helpers;
using MyFathersHomeProject.Scripts.Singletons.SceneSwitcher;

namespace MyFathersHomeProject.Scripts.Menus;
public partial class MainMenuBackButton : Button
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
			MainMenu?.GoToMainMenu();
		}
		
		if (PauseMenu is not null)
		{
			if (MainMenu is null){PauseMenu?.LetsContinueGame();}
			SceneSwitcher.Instance?.TransitionToScene(SceneSwitcher.MainMenuScreen);
		}
	}
}