using Godot;
using System.Linq;
using MyFathersHomeProject.Scripts.Dialogue.Actor;
using MyFathersHomeProject.Scripts.Shared.Constants;

namespace MyFathersHomeProject.Scripts.Dialogue;
public partial class CastCrew : Node2D
{
    public static CastCrew Instance { get; private set; }
    
    // Actors
    public ActorModule Oliver;
    public ActorModule Sasha;
    
    public override void _Ready()
    {
        Instance = this;
        InitialiseActors();
    }
    
    public void InitialiseActors()
    {
        var tree = GetTree();
        
        var actorNodes = tree.GetNodesInGroup(NodeGroup.ActorModule);
        var actorBaseNodes = actorNodes.Cast<ActorModule>().ToList();
        
        foreach (var actor in actorBaseNodes)
        {
            switch (actor.CharacterBody.Name)
            {
                case "Oliver":
                    Oliver = actor;
                    break;
                case "Sasha":
                    Sasha = actor;
                    break;
            }
        }
    }
}