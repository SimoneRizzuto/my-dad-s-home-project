using Godot;
using System.Linq;
using MyFathersHomeProject.Scripts.Camera;
using MyFathersHomeProject.Scripts.Player;
using MyFathersHomeProject.Scripts.Shared.Constants;

namespace MyFathersHomeProject.Scripts.Shared.Helpers;
public static class GetNodeHelper
{
    public static Oliver GetOliver(SceneTree tree)
    {
        var oliverNodes = tree.GetNodesInGroup(NodeGroup.Oliver);
        var oliver = oliverNodes.Cast<Oliver>().FirstOrDefault();
        if (oliver == null)
        {
            GD.PrintErr($"{nameof(oliver)} was null.");
        }
        
        return oliver;
    }
    
    public static PlayerCamera GetPlayerCamera(SceneTree tree)
    {
        var playerCameraNodes = tree.GetNodesInGroup(NodeGroup.PlayerCamera);
        var playerCamera = playerCameraNodes.Cast<PlayerCamera>().FirstOrDefault();
        if (playerCamera == null)
        {
            GD.PrintErr($"{nameof(playerCamera)} was null.");
        }
        
        return playerCamera;
    }
    
    public static CanvasLayer GetDialogueBalloonCanvasLayer(SceneTree tree)
    {
        var dialogueBalloonNodes = tree.GetNodesInGroup(NodeGroup.DialogueBalloon);
        var dialogueBalloon = dialogueBalloonNodes.Cast<CanvasLayer>().FirstOrDefault();
        if (dialogueBalloon == null)
        {
            GD.PrintErr($"{nameof(dialogueBalloon)} was null.");
        }
        
        return dialogueBalloon;
    }
}