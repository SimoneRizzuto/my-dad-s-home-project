using Godot;
using MyFathersHomeProject.Scripts.Dialogue;

public partial class ActorModule : Node2D
{
    public CharacterBody2D CharacterBody => GetParentOrNull<CharacterBody2D>();
    public AnimatedSprite2D MainSprite => GetParent().GetNode<AnimatedSprite2D>($"{nameof(MainSprite)}");
    public CollisionShape2D MainShape => GetParent().GetNode<CollisionShape2D>($"{nameof(MainShape)}");
    public ICharacter Character => (ICharacter)CharacterBody;
}
