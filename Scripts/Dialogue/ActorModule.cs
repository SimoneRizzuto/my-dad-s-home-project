using Godot;

public partial class ActorModule : Node2D
{
    public CharacterBody2D Actor => GetParentOrNull<CharacterBody2D>();
    public AnimatedSprite2D MainSprite => GetParent().GetNode<AnimatedSprite2D>($"{nameof(MainSprite)}");
}
