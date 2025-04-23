using Godot;

public partial class ActorModule : Node2D
{
    [Export] public AnimatedSprite2D AnimatedSprite2D;
    [Export] public AnimationPlayer AnimationPlayer;
    
    public CharacterBody2D Actor => GetParentOrNull<CharacterBody2D>();
}
