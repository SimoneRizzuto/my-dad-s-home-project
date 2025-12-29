using Godot;
using MyFathersHomeProject.Scripts.Dialogue;
using MyFathersHomeProject.Scripts.Singletons.SceneSwitcher;

namespace MyFathersHomeProject.Scripts.Misc;
public partial class Teaser1_BlackScreen : Node2D
{
	[Export] public Resource DialogueResource;
	
	public override void _Ready()
	{
		DialogueDirector.Instance.TriggerCutscene(DialogueResource, "teaser_1");
	}
}