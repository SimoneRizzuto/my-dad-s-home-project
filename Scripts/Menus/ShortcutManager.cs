using Godot;
using MyFathersHomeProject.Scripts.Shared.Constants;

namespace MyFathersHomeProject.Scripts.Menus;
public partial class ShortcutManager : Node2D
{
    [Export] public DisplayServer.WindowMode WindowMode = DisplayServer.WindowMode.Windowed;
    
    public override void _Ready()
    {
        DisplayServer.WindowSetMode(WindowMode);
    }
    
    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed(InputMapAction.FullscreenToggle))
        {
            switch (DisplayServer.WindowGetMode())
            {
                case DisplayServer.WindowMode.ExclusiveFullscreen:
                    DisplayServer.WindowSetMode(DisplayServer.WindowMode.Windowed);
                    break;
                default:
                    DisplayServer.WindowSetMode(DisplayServer.WindowMode.ExclusiveFullscreen);
                    break;
            }
        }
    }
}