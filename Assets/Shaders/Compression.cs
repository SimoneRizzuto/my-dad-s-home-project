using Godot;

namespace MyFathersHomeProject.Assets.Shaders;

public partial class Compression  : ColorRect
{
	private ShaderMaterial? _material;

	[Export] public float Steps = 30f;

	public override void _Ready()
	{
		_material = Material as ShaderMaterial;

		UpdateShader();
	}

	private void UpdateShader()
	{
		_material?.SetShaderParameter("steps", Steps);
	}
}