using Godot;
using System;
using MyFathersHomeProject.Scripts.Camera;

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
				if (FadeUtil.Instance != null)
				{
					FadeUtil.Instance.FadeOut();
					FadeUtil.Instance.FadeFinished += () => GetTree().Quit();
				}
				break;
		}
	}
}

public enum ButtonAction
{
	SwitchScene,
	ExitGame,
}