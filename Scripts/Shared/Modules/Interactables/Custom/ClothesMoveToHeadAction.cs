using Godot;
using MyFathersHomeProject.Scripts.Player;

namespace MyFathersHomeProject.Scripts.Shared.Modules.Interactables.Custom;
public partial class ClothesMoveToHeadAction : Node2D, IAction
{
	[Export] public Texture2D? PickUpTexture;
	
	// getters
	private Sprite2D? ClothesSprite2D => GetParent()?.GetParent<Sprite2D>();
	private Oliver? Oliver => Oliver.Instance;
	
	public void Action()
	{
		if (Oliver == null) return;
		
		if (ClothesSprite2D != null && PickUpTexture != null)
		{
			ClothesSprite2D.GetParent().RemoveChild(ClothesSprite2D);
			Oliver.AddChild(ClothesSprite2D);
			
			ClothesSprite2D.Texture = PickUpTexture;
			ClothesSprite2D.Position = new(-4, -22);
		}
		
		// activate other Action to place into drawer
		// maybe through signal??
		QueueFree();
	}
}