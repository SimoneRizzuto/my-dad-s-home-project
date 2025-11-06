using Godot;
using System.Collections.Generic;

namespace MyFathersHomeProject.Scripts.Singletons.SceneStates;
public partial class SceneStates : Node
{
    public Dictionary<string, State> SetStates = new();
    
    // dad knocks scene
    public bool ClothesPickedUp;
    public bool ClothesPutAway;
    public bool ClothesBeingHeld => ClothesPickedUp && !ClothesPutAway;
    
    #region initial setup
    public static SceneStates Instance { get; private set; }
    public override void _Ready()
    {
        Instance = this;
    }
    #endregion
}

public class State
{
    
}

