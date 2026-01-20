namespace MyFathersHomeProject.Scripts.Shared.Constants;
public enum Direction
{
    Left = 1,
    Right = 2,
}

public static class NodeGroup
{
    public const string Oliver = "oliver";
    public const string PlayerCamera = "player camera";
    public const string ActorModule = "actor module";
    public const string DialogueBalloon = "dialogue balloon";
    public const string Door = "door";
    public const string Plate = "plate";
    public const string Bed = "bed";
    public const string Window = "window";
    public const string Stars = "stars";
}

public static class ZIndexLayers
{
    public const int BackgroundLayer = 10;
    public const int ForegroundLayer = 1000;
    public const int MinLayer = -4096;
    public const int MaxLayer = 4096;
}

public enum MenuMode
{
    MainMenu,
    PauseMenu
}