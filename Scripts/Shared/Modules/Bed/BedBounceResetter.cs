using Godot;
using MyFathersHomeProject.Scripts.Player;
using MyFathersHomeProject.Scripts.Shared.Helpers;

namespace MyFathersHomeProject.Scripts.Shared.Modules.Bed;
public partial class BedBounceResetter : Area2D
{
	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
	}
	
	private void OnBodyEntered(Node2D body)
	{
		if (body is not Oliver) return;

		
		var bed = GetNodeHelper.GetBed(GetTree());
		bed.ResetBounceCount();
	}
}