using System;
using System.Linq;
using DialogueManagerRuntime;
using Godot;
using MyFathersHomeProject.Scripts.Camera;
using MyFathersHomeProject.Scripts.Dialogue;
using MyFathersHomeProject.Scripts.Player;
using MyFathersHomeProject.Scripts.Shared.Helpers;

namespace MyFathersHomeProject.Scripts.Singletons.SceneSwitcher;
public partial class SceneSwitcher : Node, ISceneSwitcher
{
    // CONSTS
    // dialogue resources
    public const string FourteenDaysRemain = "uid://c4t0mo45346ew";
    
    // sets
    public const string Set1_OnlineWorld = "uid://cjd6v6afkjbn0";
    public const string Set1_OliverBedroom = "uid://cap325m8jhcqw";
    public const string Set1_LivingRoom = "uid://bggng8c6npq0v";
    
    public const string TransitionScreen = "uid://ba8ajsihdkrwt";
    
    // getters
    public static SceneSwitcher Instance { get; private set; }

    private PlayerCamera PlayerCamera => PlayerCamera.Instance;
    private CastCrew CastCrew => CastCrew.Instance;
    
    #region cutscene transitionary methods
    
    public override void _EnterTree()
    {
        Instance = this;
    }
    
    private Node? GetMain()
    {
        var tree = GetTree();
        if (tree == null)
        {
            GD.PrintErr("Scene tree is null. SceneSwitcher is likely being accessed too early.");
            return null;
        }
        return tree.CurrentScene;
    }
    public void Spawn_TransitionaryScene()
    {
        
    }

    public void Spawn_DadKnocksScene()
    {
        TransitionToScene(Set1_OliverBedroom);
        
        PlayerCamera.Dismount();
        
        var uidLong = ResourceUid.TextToId(FourteenDaysRemain);
        var path = ResourceUid.GetIdPath(uidLong);
        var resource = ResourceLoader.Load(path);
        
        var title = "dad_knocks";

        DialogueDirector.Instance.FinishCutscene(resource);
        DialogueDirector.Instance.TriggerCutscene(resource, title);
    }
    
    #endregion
    
    #region functionality
    public void TransitionToScene(string uid)
    {
        ClearMain();
        SpawnSceneUid(uid);
        
        CastCrew.FindExistingActors(); // make into signal???
    }
    
    public void TransitionToScene(PackedScene scene)
    {
        ClearMain();
        SpawnScenePacked(scene);
        
        CastCrew.FindExistingActors(); // make into signal???
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
        GetMain()?.AddChild(instanced);
    }
    
    public void SpawnSceneUid(string uid)
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
        GetMain()?.AddChild(instanced);
    }
    
    public void SpawnScenePacked(PackedScene scene)
    {
        var instanced = scene.Instantiate();
        GetMain()?.AddChild(instanced);
    }
    
    private void ClearMain()
    {
        
        var allChildren = GetMain()?.GetChildren();
        if (allChildren != null)
            foreach (var child in allChildren)
            {
                child.QueueFree();
            }
    }
    #endregion
}