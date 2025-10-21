using Godot;
using MyFathersHomeProject.Scripts.Camera;

namespace MyFathersHomeProject.Scripts.Menus;
public partial class QuitButton : Button
{
	[Export] public string ButtonText = "Not Now";

	public override void _Ready()
	{
		Text = ButtonText;
	}

	public override void _Pressed()
	{
		if (FadeUtil.Instance != null)
		{
			FadeUtil.Instance.FadeOut();
			FadeUtil.Instance.FadeFinished += () => GetTree().Quit();
		}
	}
}