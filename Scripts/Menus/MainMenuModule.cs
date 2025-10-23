using Godot;
using MyFathersHomeProject.Scripts.Camera;
using MyFathersHomeProject.Scripts.Shared.Extensions;

namespace MyFathersHomeProject.Scripts.Menus;
public partial class MainMenuModule : CanvasLayer
{
	// main menu
	private Control MainMenu => GetNode<Control>("MainMenu");
	private Button GoInsideButton => GetNode<Button>("%GoInsideButton");
	private Button OptionsButton => GetNode<Button>("%OptionsButton");
	private Button DebugButton => GetNode<Button>("%DebugButton");
	private Button QuitButton => GetNode<Button>("%QuitButton");
	
	// settings menu
	private Control SettingsMenu => GetNode<Control>("SettingsMenu");
	private Button TestButton => GetNode<Button>("%TestButton");
	
	// debug menu
	private Control DebugMenu => GetNode<Control>("DebugMenu");
	private Button Set1Button => GetNode<Button>("%Set1Button");
	
	// variables
	private int mainMenuButtonLastFocusIndex = 0;
	
	public override void _Ready()
	{
		MainMenu.Visible = false;
		SettingsMenu.Visible = false;
		DebugMenu.Visible = false;
		
		FadeUtil.Instance?.FadeIn(2);
		GoToMainMenu();
	}
	
	public void GoToMainMenu()
	{
		SettingsMenu.FadeOut(0.25d, () => SettingsMenu.Visible = false);
		DebugMenu.FadeOut(0.25d, () => DebugMenu.Visible = false);
		
		MainMenu.Visible = true;
		MainMenuFocus();
		MainMenu.FadeIn(0.25d);
	}
	
	public void GoToSettingsMenu()
	{
		MainMenu.FadeOut(0.25d, () => MainMenu.Visible = false);
		DebugMenu.FadeOut(0.25d, () => DebugMenu.Visible = false);
		
		SettingsMenu.Visible = true;
		TestButton.GrabFocus();
		SettingsMenu.FadeIn(0.25d);
		
		mainMenuButtonLastFocusIndex = 1;
	}
	
	public void GoToDebugMenu()
	{
		MainMenu.FadeOut(0.25d, () => MainMenu.Visible = false);
		SettingsMenu.FadeOut(0.25d, () => SettingsMenu.Visible = false);
		
		DebugMenu.Visible = true;
		Set1Button.GrabFocus();
		DebugMenu.FadeIn(0.25d);
		
		mainMenuButtonLastFocusIndex = 2;
	}
	
	private void MainMenuFocus()
	{
		switch (mainMenuButtonLastFocusIndex)
		{
			case 0:
				GoInsideButton.GrabFocus();
				break;
			case 1:
				OptionsButton.GrabFocus();
				break;
			case 2:
				DebugButton.GrabFocus();
				break;
		}
	}
}