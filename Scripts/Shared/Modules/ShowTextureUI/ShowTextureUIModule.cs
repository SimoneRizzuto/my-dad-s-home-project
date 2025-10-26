using Godot;
using MyFathersHomeProject.Scripts.Camera;

namespace MyFathersHomeProject.Scripts.Shared.Modules.ShowTextureUI;
public partial class ShowTextureUIModule : Node
{
	public ShowTextureUIModule? Instance { get; set; }
	
	// variables
	private bool changedPosition;
	private Vector2? previousPosition;
	
	public override void _Ready()
	{
		Instance = this;
	}

	/*public void ShowTexture()
	{
		
	}*/
	
	public void ShowTexture(int xAdjustment)
	{
		/*if (mountPosition != null)*/
		{
			previousPosition = PlayerCamera.Instance.Position;

			var newPosition = new Vector2(previousPosition.Value.X + xAdjustment, previousPosition.Value.Y);
			
			PlayerCamera.Instance.ToggleSmoothing(true);
			PlayerCamera.Instance.Mount(newPosition);
			
			GD.Print(newPosition);
			
			changedPosition = true;
		}
		
		
		// show Texture
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
		
		// hide Texture
	}
}