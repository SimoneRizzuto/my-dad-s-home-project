using Godot;
using MyFathersHomeProject.Scripts.Shared.Helpers;

namespace MyFathersHomeProject.Scripts.Camera;
public partial class TripodMounter : Area2D
{
    private PlayerCamera PlayerCamera => GetNodeHelper.GetPlayerCamera(GetTree());
    private Marker2D MountPosition => GetNode<Marker2D>("MountPosition");
    
    public override void _Ready()
    {
        BodyShapeEntered += OnBodyShapeEntered;
    }
    
    private void OnBodyShapeEntered(Rid bodyrid, Node2D body, long bodyshapeindex, long localshapeindex)
    {
        if (body is Oliver)
        {
            PlayerCamera.Mount(new(MountPosition.GlobalPosition.X, PlayerCamera.Height));
        }
    }
}