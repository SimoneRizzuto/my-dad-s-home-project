using System.Threading.Tasks;

namespace MyFathersHomeProject.Scripts.Dialogue.Audio;
public interface IAudioDirector
{
    void PlaySound(string name);
    Task PlaySoundAsync(string name);
    void PlayMusic(string name);
    void StopMusic(string name);
    void StopAllAudio();
}