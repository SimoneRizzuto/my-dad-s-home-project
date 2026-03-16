using Godot;

namespace MyFathersHomeProject.Assets.Shaders;

public partial class Compression  : ColorRect
{
	private ShaderMaterial? _material;

	[Export] public float Steps = 6f;
	[Export] public float NoiseStrength = 0.04f;

	public override void _Ready()
	{
		_material = Material as ShaderMaterial;

		UpdateShader();
	}

	public override void _Process(double delta)
	{
		// Example: dynamically change grain slightly
		var t = (float)Time.GetTicksMsec() * 0.001f;
		_material?.SetShaderParameter("noise_scale", 800f + Mathf.Sin(t) * 50f);
	}

	private void UpdateShader()
	{
		_material?.SetShaderParameter("steps", Steps);
		_material?.SetShaderParameter("noise_strength", NoiseStrength);
	}
}