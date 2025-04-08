namespace MyFathersHomeProject.Scripts.Shared.Constants;
public enum Direction
{
    Up = 0,
    Left = 1,
    Right = 2,
    Down = 3,
    None = 4,
}

public static class NodeGroup
{
    public const string Oliver = "oliver";
    public const string PlayerCamera = "player camera";
}

public static class ZIndexLayers
{
    public const int BackgroundLayer = 10;
    public const int ForegroundLayer = 1000;
    public const int MinLayer = -4096;
    public const int MaxLayer = 4096;
}