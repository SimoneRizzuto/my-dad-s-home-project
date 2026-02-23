using System.Linq;
using Godot;
using MyFathersHomeProject.Scripts.Camera;
using MyFathersHomeProject.Scripts.Shared.Extensions;
using MyFathersHomeProject.Scripts.Shared.Helpers;
using MyFathersHomeProject.Scripts.Singletons.SceneSwitcher;

namespace MyFathersHomeProject.Scripts.Menus;
public partial class GoToSceneButton : Button
{
	[Export] public DebugSceneOptions SceneOptions;
	private MenuModule? Menu => GetTree().Root
		.GetChildrenRecursive<MenuModule>()
		.FirstOrDefault();
	public override void _Pressed()
	{
		switch (SceneOptions)
		{
			case DebugSceneOptions.Set1:
				SceneSwitcher.Instance?.TransitionToScene(SceneSwitcher.Set1_OnlineWorld);
				ResetPauseMenu();
				break;
			case DebugSceneOptions.Set2:
				SceneSwitcher.Instance?.TransitionToScene(SceneSwitcher.Set2_SashaBedroom);
				ResetPauseMenu();
				break;
			case DebugSceneOptions.Set3:
				SceneSwitcher.Instance?.TransitionToScene(SceneSwitcher.Set2_SashaCampfire);
				ResetPauseMenu();
				break;
			case DebugSceneOptions.Set4:
				SceneSwitcher.Instance?.TransitionToScene("");
				//ResetPauseMenu();
				break;
			case DebugSceneOptions.Set5:
				SceneSwitcher.Instance?.TransitionToScene("");
				//ResetPauseMenu();
				break;
			case DebugSceneOptions.Set6:
				SceneSwitcher.Instance?.TransitionToScene("");
				//ResetPauseMenu();
				break;
			case DebugSceneOptions.Set7:
				SceneSwitcher.Instance?.TransitionToScene("");
				//ResetPauseMenu();
				break;
			case DebugSceneOptions.Teaser1:
				SceneSwitcher.Instance?.TransitionToScene(SceneSwitcher.Teaser1BlackScreen);
				ResetPauseMenu();
				break;
		}
	}

	private void ResetPauseMenu()
	{
		FadeUtil.Instance?.FadeIn(NodeExtensions.MenuFadeInitialiseTime);
		Menu?.ResetPauseMenu();
		GetTree().Paused = false;
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
	Teaser1,
}