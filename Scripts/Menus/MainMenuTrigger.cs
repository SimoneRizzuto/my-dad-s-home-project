using Godot;
using System.Linq;
using MyFathersHomeProject.Scripts.Camera;
using MyFathersHomeProject.Scripts.Shared.Helpers;
using MyFathersHomeProject.Scripts.Shared.Extensions;

namespace MyFathersHomeProject.Scripts.Menus;
public partial class MainMenuTrigger : Node
{
	private MenuModule? Menu => GetTree().Root
		.GetChildrenRecursive<MenuModule>()
		.FirstOrDefault();
	public override void _Ready()
	{
		Menu?.ResetMainMenu();
		FadeUtil.Instance?.FadeIn(NodeExtensions.MenuFadeInitialiseTime);
		Menu?.GoToMainMenu();
	}
}