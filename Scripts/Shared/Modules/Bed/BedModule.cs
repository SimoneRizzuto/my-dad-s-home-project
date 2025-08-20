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
	private CollisionShape2D BounceAreaCollision => GetNode<CollisionShape2D>("BounceArea/CollisionShape2D");
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
		
		var collisionShape = (RectangleShape2D)Collision.Shape;
		
		if (_timer.ElapsedMilliseconds < 100)
		{
			collisionShape.Size = new(collisionShape.Size.X, 4);
		}
		else
		{
			collisionShape.Size = new(collisionShape.Size.X, 6);
		}
		
		Play(BedSpriteToPlay);
		
		Console.WriteLine(BedSpriteToPlay);
	}
	
	private string GetBedSpriteString(BedType type)
	{
		var collisionShape = (RectangleShape2D)Collision.Shape;
		var bounceAreaShape = (RectangleShape2D)BounceArea.GetNode<CollisionShape2D>("CollisionShape2D").Shape;
		
		switch (type)
		{
			case BedType.Oliver:
			case BedType.Papa:
			default:
				Collision.Position = new(20, Collision.Position.Y);
				BounceAreaCollision.Position = new(20, BounceAreaCollision.Position.Y);
				
				collisionShape.Size = new(40, collisionShape.Size.Y);
				bounceAreaShape.Size = new(36, bounceAreaShape.Size.Y);
				break;
			case BedType.Sasha:
				Collision.Position = new(18, Collision.Position.Y);
				BounceAreaCollision.Position = new(18, BounceAreaCollision.Position.Y);
				
				collisionShape.Size = new(36, collisionShape.Size.Y);
				bounceAreaShape.Size = new(32, bounceAreaShape.Size.Y);
				break;
		}

		return $"{Enum.GetName(type)?.ToLower()} bed";
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