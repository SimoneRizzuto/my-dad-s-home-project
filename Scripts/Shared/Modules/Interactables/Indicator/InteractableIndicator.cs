using Godot;
using MyFathersHomeProject.Scripts.Player;
using MyFathersHomeProject.Scripts.Character;
using MyFathersHomeProject.Scripts.Singletons.InteractionAllocator;

namespace MyFathersHomeProject.Scripts.Shared.Modules.Interactables.Indicator;
public partial class InteractableIndicator : AnimatedSprite2D
{
	// consts
	private const int HeightAdjustment = 25;
	
	public override void _Process(double delta)
	{
		ProcessVisibility();
		ProcessGlobalPosition();
	}

	private void ProcessGlobalPosition()
	{
		if (InteractionAllocator.Instance?.ClosestInteractable == null) return;
		
		var texture = SpriteFrames.GetFrameTexture(Animation, Frame);
		var width = texture.GetSize().X;
		var center = width / 2;
		
		var position = InteractionAllocator.Instance.ClosestInteractable.GlobalPosition;
		var offset = InteractionAllocator.Instance.ClosestInteractable.IndicatorOffset;
		
		var x = position.X - center + offset.X;
		var y = position.Y - HeightAdjustment + offset.Y;
		GlobalPosition = new Vector2(x, y);
	}

	private void ProcessVisibility()
	{
		if (InteractionAllocator.Instance?.ClosestInteractable == null || Oliver.Instance == null) return;
		
		var isColliding = InteractionAllocator.Instance.ClosestInteractable.OliverIsColliding;
		var isCutscene = Oliver.Instance.CharacterState != CharacterState.Cutscene;
		Visible =  isColliding && isCutscene;
	}
}