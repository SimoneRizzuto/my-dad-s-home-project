using Godot;

namespace MyFathersHomeProject.Scripts.Shared.Helpers;
public static class FadeHelper
{
    private static Node? fadeUtil;
    private static Node? FadeUtil
    {
        get
        {
            if (fadeUtil == null)
            {
                fadeUtil = ((SceneTree)Engine.GetMainLoop()).Root.GetNode("FadeUtil");
            }
            return fadeUtil;
        }
    }
    
    public static void FadeOut(float time = 1.0f, string pattern = "", bool reverse = false, bool smooth = false, Color colour = new())//time := 1.0, color := Color.BLACK, pattern := "", reverse := false, smooth := false
    {
        FadeUtil?.Call("fade_out", time, colour, pattern, reverse, smooth);
    }
    
    public static void FadeIn(float time = 1.0f, string pattern = "", bool reverse = true, bool smooth = false, Color colour = new()) //time := 1.0, color := Color.BLACK, pattern := "", reverse := true, smooth := false
    {
        FadeUtil?.Call("fade_in", time, colour, pattern, reverse, smooth);
    }
}