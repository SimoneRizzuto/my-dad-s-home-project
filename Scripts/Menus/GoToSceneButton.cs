using System.Linq;
using Godot;
using MyFathersHomeProject.Scripts.Shared.Helpers;
using MyFathersHomeProject.Scripts.Singletons.SceneSwitcher;

namespace MyFathersHomeProject.Scripts.Menus;
public partial class GoToSceneButton : Button
{
	[Export] public DebugSceneOptions SceneOptions;
	private MainMenuModule? MainMenu => GetTree().CurrentScene
		.GetChildrenRecursive<MainMenuModule>()
		.FirstOrDefault();
	private PauseMenuModule? PauseMenu => GetTree().Root.GetChildrenRecursive<PauseMenuModule>().FirstOrDefault();
	public override void _Pressed()
	{
		
		switch (SceneOptions)
		{
			case DebugSceneOptions.Set1:
				SceneSwitcher.Instance?.TransitionToScene(SceneSwitcher.Set1_OnlineWorld);
				if (MainMenu is null){PauseMenu?.LetsContinueGame();}
				break;
			case DebugSceneOptions.Set2:
				SceneSwitcher.Instance?.TransitionToScene(SceneSwitcher.Set2_SashaBedroom);
				if (MainMenu is null){PauseMenu?.LetsContinueGame();}
				break;
			case DebugSceneOptions.Set3:
				SceneSwitcher.Instance?.TransitionToScene("");
				break;
			case DebugSceneOptions.Set4:
				SceneSwitcher.Instance?.TransitionToScene("");
				break;
			case DebugSceneOptions.Set5:
				SceneSwitcher.Instance?.TransitionToScene("");
				break;
			case DebugSceneOptions.Set6:
				SceneSwitcher.Instance?.TransitionToScene("");
				break;
			case DebugSceneOptions.Set7:
				SceneSwitcher.Instance?.TransitionToScene("");
				break;
		}
	}
}

public enum DebugSceneOptions
{
	Set1,
	Set2,
	Set3,
	Set4,
	Set5,
	Set6,
	Set7,
}