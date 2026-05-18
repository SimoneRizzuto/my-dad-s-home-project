using Godot;
using System;
using MyFathersHomeProject.Scripts.Player;

namespace MyFathersHomeProject.Scripts.Camera;
public partial class TripodDismounter : Area2D, IDisposable
{
	[Export] public float DismountDelay = 0.2f;
	[Export] public bool InitialDismountIsSmooth = true;

	private SceneTreeTimer? _dismountTimer;
	private bool _initialMountDone;

	public override void _Ready()
	{
		BodyShapeEntered += OnBodyShapeEntered;
	}

	private void OnBodyShapeEntered(Rid bodyrid, Node2D body, long bodyshapeindex, long localshapeindex)
	{
		if (body is Oliver)
		{
			PlayerCamera.Instance.ToggleSmoothing(InitialDismountIsSmooth || _initialMountDone);
			PlayerCamera.Instance.Dismount();

			_dismountTimer = GetTree().CreateTimer(DismountDelay);
			_dismountTimer.Timeout += OnDismountDelayFinished;

			_initialMountDone = true;
		}
	}

	private void OnDismountDelayFinished()
	{
		PlayerCamera.Instance.ToggleSmoothing(true);
		_dismountTimer = null;
	}

	public void Dispose()
	{
		BodyShapeEntered -= OnBodyShapeEntered;

		if (_dismountTimer != null)
		{
			_dismountTimer.Timeout -= OnDismountDelayFinished;
			_dismountTimer = null;
		}
	}
}