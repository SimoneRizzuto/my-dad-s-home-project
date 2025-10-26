using Godot;
using System;

namespace MyFathersHomeProject.Scripts.Shared.Extensions;
public static class NodeExtensions
{
	public static void FadeIn(this CanvasItem target, double duration, Action? trigger = null)
	{
		var tween = target.CreateTween();
		tween.TweenProperty(target, "modulate:a", 1.0f, duration);
		if (trigger != null)
		{
			tween.TweenCallback(Callable.From(trigger));
		}
	}
	
	public static void FadeOut(this CanvasItem target, double duration, Action? trigger = null)
	{
		var tween = target.CreateTween();
		tween.TweenProperty(target, "modulate:a", 0, duration);
		if (trigger != null)
		{
			tween.TweenCallback(Callable.From(trigger));
		}
	}
}