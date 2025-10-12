using Godot;
using MyFathersHomeProject.Scripts.Singletons.SceneSwitcher;

namespace MyFathersHomeProject.Scripts.Shared.Modules;
[GlobalClass]
public partial class GameSetup : Node2D
{
	[Export] public string UidToSetup = "uid://bur7w500m6dcn";
	
	public override void _Process(double delta)
	{
		SceneSwitcher.Instance.TransitionToScene(UidToSetup);
	}
}