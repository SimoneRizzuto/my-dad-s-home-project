using MyFathersHomeProject.Scripts.Shared.Constants;

namespace MyFathersHomeProject.Scripts.Dialogue;
public interface ICharacter
{
    public Direction LastDirection { get; set; }
    void Move(float direction);
}