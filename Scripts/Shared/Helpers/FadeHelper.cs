using Godot;

namespace MyFathersHomeProject.Scripts.Shared.Helpers;

/// <summary>
/// Acts as a wrapper around the Universal Fade library: https://github.com/KoBeWi/Godot-Universal-Fade
/// All credit goes to KoBeWi.
/// </summary>
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

public static class FadePattern
{
    public const string Diagonal = "Diagonal";
    public const string Diamond = "Diamond";
    public const string GradientHorizontal = "GradientHorizontal";
    public const string GradientVertical = "GradientVertical";
    public const string Noise = "Noise";
    public const string Swirl = "Swirl";
    public const string Radial = "Radial";
}