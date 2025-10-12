using Godot;
using MyFathersHomeProject.Scripts.Camera;

namespace MyFathersHomeProject.Scripts.Menus;
public partial class MainMenuModule : CanvasLayer
{
	// getters
	private Button GoInside => GetNode<Button>("%Go Inside");
	private Button Options => GetNode<Button>("%Options");
	private Button Debug => GetNode<Button>("%Debug");
	private Button NotNow => GetNode<Button>("%Not Now");
	
	public override void _Ready()
	{
		FadeUtil.Instance?.FadeIn(2);
		
		GoInside.GrabFocus();
	}
}