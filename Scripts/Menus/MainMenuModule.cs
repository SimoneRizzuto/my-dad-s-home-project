using Godot;
using System.Linq;
using System.Collections.Generic;
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
	
	// audio
	private AudioStreamPlayer MenuNavigate_Neutral => GetNode<AudioStreamPlayer>("%MenuNavigate_Neutral");
	
	// variables
	private int mainMenuButtonLastFocusIndex = 0;
	private readonly List<Button> subscribedButtons = new();
	private Control lastFocusedControl;
	private bool suppressNextFocusSound;
	
	public override void _Ready()
	{
		MainMenu.Visible = false;
		SettingsMenu.Visible = false;
		DebugMenu.Visible = false;
		
		SubscribeAllButtons();
		
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
		GrabFocusSilently(TestButton);
		SettingsMenu.FadeIn(0.25d);
		
		mainMenuButtonLastFocusIndex = 1;
	}
	
	public void GoToDebugMenu()
	{
		MainMenu.FadeOut(0.25d, () => MainMenu.Visible = false);
		SettingsMenu.FadeOut(0.25d, () => SettingsMenu.Visible = false);
		
		DebugMenu.Visible = true;
		GrabFocusSilently(Set1Button);
		DebugMenu.FadeIn(0.25d);
		
		mainMenuButtonLastFocusIndex = 2;
	}
	
	private void MainMenuFocus()
	{
		switch (mainMenuButtonLastFocusIndex)
		{
			case 0:
				GrabFocusSilently(GoInsideButton);
				break;
			case 1:
				GrabFocusSilently(OptionsButton);
				break;
			case 2:
				GrabFocusSilently(DebugButton);
				break;
		}
	}
	
	private void SubscribeAllButtons()
	{
		foreach (var node in GetAllChildrenRecursive(this))
		{
			if (node is Button button)
			{
				button.FocusEntered += OnButtonFocusEntered;
				subscribedButtons.Add(button);
			}
		}
	}
	
	private void UnsubscribeAllButtons()
	{
		foreach (var button in subscribedButtons.Where(IsInstanceValid))
		{
			button.FocusEntered -= OnButtonFocusEntered;
		}
		
		subscribedButtons.Clear();
	}
	
	private void OnButtonFocusEntered()
	{
		if (suppressNextFocusSound)
		{
			suppressNextFocusSound = false;
			return;
		}
		
		var focus = GetViewport().GuiGetFocusOwner();
		if (focus == null || focus == lastFocusedControl) return;
		
		lastFocusedControl = focus;
		MenuNavigate_Neutral.Play();
	}
	
	private void GrabFocusSilently(Control control)
	{
		suppressNextFocusSound = true;
		control.GrabFocus();
	}
	
	private IEnumerable<Node> GetAllChildrenRecursive(Node root)
	{
		foreach (var child in root.GetChildren())
		{
			yield return child;
			
			foreach (var grandChild in GetAllChildrenRecursive(child))
				yield return grandChild;
		}
	}
}