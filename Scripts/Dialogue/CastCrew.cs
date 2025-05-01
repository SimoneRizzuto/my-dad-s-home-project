using Godot;
using System.Linq;
using MyFathersHomeProject.Scripts.Dialogue.Actor;
using MyFathersHomeProject.Scripts.Dialogue.Audio;
using MyFathersHomeProject.Scripts.Shared.Constants;

public partial class CastCrew : Node2D
{
    // Cast
    public DialogueDirector Director => DialogueDirector.Instance;
    
    // Actors
    public ActorModule Oliver;
    
    public override void _Ready()
    {
        var tree = GetTree();
        
        //audioDirector = AudioDirector.Instance;
        
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
