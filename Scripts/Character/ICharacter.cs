using MyFathersHomeProject.Scripts.Shared.Constants;

namespace MyFathersHomeProject.Scripts.Character;
public interface ICharacter
{
    public CharacterState CharacterState { get; set; }
    public Direction LastDirection { get; set; }
    public bool IsJumping { get; }
    void Move(float direction);
    void PlayAnimation(string animationName);
    void SetDirection(Direction direction);
    void Jump();
    void SetVisibility(bool visible);
}