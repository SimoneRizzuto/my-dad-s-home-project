using Godot;
using System;
using MyFathersHomeProject.Scripts.Singletons.SceneSwitcher;

public partial class TransitionScreen : Control
{
    [Export] public PackedScene NextScene;
    [Export] string richText;
    
    private RichTextLabel richTextLabel = new RichTextLabel();
    private Tween tween;

    public override void _Ready()
    {
        richTextLabel = GetNode<RichTextLabel>("RichTextLabel");
        richTextLabel.Text = richText;
        
        TypeWriterText();
        SceneSwitcher.TransitionToScene(null, NextScene);
    }

    private void TypeWriterText()
    {
        tween.TweenProperty(richTextLabel, "visible_ratio", 1.0, 2.0).From(0.0);
    }
}
