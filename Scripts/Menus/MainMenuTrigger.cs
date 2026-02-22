using System.Linq;
using Godot;
using MyFathersHomeProject.Scripts.Shared.Helpers;

namespace MyFathersHomeProject.Scripts.Menus;

public partial class MainMenuTrigger : Node
{
	private MenuModule? Menu => GetTree().Root
		.GetChildrenRecursive<MenuModule>()
		.FirstOrDefault();
	public override void _Ready()
	{
		
		Menu?.ResetMainMenu();
		Menu?.GoToMainMenu();
	}
}