using Godot;
using System;

namespace MyFathersHomeProject.Scripts.Menus;
public partial class MenuItemButton : Button
{
	[Export] public ButtonAction Action;
	[Export] public string SwitchSceneUid = "";
	
	public override void _Pressed()
	{
		switch (Action)
		{
			case ButtonAction.SwitchScene:
				break;
			case ButtonAction.ExitGame:
				
				// play fade
				
				GetTree().Quit();
				break;
		}
	}
}

public enum ButtonAction
{
	SwitchScene,
	ExitGame,
}