using System;
using Godot;
using MyFathersHomeProject.Scripts.Shared.Modules.Interactables;

namespace MyFathersHomeProject.Scripts.Shared.Modules.Door;

[Icon("res://Assets/Textures/StaticObjects/OliverBedroom/oliver-room-door-2.png")]
public partial class DoorModule : Node
{
	[Export] public DoorType Type = DoorType.Orange;
	[Export] public bool Closed = true;
	[Export] public bool Locked;
	
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
	
	public void EnableNavigationAction(bool enable = true)
	{
		if (Locked) return;
	    
    	var doorNavigationTrigger = GetNode<InteractableModule>("DoorNavigationTrigger");
    	switch (enable)
    	{
    		case true:
    			doorNavigationTrigger.ProcessMode = ProcessModeEnum.Inherit;
    			break;
    		case false:
    			doorNavigationTrigger.ProcessMode = ProcessModeEnum.Disabled;
    			break;
    	}
    }
	
	private string GetDoorSpriteString(DoorType type)
	{
		return type switch
		{
			DoorType.Orange => "orange",
			DoorType.Blue => "blue",
			DoorType.OliverBlue => "oliver blue",
			DoorType.Wooden => "wooden",
			DoorType.WoodenBathroom => "wooden bathroom",
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
	WoodenBathroom,
}