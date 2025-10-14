using System;
using Godot;

namespace MyFathersHomeProject.Scripts.Camera;

/// <summary>
/// Acts as a wrapper around the Universal Fade library: https://github.com/KoBeWi/Godot-Universal-Fade
/// All credit goes to KoBeWi.
/// </summary>
public partial class FadeUtil : Node
{
	[Signal] public delegate void FadeFinishedEventHandler();
	
	// const
	public static FadeUtil? Instance { get; private set; }
	
	// getters
	private Node? FadeWrapper => GetNode("FadeWrapper");
	
	public override void _Ready()
	{
		Instance = this;
	}
	
	public void FadeOut(float time = 1.0f, string pattern = "", bool reverse = false, bool smooth = false, Color colour = new())
	{
		FadeWrapper?.Call("fade_out", time, colour, pattern, reverse, smooth);
		ConnectFinishedSignal();
	}
	
	public void FadeIn(float time = 1.0f, string pattern = "", bool reverse = true, bool smooth = false, Color colour = new())
	{
		FadeWrapper?.Call("fade_in", time, colour, pattern, reverse, smooth);
		ConnectFinishedSignal();
	}
	
	private void ConnectFinishedSignal()
	{
		if (FadeWrapper?.IsConnected("finished", Callable.From(OnFinished)) == true)
		{
			FadeWrapper.Disconnect("finished", Callable.From(OnFinished));
		}
		
		FadeWrapper?.Connect("finished", Callable.From(OnFinished));
	}
	
	private void OnFinished()
	{
		EmitSignal(SignalName.FadeFinished);
	}
}

public static class FadePatternConstants
{
	public const string Diagonal = "Diagonal";
	public const string Diamond = "Diamond";
	public const string GradientHorizontal = "GradientHorizontal";
	public const string GradientVertical = "GradientVertical";
	public const string Noise = "Noise";
	public const string Swirl = "Swirl";
	public const string Radial = "Radial";
}