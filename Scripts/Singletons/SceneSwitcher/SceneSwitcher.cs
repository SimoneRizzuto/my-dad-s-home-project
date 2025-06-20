using System;
using System.Linq;
using Godot;
using MyFathersHomeProject.Scripts.Camera;
using MyFathersHomeProject.Scripts.Dialogue;
using MyFathersHomeProject.Scripts.Player;

namespace MyFathersHomeProject.Scripts.Singletons.SceneSwitcher;
public partial class SceneSwitcher : Node, ISceneSwitcher
{
    // const
    public const string Set1_OnlineWorld = "uid://cjd6v6afkjbn0";
    public const string Set1_OliverBedroom = "uid://cap325m8jhcqw";
    
    // getters
    private Node Main => GetTree().CurrentScene;
    private PlayerCamera PlayerCamera => PlayerCamera.Instance;
    private CastCrew CastCrew => CastCrew.Instance;
    
    #region cutscene transitionary methods
    public void Spawn_TransitionaryScene()
    {
        
    }

    public void Spawn_Set1_OliverBedroom()
    {
        TransitionToScene(Set1_OliverBedroom);
        PlayerCamera.SetPositionOnOliver(); // make into signal???
        
        /*do Oliver.SetCharacterState(1)
         do Oliver.PlayAnimation("sit left")*/
    }
    
    #endregion
    
    #region functionality
    public void TransitionToScene(string uid)
    {
        ClearMain();
        SpawnScene(uid);
        CastCrew.InitialiseActors(); // make into signal???
    }
    
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
    
    private void ClearMain()
    {
        var allChildren = Main.GetChildren();
        foreach (var child in allChildren)
        {
            child.QueueFree();
        }
    }
    #endregion
}