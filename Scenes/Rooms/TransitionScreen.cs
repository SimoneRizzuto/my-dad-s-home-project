using Godot;
using System;
using MyFathersHomeProject.Scripts.Singletons.SceneSwitcher;

public partial class TransitionScreen : Control
{
    [Export] public PackedScene NextScene;
    [Export] string richText;
    
    private RichTextLabel RichTextLabel = new RichTextLabel();
    private Tween tween;
    private SceneSwitcher SceneSwitcher = new();

    public override void _Ready()
    {
        RichTextLabel = GetNode<RichTextLabel>("RichTextLabel");
        RichTextLabel.Text = richText;
        
        TypeWriterText();
        SceneSwitcher.TransitionToScene(null, NextScene);
    }

    private void TypeWriterText()
    {
        tween.TweenProperty(RichTextLabel, "visible_ratio", 1.0, 2.0).From(0.0);
    }
}
