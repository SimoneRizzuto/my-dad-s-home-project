using System.Threading.Tasks;
using Godot;
using MyFathersHomeProject.Scripts.Dialogue.Base;

namespace MyFathersHomeProject.Scripts.Dialogue.Audio;

public partial class AudioDirector : Node2D, IAsyncDialogueVariables, IAudioDirector
{
    public static AudioDirector Instance { get; private set; }
    public Task ActionCompleted => ActionGiven.Task;
    public TaskCompletionSource ActionGiven { get; set; } = new();

    public override void _Ready()
    {
        Instance = this;
    }
    public void PlaySound(string name)
    {
        throw new System.NotImplementedException();
    }

    public Task PlaySoundAsync(string name)
    {
        throw new System.NotImplementedException();
    }

    public void PlayMusic(string name)
    {
        throw new System.NotImplementedException();
    }

    public void StopMusic(string name)
    {
        throw new System.NotImplementedException();
    }

    public void StopAllAudio()
    {
        throw new System.NotImplementedException();
    }
}