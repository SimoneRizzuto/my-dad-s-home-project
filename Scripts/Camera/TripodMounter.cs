using Godot;
using MyFathersHomeProject.Scripts.Player;

namespace MyFathersHomeProject.Scripts.Camera;
public partial class TripodMounter : Area2D
{
	[Export] public Marker2D MountPosition;
	[Export] public bool InitialMountIsSmooth = true;

	private bool _initialMountDone;
	
	public override void _Ready()
	{
		BodyShapeEntered += OnBodyShapeEntered;
	}
	
	private void OnBodyShapeEntered(Rid bodyrid, Node2D body, long bodyshapeindex, long localshapeindex)
	{
		if (body is Oliver)
		{
			PlayerCamera.Instance.ToggleSmoothing(InitialMountIsSmooth || _initialMountDone);
			PlayerCamera.Instance.Mount(new(MountPosition.GlobalPosition.X, PlayerCamera.Height));
			_initialMountDone = true;
		}
	}
}