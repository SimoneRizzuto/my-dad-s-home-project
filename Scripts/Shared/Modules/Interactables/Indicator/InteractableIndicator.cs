using Godot;
using MyFathersHomeProject.Scripts.Singletons.InteractionAllocator;

namespace MyFathersHomeProject.Scripts.Shared.Modules.Interactables.Indicator;
public partial class InteractableIndicator : AnimatedSprite2D
{
	private const int HeightAdjustment = 25;
	
	public override void _Process(double delta)
	{
		if (InteractionAllocator.Instance == null) return;
		
		var position = InteractionAllocator.Instance.ClosestInteractable.GlobalPosition;
		Position = new(position.X, position.Y - HeightAdjustment);
		
		Visible = InteractionAllocator.Instance.ClosestInteractable.OliverIsColliding;
	}
}