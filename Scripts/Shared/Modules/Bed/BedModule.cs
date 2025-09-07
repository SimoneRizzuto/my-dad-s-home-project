using Godot;
using System;
using System.Diagnostics;
using DialogueManagerRuntime;
using MyFathersHomeProject.Scripts.Dialogue;
using MyFathersHomeProject.Scripts.Player;

namespace MyFathersHomeProject.Scripts.Shared.Modules.Bed;
public partial class BedModule : AnimatedSprite2D
{
	[Export] public BedType Type { get; set; }
	[Export] public bool TriggerDialogue { get; set; }
	[Export] public Resource? Dialogue { get; set; }
	
	// getters
	private Area2D BounceArea => GetNode<Area2D>("BounceArea");
	private CollisionShape2D BounceAreaCollision => GetNode<CollisionShape2D>("BounceArea/CollisionShape2D");
	private CollisionShape2D Collision => GetNode<CollisionShape2D>("Collision/CollisionShape2D");
	private string BedBounceStateString => _bounceTimer is { ElapsedMilliseconds: < 150, IsRunning: true }
		? " bounce"
		: "";
	private string BedSpriteToPlay => $"{GetBedSpriteString(Type)}{BedBounceStateString}";
	
	// variables
	private int _bounceCount;
	private bool _triggerBounce;
	private int _bounceDialogueIndex;
	
	//private readonly Stopwatch _bounceBufferTimer = new();
	
	// idea: if bounce on Bed 3 times, do thing.
	//  if collide with Floor, reset
	private readonly Stopwatch _bounceTimer = new();
	
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
		if (_bounceCount == 3)
		{
			TriggerAction();
		}
		
		if (_triggerBounce)
		{
			_bounceTimer.Restart();
			_triggerBounce = false;
		}
		
		var collisionShape = (RectangleShape2D)Collision.Shape;
		
		if (_bounceTimer.ElapsedMilliseconds < 100)
		{
			collisionShape.Size = new(collisionShape.Size.X, 4);
		}
		else
		{
			collisionShape.Size = new(collisionShape.Size.X, 6);
		}
		
		Play(BedSpriteToPlay);
	}
	
	private void TriggerAction()
	{
		if (Dialogue == null) return;
		
		switch (_bounceDialogueIndex)
		{
			case 0:
				DialogueDirector.Instance.TriggerCutscene(Dialogue, "jump_on_bed_1");
				break;
			case 1:
				DialogueDirector.Instance.TriggerCutscene(Dialogue, "jump_on_bed_2");
				break;
			case 2:
				DialogueDirector.Instance.TriggerCutscene(Dialogue, "jump_on_bed_3");
				break;
		}
		
		_bounceDialogueIndex++;
		_bounceCount = 0;
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
	
	public void ResetBounceCount()
	{
		_bounceCount = 0;
	}
	
	// signals
	private void OnBodyEntered(Node2D body)
	{
		if (body is not Oliver) return;
		_triggerBounce = true;
		
		if (TriggerDialogue)
		{
			_bounceCount++;
		}
	}
}

public enum BedType
{
	Oliver,
	Sasha,
	Papa,
}