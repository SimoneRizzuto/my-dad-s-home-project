using System.Linq;
using Godot;
using MyFathersHomeProject.Scripts.Camera;
using MyFathersHomeProject.Scripts.Shared.Constants;
using MyFathersHomeProject.Scripts.Shared.Extensions;
using MyFathersHomeProject.Scripts.Shared.Helpers;

namespace MyFathersHomeProject.Scripts.Menus;

public partial class PauseMenuModule : CanvasLayer
{
	[Export] private float pauseMenuOpacity = 0.65f;
	
	// color rect
	private ColorRect ColorRect => GetNode<ColorRect>("ColorRect");
	
	// label
	private Label Label => GetNode<Label>("./ColorRect/Label");
	
	// pause menu
	private Control PauseMenu => GetNode<Control>("./ColorRect/PauseMenu");
	private Button LetsContinueButton => GetNode<Button>("%LetsContinueButton");
	private Button OptionsButton => GetNode<Button>("%OptionsButton");
	private Button DebugButton => GetNode<Button>("%DebugButton");
	private Button QuitButton => GetNode<Button>("%QuitButton");
	
	// settings menu
	private Control OptionsMenu => GetNode<Control>("./ColorRect/OptionsMenu");
	private Button TestButton => GetNode<Button>("%TestButton");

	// debug menu
	private Control DebugMenu => GetNode<Control>("./ColorRect/DebugMenu");
	private Button Set1Button => GetNode<Button>("%Set1Button");
	
	// variables
	private int pauseMenuButtonLastFocusIndex = 0;
	private bool mainObservedOnce = false;
	private Node scene;
	
	
	public override void _Ready()
	{
		ProcessMode = ProcessModeEnum.Always;
		ColorRect.Visible = false;
		Label.Visible = false;
		PauseMenu.Visible = false;
		DebugMenu.Visible = false;
		OptionsMenu.Visible = false;
	}
	
	public override void _Process(double delta)
	{
		if (GetTree().CurrentScene.Name == "Main")
		{
			// Running main game as customer OR developer
			if (GetTree().CurrentScene.GetChildren()[0].IsInGroup("Gameplay") == false)
			{
				return;
			}
		}
		else
		{
			// Running individual scene as developer
			if (GetTree().CurrentScene.IsInGroup("Gameplay") == false)
			{
				return;
			}
		}
		
		
		if (Input.IsActionJustPressed(InputMapAction.Pause) && (GetTree().Paused == false))
		{
			TogglePause();
		}
	}
	private void TogglePause()
	{
		if (GetTree().Paused == true) return;
		GetTree().Paused = true;
		//ColorRect.FadeIn(NodeExtensions.menuFadeDefaultTime);
		GoToPauseMenu();

	}
	
	public void GoToPauseMenu()
	{
		DebugMenu.FadeOut(NodeExtensions.menuFadeDefaultTime, () => DebugMenu.Visible = false);
		OptionsMenu.FadeOut(NodeExtensions.menuFadeDefaultTime, () => OptionsMenu.Visible = false);
		ColorRect.FadeIn(NodeExtensions.menuFadeDefaultTime);
		ColorRect.Visible = true;
		Label.Visible = true;
		PauseMenu.Visible = true;
		PauseMenuFocus();
		PauseMenu.FadeIn(NodeExtensions.menuFadeDefaultTime);
		Label.FadeIn(NodeExtensions.menuFadeDefaultTime);
	}
	
	public void GoToSettingsMenu()
	{
		PauseMenu.FadeOut(NodeExtensions.menuFadeDefaultTime, () => PauseMenu.Visible = false);
		DebugMenu.FadeOut(NodeExtensions.menuFadeDefaultTime, () => DebugMenu.Visible = false);
		
		OptionsMenu.Visible = true;
		TestButton.GrabFocus();
		OptionsMenu.FadeIn(NodeExtensions.menuFadeDefaultTime);
		
		pauseMenuButtonLastFocusIndex = 1;
	}
	public void GoToDebugMenu()
	{
		PauseMenu.FadeOut(NodeExtensions.menuFadeDefaultTime, () => PauseMenu.Visible = false);
		OptionsMenu.FadeOut(NodeExtensions.menuFadeDefaultTime, () => OptionsMenu.Visible = false);
		
		DebugMenu.Visible = true;
		Set1Button.GrabFocus();
		DebugMenu.FadeIn(NodeExtensions.menuFadeDefaultTime);
		
		pauseMenuButtonLastFocusIndex = 2;
	}

	public void LetsContinueGame()
	{
		// Issue with the FadeOut Signal not being  realised to finish the tween and so seeing paused screen when want to be moving
		ColorRect.FadeOut(NodeExtensions.menuFadeDefaultTime);
		//ColorRect.Visible = false;
		//Label.Visible = false;
		//PauseMenu.Visible = false;
		//DebugMenu.Visible = false;
		//OptionsMenu.Visible = false;
		pauseMenuButtonLastFocusIndex = 0;
		
		GetTree().Paused = false; 
		
	}

	public void ResetPauseMenu()
	{
		ColorRect.Visible = false;
		Label.Visible = false;
		PauseMenu.Visible = false;
		DebugMenu.Visible = false;
		OptionsMenu.Visible = false;
		pauseMenuButtonLastFocusIndex = 0;
		
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