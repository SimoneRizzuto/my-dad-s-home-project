using Godot;

namespace MyFathersHomeProject.Scripts.Singletons.SceneSwitcher;
public interface ISceneSwitcher
{
    void SpawnScene(Node node);
    void SpawnScene(string uid);
}