using Godot;
using MyFathersHomeProject.Scripts.Player;
using MyFathersHomeProject.Scripts.Shared.Constants;
using MyFathersHomeProject.Scripts.Shared.Helpers;

namespace MyFathersHomeProject.Scripts.Tools.ZIndexModifier;
public partial class ZIndexModifier : Area2D
{
    [Export] public int ZIndex = 2000;
    
    private Oliver Oliver => GetNodeHelper.GetOliver(GetTree());
    
    public override void _Ready()
    {
        BodyShapeEntered += OnBodyShapeEntered;
    }
    
    // events
    private void OnBodyShapeEntered(Rid bodyrid, Node2D body, long bodyshapeindex, long localshapeindex)
    {
        if (body is Oliver)
        {
            switch (ZIndex)
            {
                case > ZIndexLayers.MaxLayer:
                    ZIndex = ZIndexLayers.MaxLayer;
                    break;
                case < ZIndexLayers.MinLayer:
                    ZIndex = ZIndexLayers.MinLayer;
                    break;
            }

            Oliver.ZIndex = ZIndex;
        }
    }
}