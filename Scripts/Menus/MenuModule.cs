using Godot;
using MyFathersHomeProject.Scripts.Camera;
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
	private Button OptionsButton => GetNode<Button>("%OptionsButton");
	private Button DebugButton => GetNode<Button>("%DebugButton");
	private Button ExitButton => GetNode<Button>("%ExitButton");
	

	// settings menu
	private Control OptionsMenu => GetNode<Control>("./ColorRect/OptionsMenu");
	private Button TestButton => GetNode<Button>("%TestButton");

	// debug menu
	private Control DebugMenu => GetNode<Control>("./ColorRect/DebugMenu");
	private Button Set1Button => GetNode<Button>("%Set1Button");
	
	// quit menu
	private Control QuitMenu => GetNode<Control>("./ColorRect/QuitMenu");
	private Control QuitBackButton => GetNode<Control>("./ColorRect/QuitMenu/BackButton");
	private Button QuitButton => GetNode<Button>("%QuitButton");
	

	// variables
	private int menuButtonLastFocusIndex = 0;
	private bool mainObservedOnce = false;

	public MenuMode MenuMode;

	public override void _Ready()
	{
		ProcessMode = ProcessModeEnum.Always;
		MenuMode = MenuMode.MainMenu;
		ColorRect.Visible = false;
		PauseLabel.Visible = false;
		MainLabel.Visible = false;
		Menu.Visible = false;
		DebugMenu.Visible = false;
		OptionsMenu.Visible = false;
		QuitMenu.Visible = false;
	}

	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed(InputMapAction.Pause) & (MenuMode == MenuMode.PauseMenu))
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
		SetBackgroundTransparency();
		ExitButton.Text = "Not Now";
		LetsContinueButton.Text = "Go Inside";

		OptionsMenu.FadeOut(NodeExtensions.MenuFadeDefaultTime, () => OptionsMenu.Visible = false);
		DebugMenu.FadeOut(NodeExtensions.MenuFadeDefaultTime, () => DebugMenu.Visible = false);
		QuitMenu.FadeOut(NodeExtensions.MenuFadeDefaultTime, () => QuitMenu.Visible = false);

		VBoxContainerButtonToggle(OptionsMenu, true);
		VBoxContainerButtonToggle(DebugMenu, true);
		VBoxContainerButtonToggle(QuitMenu, true);

		FadeUtil.Instance?.FadeIn(NodeExtensions.MenuFadeInitialiseTime);
		MainLabel.Visible = true;
		Menu.Visible = true;
		ExitButton.Visible = true;
		LetsContinueButton.Visible = true;
		MenuFocus();
		Menu.FadeIn(NodeExtensions.MenuFadeDefaultTime);
		MainLabel.FadeIn(NodeExtensions.MenuFadeDefaultTime);
	}

	

	public void GoToPauseMenu()
	{
		SetBackgroundTransparency();
		ExitButton.Text = "Take a Break";
		LetsContinueButton.Text = "Let's Continue";
		LetsContinueButton.Disabled = false;
		OptionsButton.Disabled = false;
		DebugButton.Disabled = false;
		ExitButton.Disabled = false;


		DebugMenu.FadeOut(NodeExtensions.MenuFadeDefaultTime, () => DebugMenu.Visible = GetTree().Paused);
		OptionsMenu.FadeOut(NodeExtensions.MenuFadeDefaultTime, () => OptionsMenu.Visible = GetTree().Paused);
		QuitMenu.FadeOut(NodeExtensions.MenuFadeDefaultTime, () => QuitMenu.Visible = false);
		
		VBoxContainerButtonToggle(OptionsMenu, true);
		VBoxContainerButtonToggle(DebugMenu, true);
		VBoxContainerButtonToggle(QuitMenu, true);

		ColorRect.FadeIn(NodeExtensions.MenuFadeDefaultTime);
		ColorRect.Visible = true;

		PauseLabel.Visible = true;
		Menu.Visible = true;
		LetsContinueButton.Visible = true;
		MenuFocus();
		Menu.FadeIn(NodeExtensions.MenuFadeDefaultTime);

		PauseLabel.FadeIn(NodeExtensions.MenuFadeDefaultTime);
	}

	public void GoToSettingsMenu()
	{
		VBoxContainerButtonToggle(OptionsMenu, false);
		SetBackgroundTransparency();
		Menu.FadeOut(NodeExtensions.MenuFadeDefaultTime, () => Menu.Visible = false);
		DebugMenu.FadeOut(NodeExtensions.MenuFadeDefaultTime, () => DebugMenu.Visible = false);
		QuitMenu.FadeOut(NodeExtensions.MenuFadeDefaultTime, () => QuitMenu.Visible = false);

		OptionsMenu.Visible = true;
		TestButton.GrabFocus();
		OptionsMenu.FadeIn(NodeExtensions.MenuFadeDefaultTime);

		menuButtonLastFocusIndex = 1;
	}

	public void GoToDebugMenu()
	{
		VBoxContainerButtonToggle(DebugMenu, false);
		SetBackgroundTransparency();
		Menu.FadeOut(NodeExtensions.MenuFadeDefaultTime, () => Menu.Visible = false);
		OptionsMenu.FadeOut(NodeExtensions.MenuFadeDefaultTime, () => OptionsMenu.Visible = false);
		QuitMenu.FadeOut(NodeExtensions.MenuFadeDefaultTime, () => QuitMenu.Visible = false);

		DebugMenu.Visible = true;
		Set1Button.GrabFocus();
		DebugMenu.FadeIn(NodeExtensions.MenuFadeDefaultTime);

		menuButtonLastFocusIndex = 2;
	}

	public void GoToQuitMenu()
	{
		VBoxContainerButtonToggle(QuitMenu, false);
		SetBackgroundTransparency();
		Menu.FadeOut(NodeExtensions.MenuFadeDefaultTime, () => Menu.Visible = false);
		OptionsMenu.FadeOut(NodeExtensions.MenuFadeDefaultTime, () => OptionsMenu.Visible = false);
		DebugMenu.FadeOut(NodeExtensions.MenuFadeDefaultTime, () => DebugMenu.Visible = false);

		QuitMenu.Visible = true;
		QuitBackButton.GrabFocus();
		QuitMenu.FadeIn(NodeExtensions.MenuFadeDefaultTime);

		menuButtonLastFocusIndex = 3;
	}
	
	private void SetBackgroundTransparency()
	{
		if (MenuMode == MenuMode.MainMenu)
		{
			ColorRect.Color = new Color(0, 0, 0, a: 1);
		}
		else
		{
			ColorRect.Color = new Color(0, 0, 0, a: pauseMenuOpacity);
		}
	}

	public void LetsContinueGame()
	{
		ColorRect.FadeOut(NodeExtensions.MenuFadeDefaultTime, () => ColorRect.Visible = GetTree().Paused);

		LetsContinueButton.Disabled = true;
		OptionsButton.Disabled = true;
		DebugButton.Disabled = true;
		ExitButton.Disabled = true;

		menuButtonLastFocusIndex = 0;

		GetTree().Paused = false;
	}

	public void ResetPauseMenu()
	{
		MenuMode = MenuMode.PauseMenu;
		ColorRect.Visible = false;
		PauseLabel.Visible = false;
		MainLabel.Visible = false;
		Menu.Visible = false;
		DebugMenu.Visible = false;
		OptionsMenu.Visible = false;
		QuitMenu.Visible = false;
		menuButtonLastFocusIndex = 0;
	}

	public void ResetMainMenu()
	{
		FadeUtil.Instance?.FadeIn(NodeExtensions.MenuFadeInitialiseTime);
		
		MenuMode = MenuMode.MainMenu;
		ColorRect.Visible = true;
		MainLabel.Visible = true;
		PauseLabel.Visible = false;
		Menu.Visible = true;
		DebugMenu.Visible = false;
		OptionsMenu.Visible = false;
		QuitMenu.Visible = false;
		Visible = true;
		
		menuButtonLastFocusIndex = 0;
	}

	private void MenuFocus()
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
			case 3:
				QuitButton.GrabFocus();
				break;
		}
	}
	
	private void VBoxContainerButtonToggle(Control vBoxMenu, bool toggle)
	{
		foreach (Node child in vBoxMenu.GetChildren())
		{
			if (child is Button button)
			{
				button.Disabled = toggle;
			}
		}
	}
}