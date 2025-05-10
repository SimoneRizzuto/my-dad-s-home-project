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
        var newAudioStreamerNode = new AudioStreamPlayer2D();
        newAudioStreamerNode.Stream = (AudioStream)ResourceLoader.Load($"res://Assets/Audio/Sound/{name}.ogg");
        newAudioStreamerNode.Bus = "Audio";
        newAudioStreamerNode.Name = name;
        AddChild(newAudioStreamerNode);
        newAudioStreamerNode.AddToGroup("Audio");
        newAudioStreamerNode.Play();
        
    }

    public async Task PlaySoundAsync(string name)
    {
        var audioNodes = GetTree().GetNodesInGroup("Audio");
        var arraySize = audioNodes.Count;
        if (arraySize == 0)
        {
            ActionGiven = new TaskCompletionSource();
            var newAudioStreamerNode = new AudioStreamPlayer2D();
            newAudioStreamerNode.Stream = (AudioStream)ResourceLoader.Load($"res://Assets/Audio/{name}.ogg");
            newAudioStreamerNode.Bus = "Audio";
            newAudioStreamerNode.Name = name;
            AddChild(newAudioStreamerNode);
            newAudioStreamerNode.AddToGroup("Audio");
            newAudioStreamerNode.Play();
            await ToSignal(newAudioStreamerNode, "finished");
            await ActionCompleted;
            return;

        }

        foreach (var audioNode in audioNodes)
        {
            if (audioNode.Name != name) continue;
            ActionGiven = new TaskCompletionSource();
            var existingAudioStreamerNode = (AudioStreamPlayer2D)audioNode;
            existingAudioStreamerNode.Play();
            await ToSignal(existingAudioStreamerNode, "finished");
            await ActionCompleted;
            return;
        }
    }

    public void PlayMusic(string name)
    {
        var newAudioStreamerNode = new AudioStreamPlayer2D();
        newAudioStreamerNode.Stream = (AudioStream)ResourceLoader.Load($"res://Assets/Audio/Music/{name}.ogg");
        newAudioStreamerNode.Bus = "Audio";
        newAudioStreamerNode.Name = name;
        AddChild(newAudioStreamerNode);
        newAudioStreamerNode.AddToGroup("Audio");
        newAudioStreamerNode.Play();
        
    }

    public void StopMusic(string name)
    {
        var audioNodes = GetTree().GetNodesInGroup("Audio");
        foreach (var audioNode in audioNodes)
        {
            if (audioNode.Name != name) continue;
            RemoveChild(audioNode);
            audioNode.QueueFree();
            return;

        }
    }

    public void StopAllAudio()
    {
        // Remove all nodes from scene in the Audio node group
        var audioNodes = GetTree().GetNodesInGroup("Audio");
        foreach (var audioNode in audioNodes)
        {
            var streamingPlayer = (AudioStreamPlayer2D)audioNode;
            streamingPlayer.VolumeDb = 0;
            streamingPlayer.Stop();
            streamingPlayer.EmitSignal("finished");
            RemoveChild(audioNode);
            audioNode.QueueFree();

        }
    }
}