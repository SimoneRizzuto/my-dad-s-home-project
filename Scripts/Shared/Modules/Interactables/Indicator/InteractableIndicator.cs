using Godot;
using MyFathersHomeProject.Scripts.Player;
using MyFathersHomeProject.Scripts.Character;
using MyFathersHomeProject.Scripts.Singletons.InteractionAllocator;

namespace MyFathersHomeProject.Scripts.Shared.Modules.Interactables.Indicator;
public partial class InteractableIndicator : AnimatedSprite2D
{
	private const int HeightAdjustment = 25;
	
	public override void _Process(double delta)
	{
		if (InteractionAllocator.Instance == null || Oliver.Instance == null) return;
		
		ProcessVisibility();
		
		var position = InteractionAllocator.Instance.ClosestInteractable.GlobalPosition;
		Position = new(position.X, position.Y - HeightAdjustment);
	}
	
	private void ProcessVisibility()
	{
		if (InteractionAllocator.Instance == null || Oliver.Instance == null) return;
		var isColliding = InteractionAllocator.Instance.ClosestInteractable.OliverIsColliding;
		var isCutscene = Oliver.Instance.CharacterState != CharacterState.Cutscene;
		Visible =  isColliding && isCutscene;
	}
}