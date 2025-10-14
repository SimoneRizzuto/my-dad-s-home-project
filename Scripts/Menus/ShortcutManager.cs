using Godot;
using MyFathersHomeProject.Scripts.Shared.Constants;
using MyFathersHomeProject.Scripts.Singletons.SceneSwitcher;

namespace MyFathersHomeProject.Scripts.Menus;
public partial class ShortcutManager : Node2D
{
	[Export] public DisplayServer.WindowMode WindowMode = DisplayServer.WindowMode.Windowed;

	public override void _Ready()
	{
		DisplayServer.WindowSetMode(WindowMode);
	}
	
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed(InputMapAction.FullscreenToggle))
		{
			switch (DisplayServer.WindowGetMode())
			{
				case DisplayServer.WindowMode.ExclusiveFullscreen:
					DisplayServer.WindowSetMode(DisplayServer.WindowMode.Windowed);
					break;
				default:
					DisplayServer.WindowSetMode(DisplayServer.WindowMode.ExclusiveFullscreen);
					break;
			}
		}
		
		DebugShortcuts();
	}
	
	private static void DebugShortcuts()
	{
		if (Input.IsActionJustPressed(InputMapAction.GoToSet1))
		{
			SceneSwitcher.Instance.TransitionToScene(SceneSwitcher.Set1_OnlineWorld);
		}
		
		if (Input.IsActionJustPressed(InputMapAction.GoToSet2))
		{
			SceneSwitcher.Instance.TransitionToScene(SceneSwitcher.Set2_SashaBedroom);
		}
		
		if (Input.IsActionJustPressed(InputMapAction.GoToSet3))
		{
			//SceneSwitcher.Instance.TransitionToScene(SceneSwitcher.Set1_OnlineWorld);
		}
		
		if (Input.IsActionJustPressed(InputMapAction.GoToSet4))
		{
			//SceneSwitcher.Instance.TransitionToScene(SceneSwitcher.Set1_OnlineWorld);
		}
		
		if (Input.IsActionJustPressed(InputMapAction.GoToSet5))
		{
			//SceneSwitcher.Instance.TransitionToScene(SceneSwitcher.Set1_OnlineWorld);
		}
		
		if (Input.IsActionJustPressed(InputMapAction.GoToSet6))
		{
			//SceneSwitcher.Instance.TransitionToScene(SceneSwitcher.Set1_OnlineWorld);
		}
		
		if (Input.IsActionJustPressed(InputMapAction.GoToSet7))
		{
			//SceneSwitcher.Instance.TransitionToScene(SceneSwitcher.Set1_OnlineWorld);
		}
	}
}