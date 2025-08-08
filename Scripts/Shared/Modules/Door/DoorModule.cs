using System;
using Godot;

namespace MyFathersHomeProject.Scripts.Shared.Modules.Door;
public partial class DoorModule : Node
{
	[Export] public DoorType Type = DoorType.Orange;
	[Export] public bool Closed = true;
	
	// getters
	private AnimatedSprite2D DoorSprite => GetNode<AnimatedSprite2D>("DoorSprite");
	private string DoorOpenStateString => Closed ? "closed" : "open";
	private string DoorSpriteToPlay => $"{GetDoorSpriteString(Type)} {DoorOpenStateString}";
	
	public override void _EnterTree()
	{
		DoorSprite.Play(DoorSpriteToPlay);
	}
	
	public override void _Process(double delta)
	{
		DoorSprite.Play(DoorSpriteToPlay);
	}	
	
	private string GetDoorSpriteString(DoorType type)
	{
		return type switch
		{
			DoorType.Orange => "orange",
			DoorType.Blue => "blue",
			DoorType.OliverBlue => "oliver blue",
			DoorType.Wooden => "wooden",
			_ => "orange"
		};
	}
}

public enum DoorType
{
	Orange,
	Blue,
	OliverBlue,
	Wooden,
}