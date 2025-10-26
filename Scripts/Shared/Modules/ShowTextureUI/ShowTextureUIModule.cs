using System.Linq;
using Godot;
using MyFathersHomeProject.Scripts.Camera;
using MyFathersHomeProject.Scripts.Shared.Extensions;

namespace MyFathersHomeProject.Scripts.Shared.Modules.ShowTextureUI;
public partial class ShowTextureUIModule : CanvasLayer
{
	// constants
	private const double FadeSpeed = 0.5d;
	
	// variables
	private bool changedPosition;
	private bool wasMounted;
	private Vector2 previousPosition;
	
	public override void _Ready()
	{
		foreach (var item in GetChildren())
		{
			if (item is CanvasItem canvasItem)
			{
				canvasItem.FadeOut(FadeSpeed);
			}
		}
	}
	
	public void SetTexture(string path)
	{
		if (FileAccess.FileExists(path))
		{
			var textureRect = GetNode<TextureRect>("TextureRect");
			textureRect.Texture = GD.Load<Texture2D>(path);
		}
		else
		{
			GD.PrintErr($"Texture not found at path: {path}");
		}
	}
	
	public void ShowTexture(int xAdjustment = 0)
	{
		if (xAdjustment != 0)
		{
			wasMounted = PlayerCamera.Instance.IsMounted;
			previousPosition = PlayerCamera.Instance.Position;
			
			var newXPosition = new Vector2(previousPosition.X + xAdjustment, previousPosition.Y);
			
			PlayerCamera.Instance.ToggleSmoothing(true);
			PlayerCamera.Instance.Mount(newXPosition, 2);
			
			changedPosition = true;
		}
		
		foreach (var item in GetChildren())
		{
			if (item is CanvasItem canvasItem)
			{
				canvasItem.FadeIn(FadeSpeed);
			}
		}
	}
	
	public void HideTexture()
	{
		if (changedPosition)
		{
			if (wasMounted)
			{
				PlayerCamera.Instance.Mount(previousPosition);
			}
			else
			{
				PlayerCamera.Instance.Dismount();
			}
		}
		
		foreach (var item in GetChildren())
		{
			if (item is CanvasItem canvasItem)
			{
				canvasItem.FadeOut(FadeSpeed);
			}
		}
	}
}