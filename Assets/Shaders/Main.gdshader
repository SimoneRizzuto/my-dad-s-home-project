shader_type canvas_item;
render_mode unshaded;

uniform sampler2D screen_texture : hint_screen_texture;

uniform float steps = 30.0;
uniform float noise_strength = 0.04;
uniform float noise_scale = 800.0;

float random(vec2 uv){
	return fract(sin(dot(uv, vec2(12.9898,78.233))) * 43758.5453);
}

void fragment()
{
	vec4 col = texture(screen_texture, SCREEN_UV);

	// brightness of pixel
	float brightness = length(col.rgb);

	// quantize brightness to create rings
	brightness = floor(brightness * steps) / steps;

	// reconstruct color with stepped brightness
	vec3 base = normalize(col.rgb) * brightness;

	// grain noise
	float noise = random(SCREEN_UV * noise_scale);

	base += (noise - 0.5) * noise_strength;

	COLOR = vec4(base, col.a);
}