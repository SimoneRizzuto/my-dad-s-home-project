using Godot;
using MyFathersHomeProject.Scripts.Shared.Modules;

namespace MyFathersHomeProject.Scripts.Misc;
public partial class FoodPlate : CharacterBody2D, IAction
{
    private bool _sheIsFlying;
    
    public override void _Process(double delta)
    {
        //if (!_sheIsFlying) return;
        
        //Velocity = new Vector2(Velocity.X, Velocity.Y * (float)delta);
    }
    
    public void Action()
    {
        _sheIsFlying = true;
        
    }
}