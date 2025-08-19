using Godot;
using System;
using System.Diagnostics;
using MyFathersHomeProject.Scripts.Player;

namespace MyFathersHomeProject.Scripts.Shared.Modules.Bed;
public partial class BedModule : AnimatedSprite2D
{
	// getters
	private Area2D BounceArea => GetNode<Area2D>("BounceArea");
	private CollisionShape2D Collision => GetNode<CollisionShape2D>("Collision/CollisionShape2D");
	
	// variables
	private bool _triggerBounce;
	private readonly Stopwatch _timer = new();
	
	public override void _Ready()
	{
		BounceArea.BodyEntered += OnBodyEntered;
	}
	
	public override void _Process(double delta)
	{
		if (_triggerBounce)
		{
			_timer.Restart();
			_triggerBounce = false;
		}
		
		var shape = (RectangleShape2D)Collision.Shape;
		
		var animationToPlay = "oliver bed";
		if (_timer.ElapsedMilliseconds < 150)
		{
			shape.Size = new(shape.Size.X, 4);
			animationToPlay += " bounce";
		}
		else
		{
			shape.Size = new(shape.Size.X, 6);
		}
		
		Play(animationToPlay);
	}
	
	// signals
	private void OnBodyEntered(Node2D body)
	{
		if (body is not Oliver) return;
		_triggerBounce = true;
	}
}