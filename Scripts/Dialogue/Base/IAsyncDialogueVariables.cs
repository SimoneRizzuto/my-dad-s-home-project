using System.Threading.Tasks;

namespace MyFathersHomeProject.Scripts.Dialogue.Base;

public interface IAsyncDialogueVariables
{
    Task ActionCompleted { get; }
    TaskCompletionSource ActionGiven { get; set; }
}