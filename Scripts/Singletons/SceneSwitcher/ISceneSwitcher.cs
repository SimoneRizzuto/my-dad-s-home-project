using Godot;

namespace MyFathersHomeProject.Scripts.Singletons.SceneSwitcher;
public interface ISceneSwitcher
{
    void SpawnScene(Node node);
    void SpawnSceneUid(string uid);
    
    void SpawnScenePacked(PackedScene scene);
}