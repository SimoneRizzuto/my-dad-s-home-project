using System.Diagnostics;
using Godot;
using MyFathersHomeProject.Scripts.Camera;
using MyFathersHomeProject.Scripts.Shared.Extensions;

namespace MyFathersHomeProject.Scripts.Menus;

public partial class PauseMenuModule : CanvasLayer
{
	[Export] private float pauseMenuOpacity = 0.6f;
	// label
	private Label Label => GetNode<Label>("Label");
	
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
	
	
	public override void _Ready()
	{
		Label.Visible = false;
		PauseMenu.Visible = false;
		DebugMenu.Visible = false;
		OptionsMenu.Visible = false;
	}
	
	public void GoToPauseMenu()
	{
		DebugMenu.FadeOut(NodeExtensions.menuFadeDefaultTime, () => DebugMenu.Visible = false);
		OptionsMenu.FadeOut(NodeExtensions.menuFadeDefaultTime, () => OptionsMenu.Visible = false);
		
		Label.Visible = true;
		PauseMenu.Visible = true;
		PauseMenuFocus();
		PauseMenu.FadeIn(NodeExtensions.menuFadeDefaultTime, finalVal: pauseMenuOpacity);
		Label.FadeIn(NodeExtensions.menuFadeDefaultTime, finalVal: pauseMenuOpacity);
	}
	
	public void GoToSettingsMenu()
	{
		PauseMenu.FadeOut(NodeExtensions.menuFadeDefaultTime, () => PauseMenu.Visible = false);
		DebugMenu.FadeOut(NodeExtensions.menuFadeDefaultTime, () => DebugMenu.Visible = false);
		
		OptionsMenu.Visible = true;
		TestButton.GrabFocus();
		OptionsMenu.FadeIn(NodeExtensions.menuFadeDefaultTime, finalVal: pauseMenuOpacity);
		
		pauseMenuButtonLastFocusIndex = 1;
	}
	public void GoToDebugMenu()
	{
		PauseMenu.FadeOut(NodeExtensions.menuFadeDefaultTime, () => PauseMenu.Visible = false);
		OptionsMenu.FadeOut(NodeExtensions.menuFadeDefaultTime, () => OptionsMenu.Visible = false);
		
		DebugMenu.Visible = true;
		Set1Button.GrabFocus();
		DebugMenu.FadeIn(NodeExtensions.menuFadeDefaultTime, finalVal: pauseMenuOpacity);
		
		pauseMenuButtonLastFocusIndex = 2;
	}

	public async void LetsContinueGame()
	{
		FadeUtil.Instance?.FadeIn(NodeExtensions.menuFadeInitialiseTime);
		
		Label.Visible = false;
		PauseMenu.Visible = false;
		DebugMenu.Visible = false;
		OptionsMenu.Visible = false;
		pauseMenuButtonLastFocusIndex = 0;
		
		
		await ToSignal(GetTree().CreateTimer(NodeExtensions.menuFadeInitialiseTime * 0.8f), SceneTreeTimer.SignalName.Timeout);
		GetTree().Paused = false; 
		
	}

	public void ResetPauseMenu()
	{
		
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