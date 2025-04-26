using Godot;
using System.Linq;
using MyFathersHomeProject.Scripts.Shared.Constants;

public partial class CastActors : Node2D
{
    // Cast
    //public CutsceneDirector director;
    
    // Actors
    public ActorModule oliver;
    
    public override void _Ready()
    {
        var tree = GetTree();

        //var cutsceneDirectorNodes = tree.GetNodesInGroup(NodeGroup.CutsceneDirector);
        //director = cutsceneDirectorNodes.Cast<CutsceneDirector>().FirstOrDefault();

        //audioDirector = AudioDirector.Instance;

        //camera = GetNodeHelper.GetMainCamera2D(tree);
        
        var actorNodes = tree.GetNodesInGroup(NodeGroup.ActorModule);
        var actorBaseNodes = actorNodes.Cast<ActorModule>().ToList();
        
        foreach (var actor in actorBaseNodes)
        {
            if (actor.CharacterBody.Name == "Oliver")
            {
                oliver = actor;
            }
        }
    }
}
