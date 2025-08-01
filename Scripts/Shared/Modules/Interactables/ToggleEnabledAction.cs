using Godot;

namespace MyFathersHomeProject.Scripts.Shared.Modules.Interactables;
[GlobalClass]
public partial class ToggleEnabledAction : Node, IAction
{
	[Export] public Node? Node;
	
	public void Action()
	{
		// for what i am currently using it for, only use it if the "What are you..." dialogue doesn't occur
		if (Node == null)
		{
			GD.PrintErr("Node to toggle Enabled doesn't exist.");
			return;
		}
		
		switch (Node.ProcessMode)
		{
			case ProcessModeEnum.Disabled:
				Node.ProcessMode = ProcessModeEnum.Inherit;
				break;
			default:
				Node.ProcessMode = ProcessModeEnum.Disabled;
				break;
		}
	}
}