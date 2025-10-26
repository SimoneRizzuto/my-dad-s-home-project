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
	private Vector2? previousPosition;
	
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
	
	public void ShowTexture(int xAdjustment = 0)
	{
		if (xAdjustment != 0)
		{
			previousPosition = PlayerCamera.Instance.Position;
			
			var newXPosition = new Vector2(previousPosition.Value.X + xAdjustment, previousPosition.Value.Y);
			
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
			if (previousPosition != null)
			{
				PlayerCamera.Instance.Mount(previousPosition.Value);
			}
			else
			{
				PlayerCamera.Instance.SetPositionOnOliver();
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