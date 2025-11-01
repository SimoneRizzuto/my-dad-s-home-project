using Godot;
using MyFathersHomeProject.Scripts.Player;
using MyFathersHomeProject.Scripts.Singletons.SceneStates;

namespace MyFathersHomeProject.Scripts.Shared.Modules.Interactables.Custom;
[GlobalClass]
public partial class ClothesMoveToHeadAction : Node, IAction
{
	[Export] public Texture2D? PickUpTexture;
	
	// getters
	private Node CurrentScene => GetTree().CurrentScene;
	private Sprite2D? ClothesSprite2D => GetParent()?.GetParent<Sprite2D>();
	private Oliver? Oliver => Oliver.Instance;
	
	public void Action()
	{
		if (Oliver == null) return;
		
		if (ClothesSprite2D != null && PickUpTexture != null)
		{
			var bedsideDrawer = GetParent().GetParent().GetParent()
                .GetNode("%BedsideDrawer")
                .GetNode<InteractableModule>("InteractableModule");
			
			ClothesSprite2D.GetParent().RemoveChild(ClothesSprite2D);
			Oliver.AddChild(ClothesSprite2D);
			
			ClothesSprite2D.Texture = PickUpTexture;
			ClothesSprite2D.Position = new(-5, -22);
			
			SceneStates.Instance.ClothesPickedUp = true;
			
			if (bedsideDrawer != null)
			{
				bedsideDrawer.ProcessMode = ProcessModeEnum.Inherit;
			}
		}
		
		QueueFree();
	}
}