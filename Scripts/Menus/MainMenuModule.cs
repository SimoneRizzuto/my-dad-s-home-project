using System;
using System.Linq;
using System.Threading.Tasks;
using Godot;
using MyFathersHomeProject.Scripts.Camera;
using MyFathersHomeProject.Scripts.Shared.Extensions;
using MyFathersHomeProject.Scripts.Shared.Helpers;

namespace MyFathersHomeProject.Scripts.Menus;
public partial class MainMenuModule : CanvasLayer
{
	// getters
	private Control MainMenu => GetNode<Control>("MainMenu");
	private Control SettingsMenu => GetNode<Control>("SettingsMenu");
	private Control DebugMenu => GetNode<Control>("DebugMenu");
	
	private Button GoInsideButton => GetNode<Button>("%GoInsideButton");
	
	// variable
	private MenuFocus CurrentFocus;
	
	public override void _Ready()
	{
		MainMenu.Visible = false;
		SettingsMenu.Visible = false;
		
		FadeUtil.Instance?.FadeIn(2);
		GoToMainMenu();
	}
	
	public void GoToMainMenu()
	{
		SettingsMenu.FadeOut(0.25d, () => Console.WriteLine("this is a test to see if it worked"));
		//SettingsMenu.ProcessMode = ProcessModeEnum.Disabled;

		MainMenu.Visible = true;
		MainMenu.FadeIn(0.25d, () => Console.WriteLine("this is a secondary test, just because"));
		GoInsideButton.GrabFocus();

		CurrentFocus = MenuFocus.Main;
	}
	
	public void GoToSettingsMenu()
	{
		MainMenu.FadeOut(0.25d, () => Console.WriteLine("this is a secondary test, just because"));
		//MainMenu.ProcessMode = ProcessModeEnum.Disabled;
		
		SettingsMenu.Visible = true;
		SettingsMenu.FadeIn(0.25d, () => Console.WriteLine("this is a test to see if it worked"));
		
		CurrentFocus = MenuFocus.Settings;
	}
	
	public void GoToDebugMenu()
	{
		CurrentFocus = MenuFocus.Debug;
	}
}

public enum MenuFocus
{
	Main,
	Settings,
	Debug,
}