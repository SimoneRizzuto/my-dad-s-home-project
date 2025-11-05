using Godot;
using System;

namespace MyFathersHomeProject.Scripts.Shared.Extensions;
public static class NodeExtensions
{
	public const float menuFadeInitialiseTime = 1.5f;
	public const double menuFadeDefaultTime = 0.25d;
	public static void FadeIn(this CanvasItem target, double duration, Action? trigger = null, float finalVal = 1)
	{
		var tween = target.CreateTween();
		var finalValue = (Variant)finalVal;
		tween.TweenProperty(target, "modulate:a", finalValue, duration);
		if (trigger != null)
		{
			tween.TweenCallback(Callable.From(trigger));
		}
	}
	
	public static void FadeOut(this CanvasItem target, double duration, Action? trigger = null, float finalVal = 0)
	{
		var tween = target.CreateTween();
		var finalValue = (Variant)finalVal;
		tween.TweenProperty(target, "modulate:a", finalValue, duration);
		if (trigger != null)
		{
			tween.TweenCallback(Callable.From(trigger));
		}
	}
}