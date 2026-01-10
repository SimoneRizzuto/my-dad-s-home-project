using Godot;
using MyFathersHomeProject.Scripts.Shared.Constants;
using MyFathersHomeProject.Scripts.Shared.Extensions;

namespace MyFathersHomeProject.Scripts.Menus;

public partial class MenuModule : CanvasLayer
{
	[Export] private float pauseMenuOpacity = 0.65f;

	// color rect
	private ColorRect ColorRect => GetNode<ColorRect>("ColorRect");

	// label
	private Label PauseLabel => GetNode<Label>("./ColorRect/PauseLabel");
	private Label MainLabel => GetNode<Label>("./ColorRect/MainLabel");

	// pause menu
	private Control Menu => GetNode<Control>("./ColorRect/Menu");
	private Button LetsContinueButton => GetNode<Button>("%LetsContinueButton");
	private Button GoInsideButton => GetNode<Button>("%GoInsideButton");
	private Button OptionsButton => GetNode<Button>("%OptionsButton");
	private Button DebugButton => GetNode<Button>("%DebugButton");
	private Button QuitButton => GetNode<Button>("%QuitButton");
	private Button PauseQuitButton => GetNode<Button>("%PauseQuitButton");

	// settings menu
	private Control OptionsMenu => GetNode<Control>("./ColorRect/OptionsMenu");
	private Button TestButton => GetNode<Button>("%TestButton");

	// debug menu
	private Control DebugMenu => GetNode<Control>("./ColorRect/DebugMenu");
	private Button Set1Button => GetNode<Button>("%Set1Button");

	// variables
	private int menuButtonLastFocusIndex = 0;
	private bool mainObservedOnce = false;
	
	private enum MenuMode
	{
		Main,
		Pause
	}
	private MenuMode menuMode;

	public override void _Ready()
	{
		ProcessMode = ProcessModeEnum.Always;
		menuMode = MenuMode.Main;
		ColorRect.Visible = false;
		PauseLabel.Visible = false;
		MainLabel.Visible = false;
		Menu.Visible = false;
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

		if (Input.IsActionJustPressed(InputMapAction.Pause) & (menuMode == MenuMode.Pause))
		{
			TogglePause();
		}
	}

	private void TogglePause()
	{
		GetTree().Paused = !GetTree().Paused;

		if (GetTree().Paused)
		{
			GoToPauseMenu();
		}
		else
		{
			LetsContinueGame();
		}
	}
	
	public void GoToMainMenu()
	{
		OptionsMenu.FadeOut(NodeExtensions.MenuFadeDefaultTime, () => OptionsMenu.Visible = false);
		DebugMenu.FadeOut(NodeExtensions.MenuFadeDefaultTime, () => DebugMenu.Visible = false);
		
		Menu.Visible = true;
		GoInsideButton.Visible = true;
		QuitButton.Visible = true;
		PauseQuitButton.Visible = false;
		LetsContinueButton.Visible = false;
		MainMenuFocus();
		Menu.FadeIn(NodeExtensions.MenuFadeDefaultTime);
	}

	public void GoToPauseMenu()
	{
		LetsContinueButton.Disabled = false;
		OptionsButton.Disabled = false;
		DebugButton.Disabled = false;
		QuitButton.Disabled = false;

		DebugMenu.FadeOut(NodeExtensions.MenuFadeDefaultTime, () => DebugMenu.Visible = GetTree().Paused);
		OptionsMenu.FadeOut(NodeExtensions.MenuFadeDefaultTime, () => OptionsMenu.Visible = GetTree().Paused);

		ColorRect.FadeIn(NodeExtensions.MenuFadeDefaultTime);
		ColorRect.Visible = true;

		PauseLabel.Visible = true;
		Menu.Visible = true;
		PauseMenuFocus();
		Menu.FadeIn(NodeExtensions.MenuFadeDefaultTime);

		PauseLabel.FadeIn(NodeExtensions.MenuFadeDefaultTime);
	}

	public void GoToSettingsMenu()
	{
		Menu.FadeOut(NodeExtensions.MenuFadeDefaultTime, () => Menu.Visible = false);
		DebugMenu.FadeOut(NodeExtensions.MenuFadeDefaultTime, () => DebugMenu.Visible = false);

		OptionsMenu.Visible = true;
		TestButton.GrabFocus();
		OptionsMenu.FadeIn(NodeExtensions.MenuFadeDefaultTime);

		menuButtonLastFocusIndex = 1;
	}

	public void GoToDebugMenu()
	{
		Menu.FadeOut(NodeExtensions.MenuFadeDefaultTime, () => Menu.Visible = false);
		OptionsMenu.FadeOut(NodeExtensions.MenuFadeDefaultTime, () => OptionsMenu.Visible = false);

		DebugMenu.Visible = true;
		Set1Button.GrabFocus();
		DebugMenu.FadeIn(NodeExtensions.MenuFadeDefaultTime);

		menuButtonLastFocusIndex = 2;
	}

	public void LetsContinueGame()
	{
		ColorRect.FadeOut(NodeExtensions.MenuFadeDefaultTime, () => ColorRect.Visible = GetTree().Paused);

		LetsContinueButton.Disabled = true;
		OptionsButton.Disabled = true;
		DebugButton.Disabled = true;
		QuitButton.Disabled = true;

		menuButtonLastFocusIndex = 0;

		GetTree().Paused = false;
	}

	public void ResetPauseMenu()
	{
		ColorRect.Visible = false;
		PauseLabel.Visible = false;
		Menu.Visible = false;
		DebugMenu.Visible = false;
		OptionsMenu.Visible = false;
		menuButtonLastFocusIndex = 0;
	}
	
	private void MainMenuFocus()
	{
		switch (menuButtonLastFocusIndex)
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

	private void PauseMenuFocus()
	{
		switch (menuButtonLastFocusIndex)
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