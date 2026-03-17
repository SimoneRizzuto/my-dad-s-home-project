shader_type canvas_item;
render_mode unshaded;

uniform sampler2D screen_texture : hint_screen_texture;

uniform float steps = 30.0;

void fragment()
{
	vec4 col = texture(screen_texture, SCREEN_UV);

	// brightness of pixel
	float brightness = length(col.rgb);

	// quantize brightness to create rings
	brightness = floor(brightness * steps) / steps;

	// reconstruct color with stepped brightness
	vec3 base = normalize(col.rgb) * brightness;
	
	COLOR = vec4(base, col.a);
}