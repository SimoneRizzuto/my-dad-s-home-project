using Godot;
using System;
using System.Diagnostics;
using MyFathersHomeProject.Scripts.Player;

namespace MyFathersHomeProject.Scripts.Shared.Modules.Bed;
public partial class BedModule : AnimatedSprite2D
{
	[Export] public BedType Type { get; set; }
	
	// getters
	private Area2D BounceArea => GetNode<Area2D>("BounceArea");
	private CollisionShape2D Collision => GetNode<CollisionShape2D>("Collision/CollisionShape2D");
	private string BedBounceStateString => _timer is { ElapsedMilliseconds: < 150, IsRunning: true }
		? " bounce"
		: "";
	private string BedSpriteToPlay => $"{GetBedSpriteString(Type)}{BedBounceStateString}";
	
	// variables
	private bool _triggerBounce;
	private readonly Stopwatch _timer = new();
	
	public override void _EnterTree()
	{
		Play(BedBounceStateString);
	}
	
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
		
		if (_timer.ElapsedMilliseconds < 100)
		{
			shape.Size = new(shape.Size.X, 4);
		}
		else
		{
			shape.Size = new(shape.Size.X, 6);
		}
		
		Play(BedSpriteToPlay);
		
		Console.WriteLine(BedSpriteToPlay);
	}
	
	private string GetBedSpriteString(BedType type)
	{
		var shape = (RectangleShape2D)Collision.Shape;
		
		switch (type)
		{
			case BedType.Oliver:
				Collision.Position = new(20, Collision.Position.Y);
				shape.Size = new(40, shape.Size.Y);
				return "oliver bed";
			case BedType.Sasha:
				Collision.Position = new(18, Collision.Position.Y);
				shape.Size = new(36, shape.Size.Y);
				return "sasha bed";
			case BedType.Papa:
				Collision.Position = new(20, Collision.Position.Y);
				shape.Size = new(40, shape.Size.Y);
				return "papa bed";
			default:
				return "oliver";
		}
	}
	
	// signals
	private void OnBodyEntered(Node2D body)
	{
		if (body is not Oliver) return;
		_triggerBounce = true;
	}
}

public enum BedType
{
	Oliver,
	Sasha,
	Papa,
}