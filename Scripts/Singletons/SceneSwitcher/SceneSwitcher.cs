using System.Linq;
using Godot;
using MyFathersHomeProject.Scripts.Player;

namespace MyFathersHomeProject.Scripts.Singletons.SceneSwitcher;
public partial class SceneSwitcher : Node, ISceneSwitcher
{
    private Node Main => GetTree().CurrentScene;
    
    public void SpawnScene(Node node)
    {
        PackedScene scene = new();
        var wow = scene.Pack(node);
        if (wow != Error.Ok)
        {
            GD.PrintErr($"{node.Name} could not be packed for instantiation.");
            return;
        }
        
        var instanced = scene.Instantiate();
        Main.AddChild(instanced);
    }
    
    public void SpawnScene(string uid)
    {
        var uidLong = ResourceUid.TextToId(uid);
        var path = ResourceUid.GetIdPath(uidLong);
        var scene = (PackedScene)ResourceLoader.Load(path);
        if (scene == null)
        {
            GD.PrintErr($"Scene cannot be loaded. UID: {uid}");
            return;
        }
        
        var instanced = scene.Instantiate();
        Main.AddChild(instanced);
    }
    
    public void SpawnTransitionaryScene()
    {
        
    }
    
    private void ClearMain()
    {
        var allChildren = Main.GetChildren();
        foreach (var child in allChildren)
        {
            child.QueueFree();
        }
    }
}