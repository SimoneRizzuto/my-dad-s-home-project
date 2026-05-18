using Godot;
using MyFathersHomeProject.Scripts.Singletons.SceneSwitcher;

namespace MyFathersHomeProject.Scenes.Dialogue;

public partial class CreditsManager : Node
{
	private void PlayCredits()
	{
		SceneSwitcher.Instance?.SpawnSceneUid(SceneSwitcher.Credits);
	}
}