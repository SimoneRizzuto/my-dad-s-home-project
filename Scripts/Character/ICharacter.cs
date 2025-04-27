using MyFathersHomeProject.Scripts.Shared.Constants;

namespace MyFathersHomeProject.Scripts.Character;
public interface ICharacter
{
    public CharacterState CharacterState { get; set; }
    public Direction LastDirection { get; set; }
    void Move(float direction);
    void Jump();
}