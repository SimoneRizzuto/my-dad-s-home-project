using Godot;
using MyFathersHomeProject.Scripts.Singletons.SceneSwitcher;

namespace MyFathersHomeProject.Scripts.Shared.Modules;
[GlobalClass]
public partial class GameSetup : Node2D
{
	[Export] public PackedScene PackedScene;
	
	public override void _Process(double delta)
	{
		SceneSwitcher.Instance?.TransitionToScene(PackedScene);
	}
}