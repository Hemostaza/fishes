using Godot;
using System;

public partial class Entity : Area2D
{
    [Export]
    public AnimatedSprite2D animatedSprite2D;

    bool mouseIn = false;

    public override void _Ready()
    {
        base._Ready();
        animatedSprite2D.Play("swimm");
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
    }

    public AnimatedSprite2D GetAnimatedSprite2D(){
        return animatedSprite2D;
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        base._UnhandledInput(@event);
        if(@event is InputEventMouseButton mouseButton && mouseIn){
            
            if(mouseButton.Pressed && mouseButton.ButtonIndex == MouseButton.Left){
                OnLeftClicked();
            }
        }
    }
    virtual public void OnLeftClicked(){
        //GetTree().Root.SetInputAsHandled();
    }
    virtual public void OnRightClicked(){
        GetTree().Root.SetInputAsHandled();
    }

    public override void _MouseEnter()
    {
        base._MouseEnter();
        mouseIn = true;
    }

    public override void _MouseExit()
    {
        base._MouseExit();
        mouseIn = false;
    }

}
