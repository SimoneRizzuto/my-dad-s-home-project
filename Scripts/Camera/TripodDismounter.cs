using Godot;
using MyFathersHomeProject.Scripts.Player;

namespace MyFathersHomeProject.Scripts.Camera;
public partial class TripodDismounter : Area2D
{
	public override void _Ready()
	{
		BodyShapeEntered += OnBodyShapeEntered;
	}
	
	private void OnBodyShapeEntered(Rid bodyrid, Node2D body, long bodyshapeindex, long localshapeindex)
	{
		if (body is Oliver)
		{
			PlayerCamera.Instance.ToggleSmoothing(true);
			PlayerCamera.Instance.Dismount();
		}
	}
}