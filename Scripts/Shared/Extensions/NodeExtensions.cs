using Godot;
using System;

namespace MyFathersHomeProject.Scripts.Shared.Extensions;
public static class NodeExtensions
{
	public static void FadeIn(this Node target, double duration, Action trigger)
	{
		var tween = target.CreateTween();
		tween.TweenProperty(target, "modulate:a", 1.0f, duration);
		tween.TweenCallback(Callable.From(trigger));
	}
	
	public static void FadeOut(this Node target, double duration, Action trigger)
	{
		var tween = target.CreateTween();
		tween.TweenProperty(target, "modulate:a", 0, duration);
		tween.TweenCallback(Callable.From(trigger));
	}
}