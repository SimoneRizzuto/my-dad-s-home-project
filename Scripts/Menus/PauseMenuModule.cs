using Godot;
using MyFathersHomeProject.Scripts.Shared.Extensions;

namespace MyFathersHomeProject.Scripts.Menus;

public partial class PauseMenuModule : CanvasLayer
{
	public static PauseMenuModule? Instance { get; private set;}
	
	
	// pause menu
	private Control PauseMenu => GetNode<Control>("PauseMenu");
	private Button LetsContinueButton => GetNode<Button>("%LetsContinueButton");
	private Button OptionsButton => GetNode<Button>("%OptionsButton");
	private Button DebugButton => GetNode<Button>("%DebugButton");
	private Button QuitButton => GetNode<Button>("%QuitButton");
	
	// settings menu
	private Control OptionsMenu => GetNode<Control>("OptionsMenu");
	private Button TestButton => GetNode<Button>("%TestButton");

	// debug menu
	private Control DebugMenu => GetNode<Control>("DebugMenu");
	private Button Set1Button => GetNode<Button>("%Set1Button");
	
	// variables
	private int pauseMenuButtonLastFocusIndex = 0;
	const double menuFadeDefaultTime = 0.25d;
	
	public override void _Ready()
	{
		Instance = this;
		PauseMenu.Visible = false;
		DebugMenu.Visible = false;
		OptionsMenu.Visible = false;
	}
	
	public void GoToPauseMenu()
	{
		DebugMenu.FadeOut(menuFadeDefaultTime, () => DebugMenu.Visible = false);
		OptionsMenu.FadeOut(menuFadeDefaultTime, () => OptionsMenu.Visible = false);
		
		PauseMenu.Visible = true;
		PauseMenuFocus();
		PauseMenu.FadeIn(menuFadeDefaultTime);
	}
	
	/*public void GoToMainMenu()
{
	OptionsMenu.FadeOut(menuFadeDefaultTime, () => OptionsMenu.Visible = false);
	DebugMenu.FadeOut(menuFadeDefaultTime, () => DebugMenu.Visible = false);

	// Need to scene transition here to MainMenu
	/*MainMenu.Visible = true;
	MainMenuFocus();
	MainMenu.FadeIn(0.25d);#1#
}*/
	
	public void GoToSettingsMenu()
	{
		PauseMenu.FadeOut(menuFadeDefaultTime, () => PauseMenu.Visible = false);
		DebugMenu.FadeOut(menuFadeDefaultTime, () => DebugMenu.Visible = false);
		
		OptionsMenu.Visible = true;
		TestButton.GrabFocus();
		OptionsMenu.FadeIn(menuFadeDefaultTime);
		
		pauseMenuButtonLastFocusIndex = 1;
	}
	public void GoToDebugMenu()
	{
		PauseMenu.FadeOut(menuFadeDefaultTime, () => PauseMenu.Visible = false);
		OptionsMenu.FadeOut(menuFadeDefaultTime, () => OptionsMenu.Visible = false);
		
		DebugMenu.Visible = true;
		Set1Button.GrabFocus();
		DebugMenu.FadeIn(menuFadeDefaultTime);
		
		pauseMenuButtonLastFocusIndex = 2;
	}
	
	private void PauseMenuFocus()
	{
		switch (pauseMenuButtonLastFocusIndex)
		{
			case 0:
				LetsContinueButton.GrabFocus();
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