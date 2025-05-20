using Godot;
using System.Linq;
using MyFathersHomeProject.Scripts.Dialogue.Actor;
using MyFathersHomeProject.Scripts.Shared.Constants;

namespace MyFathersHomeProject.Scripts.Dialogue;
public partial class CastCrew : Node2D
{
    // Actors
    public ActorModule Oliver;
    
    public override void _Ready()
    {
        var tree = GetTree();
        
        //camera = GetNodeHelper.GetMainCamera2D(tree);
        
        var actorNodes = tree.GetNodesInGroup(NodeGroup.ActorModule);
        var actorBaseNodes = actorNodes.Cast<ActorModule>().ToList();
        
        foreach (var actor in actorBaseNodes)
        {
            if (actor.CharacterBody.Name == "Oliver")
            {
                Oliver = actor;
            }
        }
    }
}