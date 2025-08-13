using Godot;

namespace MyFathersHomeProject.Scripts.Shared.Modules.Interactables.Custom;
[GlobalClass]
public partial class TurnOffLightAction : Node, IAction
{
	private bool _switchedOn;
	
	public override void _Ready()
	{
		var children = GetParent().GetParent().GetChildren();
		foreach (var child in children)
		{
			if (child is PointLight2D light)
			{
				_switchedOn = light.Enabled;
				break;
			}
		}
	}
	
	public void Action()
	{
		_switchedOn = !_switchedOn;
		
		var children = GetParent().GetParent().GetChildren();
		foreach (var child in children)
		{
			if (child is PointLight2D light)
			{
				light.Enabled = _switchedOn;
			}
		}
	}
}